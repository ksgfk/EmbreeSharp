using EmbreeSharp.Native;
using System.Numerics;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Test
{
    [TestClass]
    public class TestDevice
    {
        [TestMethod]
        public void TestConfig()
        {
            {
                RtcDeviceConfig cfg = new();
                Assert.AreEqual("", cfg.ToString());
            }
            {
                RtcDeviceConfig cfg = new()
                {
                    Threads = 1,
                    ISA = RtcISA.Avx
                };
                Assert.AreEqual("threads=1,isa=avx", cfg.ToString());
            }
            {
                RtcDeviceConfig cfg = new()
                {
                    Threads = 1,
                    ISA = RtcISA.Avx,
                    Verbose = -1
                };
                Assert.AreEqual("threads=1,isa=avx,verbose=0", cfg.ToString());
            }
            {
                RtcDeviceConfig cfg = new()
                {
                    Threads = 32,
                    UserThreads = 32,
                    SetAffinity = true,
                    StartThreads = false,
                    ISA = RtcISA.Avx,
                    MaxISA = RtcISA.Avx512,
                    HugePages = false,
                    EnableSeLockMemoryPrivilege = false,
                    Verbose = 5,
                    FrequencyLevel = RtcFrequencyLevel.Simd512
                };
                Assert.AreEqual("threads=32,user_threads=32,set_affinity=1,start_threads=0,isa=avx,max_isa=avx512,hugepages=0,enable_selockmemoryprivilege=0,verbose=3,frequency_level=simd512", cfg.ToString());
            }
        }

        [TestMethod]
        public void SimpleWrapper()
        {
            using RtcDevice device = new();
            using RtcScene scene = device.NewScene();
            {
                using RtcGeometry geom = device.NewGeometry(RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);
                Span<Vector3> vertices = geom.SetNewBuffer<Vector3>(RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, 4);
                Span<byte> i = geom.SetNewBuffer(RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, sizeof(uint) * 3, 2);
                Assert.AreEqual(4, vertices.Length);
                Assert.AreEqual(24, i.Length);
                Span<uint> indices = MemoryMarshal.Cast<byte, uint>(i);
                vertices[0].X = -1f; vertices[0].Y = 0; vertices[0].Z = 1;
                vertices[1].X = -1f; vertices[1].Y = 0; vertices[1].Z = -1;
                vertices[2].X = 1f; vertices[2].Y = 0; vertices[2].Z = 1;
                vertices[3].X = 1f; vertices[3].Y = 0; vertices[3].Z = -1;
                indices[0] = 0; indices[1] = 1; indices[2] = 2;
                indices[3] = 1; indices[4] = 3; indices[5] = 2;
                geom.Commit();
                scene.AttachGeometry(geom);
            }
            scene.Commit();
            RTCRayHit rayhit = RayUtility.InitRayHit(0, 1, 0, 0, -1, 0);
            scene.Intersect(in rayhit);
            Assert.IsTrue(RayUtility.IsHit(in rayhit));
        }
    }
}
