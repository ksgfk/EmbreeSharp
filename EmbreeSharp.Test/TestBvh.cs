using EmbreeSharp.Native;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Test
{
    [StructLayout(LayoutKind.Sequential, Pack = 16, Size = 80)]
    public struct Node
    {
        public RTCBounds LBound;
        public RTCBounds RBound;
        public BvhNodeRef LChild;
        public BvhNodeRef RChild;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 16, Size = 32)]
    public struct Leaf
    {
        public RTCBounds Bound;
    }

    [TestClass]
    public class TestBvh
    {
        [TestMethod]
        public void Simple()
        {
            using RtcDevice device = new();
            using var bvh = device.NewBvh<Node, Leaf>();
            const int n = 2300000;
            RTCBuildPrimitive[] prims = new RTCBuildPrimitive[n];
            Vector3 max = new(float.MinValue);
            Vector3 min = new(float.MaxValue);
            for (int i = 0; i < n; i++)
            {
                float x = (float)Random.Shared.NextDouble();
                float y = (float)Random.Shared.NextDouble();
                float z = (float)Random.Shared.NextDouble();
                Vector3 p1 = new Vector3(x, y, z) * 100 - new Vector3(50);
                Vector3 p2 = p1 + new Vector3(1);
                ref RTCBuildPrimitive prim = ref prims[i];
                prim.lower_x = p1.X;
                prim.lower_y = p1.Y;
                prim.lower_z = p1.Z;
                prim.upper_x = p2.X;
                prim.upper_y = p2.Y;
                prim.upper_z = p2.Z;
                prim.geomID = 0;
                prim.primID = (uint)i;
                max.X = MathF.Max(max.X, prim.upper_x);
                max.Y = MathF.Max(max.Y, prim.upper_y);
                max.Z = MathF.Max(max.Z, prim.upper_z);
                min.X = MathF.Min(min.X, prim.lower_x);
                min.Y = MathF.Min(min.Y, prim.lower_y);
                min.Z = MathF.Min(min.Z, prim.lower_z);
            }
            bvh.BuildQuality = RTCBuildQuality.RTC_BUILD_QUALITY_LOW;
            bvh.MaxDepth = 1024;
            bvh.CreateNode = (RTCThreadLocalAllocator allocator, uint childCount) =>
            {
                Assert.AreEqual(2u, childCount);
                ref Node node = ref RtcThreadLocalAllocator.Alloc<Node>(allocator, 16);
                return ref node;
            };
            bvh.SetNodeChildren = (ref Node node, ReadOnlySpan<BvhNodeRef> children) =>
            {
                Assert.AreEqual(2, children.Length);
                node.LChild = children[0];
                node.RChild = children[1];
            };
            bvh.SetNodeBounds = (ref Node node, ReadOnlySpan<BvhBoundRef> bounds) =>
            {
                Assert.AreEqual(2, bounds.Length);
                node.LBound = bounds[0].Bound;
                node.RBound = bounds[1].Bound;
            };
            bvh.CreateLeaf = (RTCThreadLocalAllocator allocator, ReadOnlySpan<RTCBuildPrimitive> primitives) =>
            {
                Assert.AreEqual(1, primitives.Length);
                ref Leaf leaf = ref RtcThreadLocalAllocator.Alloc<Leaf>(allocator, 16);
                ref readonly RTCBuildPrimitive prim = ref primitives[0];
                leaf.Bound = Unsafe.As<RTCBuildPrimitive, RTCBounds>(ref Unsafe.AsRef(in prim));
                return ref leaf;
            };
            bvh.SplitPrimitive = (in RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds) =>
            {
                Assert.IsTrue(dimension < 3);
                Assert.IsTrue(primitive.geomID == 0);
                leftBounds = Unsafe.As<RTCBuildPrimitive, RTCBounds>(ref Unsafe.AsRef(in primitive));
                rightBounds = Unsafe.As<RTCBuildPrimitive, RTCBounds>(ref Unsafe.AsRef(in primitive));
                Unsafe.Add(ref leftBounds.upper_x, dimension) = position;
                Unsafe.Add(ref rightBounds.lower_x, dimension) = position;
            };
            bvh.BuildProgress = (double n) =>
            {
                return true;
            };
            ref Node node = ref bvh.Build(prims);
            Vector3 resMax = new(float.MinValue);
            Vector3 resMin = new(float.MaxValue);
            resMax.X = MathF.Max(resMax.X, node.LBound.upper_x);
            resMax.Y = MathF.Max(resMax.Y, node.LBound.upper_y);
            resMax.Z = MathF.Max(resMax.Z, node.LBound.upper_z);
            resMin.X = MathF.Min(resMin.X, node.LBound.lower_x);
            resMin.Y = MathF.Min(resMin.Y, node.LBound.lower_y);
            resMin.Z = MathF.Min(resMin.Z, node.LBound.lower_z);
            resMax.X = MathF.Max(resMax.X, node.RBound.upper_x);
            resMax.Y = MathF.Max(resMax.Y, node.RBound.upper_y);
            resMax.Z = MathF.Max(resMax.Z, node.RBound.upper_z);
            resMin.X = MathF.Min(resMin.X, node.RBound.lower_x);
            resMin.Y = MathF.Min(resMin.Y, node.RBound.lower_y);
            resMin.Z = MathF.Min(resMin.Z, node.RBound.lower_z);
            const float delta = 0.000001f;
            Assert.AreEqual(max.X, resMax.X, delta);
            Assert.AreEqual(max.Y, resMax.Y, delta);
            Assert.AreEqual(max.Z, resMax.Z, delta);
            Assert.AreEqual(min.X, resMin.X, delta);
            Assert.AreEqual(min.Y, resMin.Y, delta);
            Assert.AreEqual(min.Z, resMin.Z, delta);
        }
    }
}
