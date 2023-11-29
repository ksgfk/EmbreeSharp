using EmbreeSharp.Native;

namespace EmbreeSharp.Test
{
    [TestClass]
    public class TestSimpleWrapper
    {
        [TestMethod]
        public void TestWrapper()
        {
            using RtcDevice device = new();
            device.SetErrorFunction((code, str) =>
            {
                Console.WriteLine($"error {code}, {str}");
                Assert.Fail();
            });
            using RtcScene scene = new(device);
            using RtcGeometry geo = new(device, RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);

            NativeMemoryView<byte> verticesData = geo.SetNewBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, (nuint)sizeof(float) * 3, 4);
            NativeMemoryView<float> vertices = verticesData.Cast<byte, float>();
            vertices[0] = -1f; vertices[1] = 0; vertices[2] = 1;
            vertices[3] = -1f; vertices[4] = 0; vertices[5] = -1;
            vertices[6] = 1f; vertices[7] = 0; vertices[8] = 1;
            vertices[9] = 1f; vertices[10] = 0; vertices[11] = -1;

            NativeMemoryView<byte> indicesData = geo.SetNewBuffer(RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, (nuint)sizeof(uint) * 3, 2);
            NativeMemoryView<uint> indices = indicesData.Cast<byte, uint>();
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 1; indices[4] = 3; indices[5] = 2;

            geo.Commit();
            scene.AttachGeometry(geo);
            scene.Commit();

            {
                RTCRayHit rayHit = RtcRayUtility.CreateRay(0, 1, 0, 0, -1, 0);
                scene.Intersect(ref rayHit);
                Assert.IsTrue(rayHit.IsHit());
                Assert.AreEqual(geo.Id, rayHit.hit.geomID);
            }
            {
                RTCRayHit rayHit = RtcRayUtility.CreateRay(0, 1, 0, 1, 0, 0);
                scene.Intersect(ref rayHit);
                Assert.IsFalse(rayHit.IsHit());
            }
        }
    }
}
