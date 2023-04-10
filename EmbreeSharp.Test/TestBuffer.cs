namespace EmbreeSharp.Test
{
    [TestClass]
    public class TestBuffer
    {
        [TestMethod]
        public void ManagedBuffer()
        {
            const uint cnt = 16;

            using RtcDevice device = new();
            int[] arr = new int[cnt];
            for (int i = 0; i < cnt; i++)
            {
                arr[i] = Random.Shared.Next();
            }
            using ManagedRtcSharedBuffer<int> buffer = device.NewSharedBuffer(arr);
            RtcBufferView<int> v = buffer.AsTypedView();

            Assert.AreEqual(arr.LongLength, v.Length);

            for (int i = 0; i < cnt; i++)
            {
                Assert.AreEqual(arr[i], v[i]);
            }

            v[3] = 114514;
            Assert.AreEqual(114514, v[3]);
            Assert.AreEqual(114514, arr[3]);

            arr[4] = 1919810;
            Assert.AreEqual(1919810, v[4]);
            Assert.AreEqual(1919810, arr[4]);
        }
    }
}
