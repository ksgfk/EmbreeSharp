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
            Assert.AreEqual(32, sizeof(RTCBuildPrimitive));
            Assert.AreEqual(136, sizeof(RTCBuildArguments));
        }

        // rtcore_common.h
        {
            Assert.AreEqual(32, sizeof(RTCBounds));
            Assert.AreEqual(64, sizeof(RTCLinearBounds));
            Assert.AreEqual(48, sizeof(RTCFilterFunctionNArguments));
            Assert.AreEqual(8, sizeof(RTCRayQueryContext));
            Assert.AreEqual(32, sizeof(RTCPointQuery));
            Assert.AreEqual(80, sizeof(RTCPointQuery4));
            Assert.AreEqual(160, sizeof(RTCPointQuery8));
            Assert.AreEqual(320, sizeof(RTCPointQuery16));
            Assert.AreEqual(144, sizeof(RTCPointQueryContext));
            Assert.AreEqual(48, sizeof(RTCPointQueryFunctionArguments));
        }

        // rtcore_geometry.h
        {
            Assert.AreEqual(24, sizeof(RTCBoundsFunctionArguments));
            Assert.AreEqual(48, sizeof(RTCIntersectFunctionNArguments));
            Assert.AreEqual(48, sizeof(RTCOccludedFunctionNArguments));
            Assert.AreEqual(96, sizeof(RTCDisplacementFunctionNArguments));
            Assert.AreEqual(88, sizeof(RTCInterpolateArguments));
            Assert.AreEqual(112, sizeof(RTCInterpolateNArguments));
            Assert.AreEqual(12, sizeof(RTCGrid));
        }

        // rtcore_quaternion.h
        {
            Assert.AreEqual(64, sizeof(RTCQuaternionDecomposition));
        }

        //rtcore_ray.h
        {
            Assert.AreEqual(48, sizeof(RTCRay));
            Assert.AreEqual(48, sizeof(RTCHit));
            Assert.AreEqual(96, sizeof(RTCRayHit));
            Assert.AreEqual(192, sizeof(RTCRay4));
            Assert.AreEqual(144, sizeof(RTCHit4));
            Assert.AreEqual(336, sizeof(RTCRayHit4));
            Assert.AreEqual(384, sizeof(RTCRay8));
            Assert.AreEqual(288, sizeof(RTCHit8));
            Assert.AreEqual(672, sizeof(RTCRayHit8));
            Assert.AreEqual(768, sizeof(RTCRay16));
            Assert.AreEqual(576, sizeof(RTCHit16));
            Assert.AreEqual(1344, sizeof(RTCRayHit16));
        }

        //rtcore_scene.h
        {
            Assert.AreEqual(32, sizeof(RTCIntersectArguments));
            Assert.AreEqual(32, sizeof(RTCOccludedArguments));
            Assert.AreEqual(16, sizeof(RTCCollision));
        }
    }
}
