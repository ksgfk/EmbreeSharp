using EmbreeSharp.Native;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp.Test
{
    [TestClass]
    public class TestOccluded
    {
        RTCDevice _device;
        RTCScene _scene;

        [TestInitialize]
        public void StartUp()
        {
            unsafe
            {
                string config = "verbose=3";
                byte[] bytes = Encoding.ASCII.GetBytes(config);
                fixed (byte* ptr = bytes)
                {
                    _device = rtcNewDevice(ptr);
                }
                rtcSetDeviceErrorFunction(_device, (void* userPtr, RTCError code, byte* str) =>
                {
                    var len = InteropUtility.Strlen(str);
                    var msg = new string(Encoding.ASCII.GetString(str, (int)len));
                    Debug.WriteLine($"[{code}] {msg}");
                }, null);
                rtcSetDeviceMemoryMonitorFunction(_device, (void* ptr, nint bytes, bool post) =>
                {
                    var str = (bytes > 0 ? "allocated" : "deallocated");
                    Debug.WriteLine($"[MEMORY] {Math.Abs((int)bytes)} bytes {str}");
                    return true;

                }, null);

            }
            _scene = rtcNewScene(_device);
            unsafe
            {
                rtcSetSceneProgressMonitorFunction(_scene, (void* ptr, double n) =>
                {

                    Debug.WriteLine($"[PROGRESS] {n * 100.0f}");
                    return true;
                }, null);
            }

            var geo = rtcNewGeometry(_device, RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);
            float[] vertices = [
                -1.0f, -1.0f,  1.0f,
                1.0f, -1.0f,  1.0f,
                1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,

                -1.0f, -1.0f, -1.0f,
                1.0f, -1.0f, -1.0f,
                1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f
            ];
            uint[] indices =
            [
                0,1,2,
                2,3,0,

                1,5,6,
                6,2,1,

                7,6,5,
                5,4,7,

                4,0,3,
                3,7,4,

                4,5,1,
                1,0,4,

                3,2,6,
                6,7,3
            ];
            unsafe
            {
                fixed (float* dataPtr = vertices)
                {
                    void* buffer = rtcSetNewGeometryBuffer(geo, RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, sizeof(float) * 3, (nuint)vertices.Length / 3);
                    Unsafe.CopyBlock(buffer, dataPtr, (uint)(sizeof(float) * vertices.Length));
                }
            }
            unsafe
            {
                fixed (uint* dataPtr = indices)
                {
                    void* buffer = rtcSetNewGeometryBuffer(geo, RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, sizeof(uint) * 3, (nuint)indices.Length / 3);
                    Unsafe.CopyBlock(buffer, dataPtr, (uint)(sizeof(uint) * indices.Length));
                }
            }
            rtcSetGeometryMask(geo, 0xFFFFFFFF);
            rtcCommitGeometry(geo);
            rtcAttachGeometry(_scene, geo);
            rtcSetSceneBuildQuality(_scene, RTCBuildQuality.RTC_BUILD_QUALITY_HIGH);
            rtcCommitScene(_scene);
        }

        [TestMethod]
        public void Occluded1()
        {
            unsafe
            {
                Span<byte> stack = stackalloc byte[sizeof(RTCRay) + RTCRay.Alignment];
                ref RTCRay ray = ref InteropUtility.StackAllocAligned<RTCRay>(stack, RTCRay.Alignment);
                ray.dir_x = 0.0f;
                ray.dir_y = 1.0f;
                ray.dir_z = 0.0f;
                ray.org_x = 0.0f;
                ray.org_y = -2.0f;
                ray.org_z = 0.0f;
                ray.tfar = float.PositiveInfinity;
                ray.tnear = 0.0f;
                ray.mask = 0xFFFFFFFF;
                rtcOccluded1(_scene, (RTCRay*)Unsafe.AsPointer(ref ray), null);
            }
        }
    }
}
