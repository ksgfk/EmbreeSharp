using EmbreeSharp.Native;
using System.Runtime.CompilerServices;

namespace EmbreeSharp.Test;

[TestClass]
public class TestBuilder
{
    public struct Node
    {
        public RTCBounds LeftBound;
        public RTCBounds RightBound;
        public Ref<Node> LeftChild;
        public Ref<Node> RightChild;
        public uint Id;
        public bool IsLeaf;
    }

    [TestMethod]
    public void Example()
    {
        const int N = 2300000;
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
        device.SetErrorFunction((code, str) =>
        {
            Console.WriteLine($"error {code}, {str}");
            Assert.Fail();
        });
        using EmbreeBuilder builder = new(device);
        builder.SetBuildQuality(RTCBuildQuality.RTC_BUILD_QUALITY_HIGH);
        builder.SetMaxDepth(1024);
        builder.SetMaxLeafSize(1);
        builder.SetBuildPrimitive(prims);
        builder.SetCreateNodeFunction((allocator, childCount) =>
        {
            RTCThreadLocalAllocation alloc = allocator.Allocate((nuint)Unsafe.SizeOf<Node>(), 32);
            alloc.Ref<Node>().IsLeaf = false;
            return alloc;
        });
        builder.SetSetNodeChildrenFunction((node, children) =>
        {
            ref Node n = ref node.Ref<Node>();
            n.LeftChild = children[0].AsRef<Node>();
            n.RightChild = children[1].AsRef<Node>();
        });
        builder.SetSetNodeBoundsFunction((node, bounds) =>
        {
            ref Node n = ref node.Ref<Node>();
            n.LeftBound = bounds[0].Value;
            n.RightBound = bounds[1].Value;
        });
        builder.SetCreateLeafFunction((allocator, prims) =>
        {
            RTCThreadLocalAllocation alloc = allocator.Allocate((nuint)Unsafe.SizeOf<Node>(), 32);
            alloc.Ref<Node>().IsLeaf = true;
            return alloc;
        });
        builder.SetSplitPrimitiveFunction((ref readonly RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds) =>
        {
            leftBounds.lower_x = primitive.lower_x;
            leftBounds.lower_y = primitive.lower_y;
            leftBounds.lower_z = primitive.lower_z;
            leftBounds.upper_x = primitive.upper_x;
            leftBounds.upper_y = primitive.upper_y;
            leftBounds.upper_z = primitive.upper_z;
            rightBounds.lower_x = primitive.lower_x;
            rightBounds.lower_y = primitive.lower_y;
            rightBounds.lower_z = primitive.lower_z;
            rightBounds.upper_x = primitive.upper_x;
            rightBounds.upper_y = primitive.upper_y;
            rightBounds.upper_z = primitive.upper_z;
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
        builder.SetProgressMonitorFunction((n) =>
        {
            Console.WriteLine($"monitor {n}");
            return true;
        });
        builder.Build();
        ref Node res = ref builder.Result.Ref<Node>();
        Assert.AreEqual(minX, Math.Min(res.LeftBound.lower_x, res.RightBound.lower_x));
        Assert.AreEqual(minY, Math.Min(res.LeftBound.lower_y, res.RightBound.lower_y));
        Assert.AreEqual(minZ, Math.Min(res.LeftBound.lower_z, res.RightBound.lower_z));
        Assert.AreEqual(maxX, Math.Max(res.LeftBound.upper_x, res.RightBound.upper_x));
        Assert.AreEqual(maxY, Math.Max(res.LeftBound.upper_y, res.RightBound.upper_y));
        Assert.AreEqual(maxZ, Math.Max(res.LeftBound.upper_z, res.RightBound.upper_z));
    }

    [TestMethod]
    public void Generic()
    {
        const int N = 2300000;
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
        device.SetErrorFunction((code, str) =>
        {
            Console.WriteLine($"error {code}, {str}");
            Assert.Fail();
        });
        using EmbreeBuilder<Node, Node> builder = new(device);
        builder.SetBuildQuality(RTCBuildQuality.RTC_BUILD_QUALITY_HIGH);
        builder.SetMaxDepth(1024);
        builder.SetMaxLeafSize(1);
        builder.SetBuildPrimitive(prims);
        builder.SetCreateNodeFunction((allocator, childCount) =>
        {
            ref Node node = ref allocator.Allocate<Node>(32);
            node.IsLeaf = false;
            return ref node;
        });
        builder.SetSetNodeChildrenFunction((ref Node n, NativeMemoryView<Ref<Node>> children) =>
        {
            n.LeftChild = children[0];
            n.RightChild = children[1];
        });
        builder.SetSetNodeBoundsFunction((ref Node n, NativeMemoryView<Ref<RTCBounds>> bounds) =>
        {
            n.LeftBound = bounds[0].Value;
            n.RightBound = bounds[1].Value;
        });
        builder.SetCreateLeafFunction((allocator, prims) =>
        {
            ref Node node = ref allocator.Allocate<Node>(32);
            node.IsLeaf = true;
            return ref node;
        });
        builder.SetSplitPrimitiveFunction((ref readonly RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds) =>
        {
            leftBounds.lower_x = primitive.lower_x;
            leftBounds.lower_y = primitive.lower_y;
            leftBounds.lower_z = primitive.lower_z;
            leftBounds.upper_x = primitive.upper_x;
            leftBounds.upper_y = primitive.upper_y;
            leftBounds.upper_z = primitive.upper_z;
            rightBounds.lower_x = primitive.lower_x;
            rightBounds.lower_y = primitive.lower_y;
            rightBounds.lower_z = primitive.lower_z;
            rightBounds.upper_x = primitive.upper_x;
            rightBounds.upper_y = primitive.upper_y;
            rightBounds.upper_z = primitive.upper_z;
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
        builder.SetProgressMonitorFunction((n) =>
        {
            Console.WriteLine($"monitor {n}");
            return true;
        });
        builder.Build();
        ref Node res = ref builder.Result;
        Assert.AreEqual(minX, Math.Min(res.LeftBound.lower_x, res.RightBound.lower_x));
        Assert.AreEqual(minY, Math.Min(res.LeftBound.lower_y, res.RightBound.lower_y));
        Assert.AreEqual(minZ, Math.Min(res.LeftBound.lower_z, res.RightBound.lower_z));
        Assert.AreEqual(maxX, Math.Max(res.LeftBound.upper_x, res.RightBound.upper_x));
        Assert.AreEqual(maxY, Math.Max(res.LeftBound.upper_y, res.RightBound.upper_y));
        Assert.AreEqual(maxZ, Math.Max(res.LeftBound.upper_z, res.RightBound.upper_z));
    }
}
