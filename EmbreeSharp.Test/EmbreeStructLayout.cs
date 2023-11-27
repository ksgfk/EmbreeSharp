using EmbreeSharp.Native;

namespace EmbreeSharp.Test;

[TestClass]
public class EmbreeStructLayout
{
    [TestMethod]
    public unsafe void StructSize()
    {
        // rtcore_builder.h
        {
            Assert.AreEqual(sizeof(RTCBuildPrimitive), 32);
            Assert.AreEqual(sizeof(RTCBuildArguments), 136);
        }

        // rtcore_common.h
        {
            Assert.AreEqual(sizeof(RTCBounds), 32);
            Assert.AreEqual(sizeof(RTCLinearBounds), 64);
            Assert.AreEqual(sizeof(RTCFilterFunctionNArguments), 48);
            Assert.AreEqual(sizeof(RTCRayQueryContext), 8);
            Assert.AreEqual(sizeof(RTCPointQuery), 32);
            Assert.AreEqual(sizeof(RTCPointQuery4), 80);
            Assert.AreEqual(sizeof(RTCPointQuery8), 160);
            Assert.AreEqual(sizeof(RTCPointQuery16), 320);
            Assert.AreEqual(sizeof(RTCPointQueryContext), 144);
            Assert.AreEqual(sizeof(RTCPointQueryFunctionArguments), 48);
        }

        // rtcore_geometry.h
        {
            Assert.AreEqual(sizeof(RTCBoundsFunctionArguments), 24);
            Assert.AreEqual(sizeof(RTCIntersectFunctionNArguments), 48);
            Assert.AreEqual(sizeof(RTCOccludedFunctionNArguments), 48);
            Assert.AreEqual(sizeof(RTCDisplacementFunctionNArguments), 96);
            Assert.AreEqual(sizeof(RTCInterpolateArguments), 88);
            Assert.AreEqual(sizeof(RTCInterpolateNArguments), 112);
            Assert.AreEqual(sizeof(RTCGrid), 12);
        }

        // rtcore_quaternion.h
        {
            Assert.AreEqual(sizeof(RTCQuaternionDecomposition), 64);
        }

        //rtcore_ray.h
        {
            Assert.AreEqual(sizeof(RTCRay), 48);
            Assert.AreEqual(sizeof(RTCHit), 48);
            Assert.AreEqual(sizeof(RTCRayHit), 96);
            Assert.AreEqual(sizeof(RTCRay4), 192);
            Assert.AreEqual(sizeof(RTCHit4), 144);
            Assert.AreEqual(sizeof(RTCRayHit4), 336);
            Assert.AreEqual(sizeof(RTCRay8), 384);
            Assert.AreEqual(sizeof(RTCHit8), 288);
            Assert.AreEqual(sizeof(RTCRayHit8), 672);
            Assert.AreEqual(sizeof(RTCRay16), 768);
            Assert.AreEqual(sizeof(RTCHit16), 576);
            Assert.AreEqual(sizeof(RTCRayHit16), 1344);
        }

        //rtcore_scene.h
        {
            Assert.AreEqual(sizeof(RTCIntersectArguments), 32);
            Assert.AreEqual(sizeof(RTCOccludedArguments), 32);
            Assert.AreEqual(sizeof(RTCCollision), 16);
        }
    }
}
