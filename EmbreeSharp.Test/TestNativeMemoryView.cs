using System.Runtime.InteropServices;

namespace EmbreeSharp.Test
{
    [TestClass]
    public unsafe class TestNativeMemoryView
    {
        private int* _data;
        private nuint _length;

        [TestInitialize]
        public void SetUp()
        {
            const int cnt = 32;
            _data = (int*)NativeMemory.AlignedAlloc(sizeof(int) * cnt, 16);
            _length = cnt;

            _data[0] = 233;
            _data[16] = 666;
            _data[11] = 4;
            _data[4] = 514;
        }

        [TestCleanup]
        public void ClearUp()
        {
            NativeMemory.AlignedFree(_data);
        }

        [TestMethod]
        public void TestBasic()
        {
            NativeMemoryView<byte> raw = new(_data, sizeof(int) * _length);
            Assert.AreEqual(sizeof(int) * _length, raw.Length);
            Assert.AreEqual(raw.ByteCount, raw.Length);
            NativeMemoryView<int> data = raw.Cast<byte, int>();
            Assert.AreEqual(_length, data.Length);
            Assert.AreEqual(raw.ByteCount, data.ByteCount);

            Assert.AreEqual(_data[0], data[0]);
            Assert.AreEqual(_data[16], data[16]);
            Assert.AreEqual(_data[11], data[11]);
            Assert.AreEqual(_data[4], data[4]);
        }

        [TestMethod]
        public void TestEnumerator()
        {
            NativeMemoryView<int> data = new(_data, _length);
            int idx = 0;
            foreach (int i in data)
            {
                Assert.AreEqual(_data[idx], i);
                idx++;
            }
            Assert.AreEqual(_length, (nuint)idx);
        }

        [TestMethod]
        public void TestCopySpan()
        {
            NativeMemoryView<int> data = new(_data, _length);
            int[] test = new int[_length];
            Assert.AreEqual((nuint)test.Length, data.Length);
            Random rand = new();
            foreach (ref int i in test.AsSpan())
            {
                i = rand.Next();
            }
            data.CopyFrom(test);
            for (nuint i = 0; i < _length; i++)
            {
                Assert.AreEqual(test[i], data[i]);
            }
            {
                int[] exce = new int[_length + 1];
                try
                {
                    data.CopyFrom(exce);
                }
                catch (Exception ex)
                {
                    if (!typeof(ArgumentOutOfRangeException).Equals(ex.GetType()))
                    {
                        Assert.Fail();
                    }
                    return;
                }
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCopy()
        {
            NativeMemoryView<int> src = new(_data, _length);
            Random rand = new();
            foreach (ref int i in src)
            {
                i = rand.Next();
            }

            int[] dstData = new int[_length];
            fixed (int* dstPtr = dstData)
            {
                NativeMemoryView<int> dst = new(dstPtr, _length);
                src.CopyTo(dst);

                for (nuint i = 0; i < _length; i++)
                {
                    Assert.AreEqual(src[i], dst[i]);
                }
                for (nuint i = 0; i < _length; i++)
                {
                    Assert.AreEqual(src[i], dstData[i]);
                }
            }

            {
                int[] expe = new int[8];
                fixed (int* dstPtr = dstData)
                {
                    NativeMemoryView<int> dst = new(dstPtr, (nuint)expe.Length);
                    bool isSucc = src.TryCopyTo(dst);
                    Assert.IsFalse(isSucc);
                    try
                    {
                        src.CopyTo(dst);
                    }
                    catch (Exception ex)
                    {
                        if (!typeof(ArgumentOutOfRangeException).Equals(ex.GetType()))
                        {
                            Assert.Fail();
                        }
                        return;
                    }
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void TestAsSpan()
        {
            Random rand = new();
            for (nuint i = 0; i < _length; i++)
            {
                _data[i] = rand.Next();
            }

            NativeMemoryView<int> src = new(_data, _length);
            {
                Span<int> sp = src.AsSpan();
                Assert.AreEqual(sp.Length, (int)_length);
                for (nuint i = 0; i < _length; i++)
                {
                    Assert.AreEqual(_data[i], sp[(int)i]);
                }
            }
            {
                Span<int> sp = src.AsSpan(4);
                Assert.AreEqual(sp.Length, (int)_length - 4);
                for (int i = 0; i < sp.Length; i++)
                {
                    Assert.AreEqual(_data[i + 4], sp[i]);
                }
            }
            {
                Span<int> sp = src.AsSpan(4, 1);
                Assert.AreEqual(sp.Length, 1);
                Assert.AreEqual(_data[4], sp[0]);
            }
            {
                NativeMemoryView<int> t = new(_data, unchecked(uint.MaxValue + (nuint)1));
                try
                {
                    t.AsSpan();
                }
                catch (Exception ex)
                {
                    if (!typeof(InvalidOperationException).Equals(ex.GetType()))
                    {
                        Assert.Fail();
                    }
                    return;
                }
                Assert.Fail();
            }
        }
    }
}
