using EmbreeSharp.Native;

namespace EmbreeSharp.Test;

[TestClass]
public class EmbreeStructLayout
{
    [TestMethod]
    public unsafe void StructSize()
    {
        Assert.AreEqual(sizeof(RTCBounds), 32);
    }
}
