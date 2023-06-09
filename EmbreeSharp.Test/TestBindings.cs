using EmbreeSharp.Native;
using System.Numerics;
using System.Runtime.InteropServices;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp.Test
{
    [TestClass]
    public unsafe class TestBindings
    {
        [TestMethod]
        public void TypeSize()
        {
            //assume that the runtime environment is 64 bit
            Assert.AreEqual(8, sizeof(nuint));

            Assert.AreEqual(8, sizeof(nuint));
            Assert.AreEqual(8, sizeof(RTCDevice));
            Assert.AreEqual(4, sizeof(RTCGeometryType));
            Assert.AreEqual(4, sizeof(RTCBufferType));
            Assert.AreEqual(4, sizeof(RTCFormat));
            Assert.AreEqual(48, sizeof(RTCRay));
            Assert.AreEqual(32, sizeof(RTCHit));
            Assert.AreEqual(80, sizeof(RTCRayHit));
            Assert.AreEqual(32, sizeof(RTCBounds));
            Assert.AreEqual(24, sizeof(RTCBoundsFunctionArguments));
            Assert.AreEqual(136, sizeof(RTCBuildArguments));
            Assert.AreEqual(4, sizeof(RTCBuildConstants));
            Assert.AreEqual(4, sizeof(RTCBuildFlags));
            Assert.AreEqual(32, sizeof(RTCBuildPrimitive));
            Assert.AreEqual(16, sizeof(RTCCollision));
            Assert.AreEqual(4, sizeof(RTCCurveFlags));
            Assert.AreEqual(4, sizeof(RTCDeviceProperty));
            Assert.AreEqual(96, sizeof(RTCDisplacementFunctionNArguments));
            Assert.AreEqual(4, sizeof(RTCError));
            Assert.AreEqual(4, sizeof(RTCFeatureFlags));
            Assert.AreEqual(48, sizeof(RTCFilterFunctionNArguments));
            Assert.AreEqual(4, sizeof(RTCFormat));
            Assert.AreEqual(4, sizeof(RTCGeometryType));
            Assert.AreEqual(12, sizeof(RTCGrid));
            Assert.AreEqual(512, sizeof(RTCHit16));
            Assert.AreEqual(128, sizeof(RTCHit4));
            Assert.AreEqual(256, sizeof(RTCHit8));
            Assert.AreEqual(88, sizeof(RTCInterpolateArguments));
            Assert.AreEqual(112, sizeof(RTCInterpolateNArguments));
            Assert.AreEqual(32, sizeof(RTCIntersectArguments));
            Assert.AreEqual(48, sizeof(RTCIntersectFunctionNArguments));
            Assert.AreEqual(64, sizeof(RTCLinearBounds));
            Assert.AreEqual(32, sizeof(RTCOccludedArguments));
            Assert.AreEqual(48, sizeof(RTCOccludedFunctionNArguments));
            Assert.AreEqual(32, sizeof(RTCPointQuery));
            Assert.AreEqual(320, sizeof(RTCPointQuery16));
            Assert.AreEqual(80, sizeof(RTCPointQuery4));
            Assert.AreEqual(160, sizeof(RTCPointQuery8));
            Assert.AreEqual(144, sizeof(RTCPointQueryContext));
            Assert.AreEqual(48, sizeof(RTCPointQueryFunctionArguments));
            Assert.AreEqual(64, sizeof(RTCQuaternionDecomposition));
            Assert.AreEqual(768, sizeof(RTCRay16));
            Assert.AreEqual(192, sizeof(RTCRay4));
            Assert.AreEqual(384, sizeof(RTCRay8));
            Assert.AreEqual(1280, sizeof(RTCRayHit16));
            Assert.AreEqual(320, sizeof(RTCRayHit4));
            Assert.AreEqual(640, sizeof(RTCRayHit8));
            Assert.AreEqual(4, sizeof(RTCRayQueryContext));
            Assert.AreEqual(4, sizeof(RTCRayQueryFlags));
            Assert.AreEqual(4, sizeof(RTCSceneFlags));
            Assert.AreEqual(4, sizeof(RTCSubdivisionMode));

            RTCRayHit rayhit = default;
            IntPtr headAddr = new(&rayhit);
            IntPtr hitAddr = new(&rayhit.hit);
            Assert.AreEqual(48, hitAddr.ToInt64() - headAddr.ToInt64());
        }

        [TestMethod]
        public void LibraryLoad()
        {
            RTCDevice d1 = rtcNewDevice(null);
            RTCDevice d2 = rtcNewDevice(null);
            Assert.AreNotEqual(d1.Ptr, d2.Ptr);
            rtcReleaseDevice(d1);
            rtcReleaseDevice(d2);
        }

        [TestMethod]
        public void SimpleIntersect()
        {
            RTCDevice device = rtcNewDevice(null);
            RTCScene scene = rtcNewScene(device);

            RTCGeometry geom = rtcNewGeometry(device, RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);
            void* v = rtcSetNewGeometryBuffer(geom, RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, (nuint)sizeof(Vector3), 4);
            void* i = rtcSetNewGeometryBuffer(geom, RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, 3 * sizeof(uint), 2);
            Span<Vector3> vertices = MemoryMarshal.Cast<byte, Vector3>(new Span<byte>(v, 48));
            Span<uint> indices = MemoryMarshal.Cast<byte, uint>(new Span<byte>(i, 24));
            vertices[0].X = -1f; vertices[0].Y = 0; vertices[0].Z = 1;
            vertices[1].X = -1f; vertices[1].Y = 0; vertices[1].Z = -1;
            vertices[2].X = 1f; vertices[2].Y = 0; vertices[2].Z = 1;
            vertices[3].X = 1f; vertices[3].Y = 0; vertices[3].Z = -1;
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 1; indices[4] = 3; indices[5] = 2;
            rtcCommitGeometry(geom);
            uint geomID = rtcAttachGeometry(scene, geom);
            rtcReleaseGeometry(geom);

            rtcCommitScene(scene);

            RTCRayHit rayhit = default;
            rayhit.ray.org_x = 0;
            rayhit.ray.org_y = 1;
            rayhit.ray.org_z = 0;
            rayhit.ray.dir_x = 0;
            rayhit.ray.dir_y = -1;
            rayhit.ray.dir_z = 0;
            rayhit.ray.tnear = 0;
            rayhit.ray.tfar = float.PositiveInfinity;
            rayhit.ray.mask = uint.MaxValue;
            rayhit.ray.flags = 0;
            rayhit.hit.geomID = RTC_INVALID_GEOMETRY_ID;
            rayhit.hit.instID[0] = RTC_INVALID_GEOMETRY_ID;

            rtcIntersect1(scene, &rayhit, null);
            Assert.AreNotEqual(RTC_INVALID_GEOMETRY_ID, rayhit.hit.geomID);

            rtcReleaseScene(scene);
            rtcReleaseDevice(device);
        }
    }
}
