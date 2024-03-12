using EmbreeSharp;
using EmbreeSharp.Native;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class Program
{
    [StructLayout(LayoutKind.Sequential)]
    struct NodeHeader
    {
        public bool IsLeaf;

        public int NodeCount()
        {
            if (IsLeaf)
            {
                return Unsafe.As<NodeHeader, LeafNode>(ref this).NodeCount();
            }
            else
            {
                return Unsafe.As<NodeHeader, InnerNode>(ref this).NodeCount();
            }
        }

        public float Sah()
        {
            if (IsLeaf)
            {
                return Unsafe.As<NodeHeader, LeafNode>(ref this).Sah();
            }
            else
            {
                return Unsafe.As<NodeHeader, InnerNode>(ref this).Sah();
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct InnerNode
    {
        public NodeHeader Header;
        public RTCBounds LeftBound;
        public RTCBounds RightBound;
        public Ref<NodeHeader> LeftNode;
        public Ref<NodeHeader> RightNode;

        public readonly int NodeCount()
        {
            int leftCnt = 0;
            if (!LeftNode.IsNull)
            {
                leftCnt = LeftNode.Value.IsLeaf ? LeftNode.Cast<LeafNode>().Value.NodeCount() : LeftNode.Cast<InnerNode>().Value.NodeCount();
            }
            int rightCnt = 0;
            if (!RightNode.IsNull)
            {
                rightCnt = RightNode.Value.IsLeaf ? RightNode.Cast<LeafNode>().Value.NodeCount() : RightNode.Cast<InnerNode>().Value.NodeCount();
            }
            return 1 + leftCnt + rightCnt;
        }

        public readonly float Sah()
        {
            RTCBounds holeBound = LeftBound.Union(in RightBound);
            float holeArea = holeBound.SurfaceArea();
            float leftArea = LeftBound.SurfaceArea();
            float rightArea = RightBound.SurfaceArea();
            float leftSah = 0;
            if (!LeftNode.IsNull)
            {
                leftSah = LeftNode.Value.IsLeaf ? LeftNode.Cast<LeafNode>().Value.Sah() : LeftNode.Cast<InnerNode>().Value.Sah();
            }
            float rightSah = 0;
            if (!RightNode.IsNull)
            {
                rightSah = RightNode.Value.IsLeaf ? RightNode.Cast<LeafNode>().Value.Sah() : RightNode.Cast<InnerNode>().Value.Sah();
            }
            return 1 + (leftArea * leftSah + rightArea * rightSah) / holeArea;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct LeafNode
    {
        public NodeHeader Header;
        public RTCBounds Bounds;
        public uint PrimId;

        public int NodeCount()
        {
            return 1;
        }

        public float Sah()
        {
            return 1;
        }
    }

    private static void Main(string[] args)
    {
        const int N = 5000000;
        RTCBuildPrimitive[] prims = new RTCBuildPrimitive[N];
        Random rand = new();
        float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;
        for (int i = 0; i < prims.Length; i++)
        {
            float x = rand.NextSingle() * 1000;
            float y = rand.NextSingle() * 1000;
            float z = rand.NextSingle() * 1000;
            ref RTCBuildPrimitive p = ref prims[i];
            p.lower_x = x;
            p.lower_y = y;
            p.lower_z = z;
            p.geomID = 0;
            p.upper_x = x + 1;
            p.upper_y = y + 1;
            p.upper_z = z + 1;
            p.primID = (uint)i;
            minX = Math.Min(minX, p.lower_x);
            minY = Math.Min(minY, p.lower_y);
            minZ = Math.Min(minZ, p.lower_z);
            maxX = Math.Max(maxX, p.upper_x);
            maxY = Math.Max(maxY, p.upper_y);
            maxZ = Math.Max(maxZ, p.upper_z);
        }
        for (int i = 0; i < 100; i++)
        {
            Build(RTCBuildQuality.RTC_BUILD_QUALITY_LOW, prims);
            Build(RTCBuildQuality.RTC_BUILD_QUALITY_MEDIUM, prims);
            Build(RTCBuildQuality.RTC_BUILD_QUALITY_HIGH, prims);
        }
    }

    static void Build(RTCBuildQuality quality, RTCBuildPrimitive[] prims)
    {
        Stopwatch sw = Stopwatch.StartNew();
        string config = "verbose=3";
        using EmbreeDevice device = new(config);
        device.SetErrorFunction((code, str) => Console.WriteLine($"error {code}, {str}"));
        using EmbreeBuilder<InnerNode, LeafNode> builder = new(device);
        builder.SetBuildQuality(quality);
        builder.SetMaxDepth(1024);
        builder.SetMaxLeafSize(1);
        builder.SetBuildPrimitive(prims);
        builder.SetCreateNodeFunction((allocator, childCount) =>
        {
            ref InnerNode node = ref allocator.Allocate<InnerNode>(32);
            node.Header.IsLeaf = false;
            return ref node;
        });
        builder.SetSetNodeChildrenFunction((ref InnerNode n, NativeMemoryView<Ref<InnerNode>> children) =>
        {
            n.LeftNode = children[0].Cast<NodeHeader>();
            n.RightNode = children[1].Cast<NodeHeader>();
        });
        builder.SetSetNodeBoundsFunction((ref InnerNode n, NativeMemoryView<Ref<RTCBounds>> bounds) =>
        {
            n.LeftBound = bounds[0].Value;
            n.RightBound = bounds[1].Value;
        });
        builder.SetCreateLeafFunction((allocator, prims) =>
        {
            if (prims.Length <= 0)
            {
                return ref Unsafe.NullRef<LeafNode>();
            }
            ref LeafNode node = ref allocator.Allocate<LeafNode>(32);
            node.Header.IsLeaf = true;
            ref var prim = ref prims[0];
            node.Bounds = prim.GetBounds();
            node.PrimId = prim.primID;
            return ref node;
        });
        builder.SetSplitPrimitiveFunction((ref readonly RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds) =>
        {
            leftBounds = primitive.GetBounds();
            rightBounds = primitive.GetBounds();
            if (dimension == 0)
            {
                leftBounds.upper_x = position;
                rightBounds.lower_x = position;
            }
            if (dimension == 1)
            {
                leftBounds.upper_y = position;
                rightBounds.lower_y = position;
            }
            if (dimension == 2)
            {
                leftBounds.upper_z = position;
                rightBounds.lower_z = position;
            }
        });
        Ref<NodeHeader> root = builder.Build().Cast<NodeHeader>();
        sw.Stop();
        if (root.IsNull)
        {
            Console.WriteLine("bvh root is null");
        }
        else
        {
            float sah = root.Value.Sah();
            int nodes = root.Value.NodeCount();
            Console.WriteLine($"Nodes={nodes} SAH={sah} ms={sw.ElapsedMilliseconds}");
        }
    }
}
