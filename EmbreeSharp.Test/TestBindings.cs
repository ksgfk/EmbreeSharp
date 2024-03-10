using EmbreeSharp.Native;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp.Test
{
    [TestClass]
    public class TestBindings
    {
        private static unsafe void ErrorFunctionImpl(void* userPtr, RTCError code, byte* str)
        {
            nuint len = InteropUtility.Strlen(str);
            int byteCnt = len <= int.MaxValue ? (int)len : int.MaxValue;
            var mgrStr = Encoding.UTF8.GetString(str, byteCnt);
            Console.WriteLine($"error {code}, {mgrStr}");
        }

        [TestMethod]
        public unsafe void SimpleIntersect()
        {
            RTCDevice device = rtcNewDevice(null);
            RTCErrorFunction errFunc = ErrorFunctionImpl;
            rtcSetDeviceErrorFunction(device, errFunc, null);
            RTCScene scene = rtcNewScene(device);
            RTCGeometry geo = rtcNewGeometry(device, RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);
            float* vertices = (float*)rtcSetNewGeometryBuffer(geo, RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, (nuint)sizeof(float) * 3, 4);
            uint* indices = (uint*)rtcSetNewGeometryBuffer(geo, RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, (nuint)sizeof(uint) * 3, 2);
            vertices[0] = -1f; vertices[1] = 0; vertices[2] = 1;
            vertices[3] = -1f; vertices[4] = 0; vertices[5] = -1;
            vertices[6] = 1f; vertices[7] = 0; vertices[8] = 1;
            vertices[9] = 1f; vertices[10] = 0; vertices[11] = -1;
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 1; indices[4] = 3; indices[5] = 2;
            rtcCommitGeometry(geo);
            uint geoId = rtcAttachGeometry(scene, geo);
            rtcCommitScene(scene);

            {
                Span<byte> stack = stackalloc byte[sizeof(RTCRayHit) + RTCRayHit.Alignment];
                ref RTCRayHit rayhit = ref InteropUtility.StackAllocAligned<RTCRayHit>(stack, RTCRayHit.Alignment);
                rayhit.ray.org_x = 0;
                rayhit.ray.org_y = 1;
                rayhit.ray.org_z = 0;
                rayhit.ray.dir_x = 0;
                rayhit.ray.dir_y = -1;
                rayhit.ray.dir_z = 0;
                rayhit.ray.time = 0;
                rayhit.ray.tnear = 0;
                rayhit.ray.tfar = float.PositiveInfinity;
                rayhit.ray.mask = uint.MaxValue;
                rayhit.ray.flags = 0;
                rayhit.hit.geomID = RTC_INVALID_GEOMETRY_ID;
                rayhit.hit.instID[0] = RTC_INVALID_GEOMETRY_ID;
                rtcIntersect1(scene, (RTCRayHit*)Unsafe.AsPointer(ref rayhit));
                Assert.IsTrue(rayhit.hit.geomID != RTC_INVALID_GEOMETRY_ID);
                Assert.AreEqual(geoId, rayhit.hit.geomID);
            }
            {
                Span<byte> stack = stackalloc byte[sizeof(RTCRayHit) + RTCRayHit.Alignment];
                ref RTCRayHit rayhit = ref InteropUtility.StackAllocAligned<RTCRayHit>(stack, RTCRayHit.Alignment);
                rayhit.ray.org_x = 0;
                rayhit.ray.org_y = 1;
                rayhit.ray.org_z = 0;
                rayhit.ray.dir_x = 1;
                rayhit.ray.dir_y = 0;
                rayhit.ray.dir_z = 0;
                rayhit.ray.time = 0;
                rayhit.ray.tnear = 0;
                rayhit.ray.tfar = float.PositiveInfinity;
                rayhit.ray.mask = uint.MaxValue;
                rayhit.ray.flags = 0;
                rayhit.hit.geomID = RTC_INVALID_GEOMETRY_ID;
                rayhit.hit.instID[0] = RTC_INVALID_GEOMETRY_ID;
                rtcIntersect1(scene, (RTCRayHit*)Unsafe.AsPointer(ref rayhit));
                Assert.IsTrue(rayhit.hit.geomID == RTC_INVALID_GEOMETRY_ID);
            }
        }
    }
}
