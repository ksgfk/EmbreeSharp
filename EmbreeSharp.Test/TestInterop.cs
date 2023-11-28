using System.Runtime.CompilerServices;
using EmbreeSharp.Native;

namespace EmbreeSharp.Test
{
    [TestClass]
    public class TestInterop
    {
        [TestMethod]
        public unsafe void TestStackAllocAligned()
        {
            Span<byte> stack = stackalloc byte[sizeof(RTCRayHit16) + RTCRayHit16.Alignment];
            ref RTCRayHit16 rayHit = ref InteropUtility.StackAllocAligned<RTCRayHit16>(stack, RTCRayHit16.Alignment);
            void* ptr = Unsafe.AsPointer(ref rayHit);
            nint managedPtr = new(ptr);
            Assert.AreEqual(managedPtr.ToInt64() % RTCRayHit16.Alignment, 0);
        }
    }
}
