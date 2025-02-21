namespace EmbreeSharp.Test
{
    [TestClass]
    public class TestMXCSR
    {
        [TestMethod]
        public void TestCall()
        {
            SseUtility.EmbreeMxcsrRegisterControl();
            using EmbreeDevice device = new();
            device.SetErrorFunction((code, str) =>
            {
                Console.WriteLine($"error {code}, {str}");
                Assert.Fail();
            });
        }
    }
}
