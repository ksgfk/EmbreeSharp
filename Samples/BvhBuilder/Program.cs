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

        public long NodeCount()
        {
            return IsLeaf ? Unsafe.As<NodeHeader, LeafNode>(ref this).NodeCount() : Unsafe.As<NodeHeader, InnerNode>(ref this).NodeCount();
        }

        public double Sah()
        {
            return IsLeaf ? Unsafe.As<NodeHeader, LeafNode>(ref this).Sah() : Unsafe.As<NodeHeader, InnerNode>(ref this).Sah();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct InnerNode
    {
        public NodeHeader Header;
        public NativeMemoryView<Ref<NodeHeader>> Childrens;
        public NativeMemoryView<RTCBounds> Bounds;

        public readonly long NodeCount()
        {
            long cnt = 0;
            foreach (var i in Childrens)
            {
                ref readonly var c = ref i.Value;
                cnt += c.IsLeaf ? i.Cast<LeafNode>().Value.NodeCount() : i.Cast<InnerNode>().Value.NodeCount();
            }
            return cnt;
        }

        public readonly double Sah()
        {
            if (Bounds.Length <= 0)
            {
                return 0;
            }
            RTCBounds holeBound = Bounds[0];
            double holeSah = 0;
            for (nuint i = 0; i < Childrens.Length; i++)
            {
                ref readonly var b = ref Bounds[i];
                var childRef = Childrens[i];
                ref readonly var c = ref childRef.Value;
                holeBound = holeBound.Union(in b);
                var sah = c.IsLeaf ? childRef.Cast<LeafNode>().Value.Sah() : childRef.Cast<InnerNode>().Value.Sah();
                var t = sah * b.SurfaceArea();
                holeSah += t;
            }
            return 1 + holeSah / holeBound.SurfaceArea();
        }
    }

    struct Prims
    {
        public RTCBounds Bounds;
        public uint PrimId;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct LeafNode
    {
        public NodeHeader Header;
        public NativeMemoryView<Prims> Prims;

        public readonly long NodeCount() => 1;
        public readonly double Sah()
        {
            double hole = 0;
            foreach (ref readonly var i in Prims)
            {
                hole += i.Bounds.SurfaceArea();
            }
            return hole;
        }
    }

    private static void Main(string[] args)
    {
        const int N = 10000000;
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
        string config = "verbose=3";
        using EmbreeDevice device = new(config);
        device.SetErrorFunction((code, str) => Console.WriteLine($"error {code}, {str}"));
        for (int i = 0; i < 10; i++)
        {
            Build(device, RTCBuildQuality.RTC_BUILD_QUALITY_LOW, prims);
            Build(device, RTCBuildQuality.RTC_BUILD_QUALITY_MEDIUM, prims);
            Build(device, RTCBuildQuality.RTC_BUILD_QUALITY_HIGH, prims);
            Console.WriteLine();
        }
    }

    static void Build(EmbreeDevice device, RTCBuildQuality quality, RTCBuildPrimitive[] prims)
    {
        Stopwatch sw = Stopwatch.StartNew();
        using EmbreeBuilder<NodeHeader, InnerNode, LeafNode> builder = new(device);
        builder.SetBuildQuality(quality);
        builder.SetBuildPrimitive(prims);
        builder.SetCreateNodeFunction((allocator, childCount) =>
        {
            ref InnerNode node = ref allocator.Allocate<InnerNode>(32);
            node.Header.IsLeaf = false;
            node.Childrens = new NativeMemoryView<Ref<NodeHeader>>(ref allocator.Allocate<Ref<NodeHeader>>(childCount, 32), childCount);
            node.Bounds = new NativeMemoryView<RTCBounds>(ref allocator.Allocate<RTCBounds>(childCount, 32), childCount);
            return ref node;
        });
        builder.SetSetNodeChildrenFunction((ref InnerNode n, NativeMemoryView<Ref<NodeHeader>> children) =>
        {
            for (nuint i = 0; i < children.Length; i++)
            {
                n.Childrens[i] = children[i];
            }
        });
        builder.SetSetNodeBoundsFunction((ref InnerNode n, NativeMemoryView<Ref<RTCBounds>> bounds) =>
        {
            for (nuint i = 0; i < bounds.Length; i++)
            {
                n.Bounds[i] = bounds[i].Value;
            }
        });
        builder.SetCreateLeafFunction((allocator, prims) =>
        {
            if (prims.Length <= 0)
            {
                return ref allocator.NullRef<LeafNode>();
            }
            ref LeafNode node = ref allocator.Allocate<LeafNode>(32);
            node.Header.IsLeaf = true;
            node.Prims = new NativeMemoryView<Prims>(ref allocator.Allocate<Prims>(prims.Length, 32), prims.Length);
            for (nuint i = 0; i < prims.Length; i++)
            {
                ref var prim = ref prims[i];
                ref var my = ref node.Prims[i];
                my.Bounds = prim.GetBounds();
                my.PrimId = prim.primID;
            }
            return ref node;
        });
        builder.SetSplitPrimitiveFunction((ref readonly RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds) =>
        {
            leftBounds = primitive.GetBounds();
            rightBounds = primitive.GetBounds();
            leftBounds.SetUpperDimension((int)dimension, position);
            rightBounds.SetLowerDimension((int)dimension, position);
        });
        Ref<NodeHeader> root = builder.Build();
        sw.Stop();
        if (root.IsNull)
        {
            Console.WriteLine("bvh root is null");
        }
        else
        {
            double sah = root.Value.Sah();
            long nodes = root.Value.NodeCount();
            Console.WriteLine($"Nodes={nodes} SAH={sah} ms={sw.ElapsedMilliseconds}");
        }
    }
}
