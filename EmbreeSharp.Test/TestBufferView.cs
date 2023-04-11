using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Test
{
    [TestClass]
    public unsafe class TestBufferView
    {
        [TestMethod]
        public void Basic()
        {
            const uint cnt = 16;
            int* ptr = (int*)NativeMemory.Alloc(sizeof(int) * cnt);

            Span<int> a = new(ptr, (int)cnt);
            RtcBufferView<int> b = new(ref Unsafe.AsRef<int>(ptr), cnt);

            Assert.AreEqual(a.Length, b.Length);

            for (int i = 0; i < cnt; i++)
            {
                a[i] = Random.Shared.Next();
            }

            for (int i = 0; i < cnt; i++)
            {
                Assert.AreEqual(a[i], b[i]);
            }

            int t = 0;
            foreach (ref readonly var i in b)
            {
                Assert.AreEqual(a[t++], i);
            }
            Assert.AreEqual(a.Length, t);

            b[2] = 114514;
            Assert.AreEqual(114514, a[2]);

            NativeMemory.Free(ptr);
        }

        [TestMethod]
        public void Copy()
        {
            const uint cnt = 16;
            Span<int> r = new int[cnt];

            int* p1 = (int*)NativeMemory.Alloc(sizeof(int) * cnt);
            RtcBufferView<int> v1 = new(ref Unsafe.AsRef<int>(p1), cnt);
            for (int i = 0; i < cnt; i++)
            {
                v1[i] = Random.Shared.Next();
            }
            v1.CopyTo(r);

            for (int i = 0; i < cnt; i++)
            {
                Assert.AreEqual(p1[i], r[i]);
            }

            int* p2 = (int*)NativeMemory.Alloc(sizeof(int) * cnt);
            RtcBufferView<int> v2 = new(ref Unsafe.AsRef<int>(p2), cnt);
            v1.CopyTo(v2);

            for (int i = 0; i < cnt; i++)
            {
                Assert.AreEqual(p1[i], p2[i]);
            }

            NativeMemory.Free(p1);
            NativeMemory.Free(p2);
        }

        [TestMethod]
        public void Slice()
        {
            const uint cnt = 16;
            int[] r = new int[cnt];
            foreach (ref var i in r.AsSpan())
            {
                i = Random.Shared.Next();
            }
            Span<int> a = new(r);
            RtcBufferView<int> b = new(ref r[0], cnt);
            for (int i = 0; i < cnt; i++)
            {
                Assert.AreEqual(a[i], b[i]);
            }
            {
                Span<int> x = a[4..];
                RtcBufferView<int> y = b.Slice(4);
                Assert.AreEqual(x.Length, y.Length);
                for (int i = 0; i < x.Length; i++)
                {
                    Assert.AreEqual(x[i], y[i]);
                }
            }
            {
                Span<int> x = a.Slice(4, 4);
                RtcBufferView<int> y = b.Slice(4L, 4L);
                Assert.AreEqual(x.Length, y.Length);
                for (int i = 0; i < x.Length; i++)
                {
                    Assert.AreEqual(x[i], y[i]);
                }
            }
            {
                Span<int> x = a.Slice(4, 4);
                Span<int> y = b.Slice(4, 4);
                Assert.AreEqual(x.Length, y.Length);
                for (int i = 0; i < x.Length; i++)
                {
                    Assert.AreEqual(x[i], y[i]);
                }
            }
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Span<int> test = stackalloc int[1];
                RtcBufferView<int> z = new(ref test[0], long.MaxValue - 1);
                z.AsSpan();
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Span<int> test = stackalloc int[1];
                RtcBufferView<int> z = new(ref test[0], (long)int.MaxValue + 100);
                z.Slice(int.MaxValue, int.MaxValue);
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Span<int> test = stackalloc int[1];
                RtcBufferView<int> z = new(ref test[0], 1);
                z.Slice(1, 1);
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Span<int> test = stackalloc int[1];
                RtcBufferView<int> z = new(ref test[0], 1);
                z.Slice(-1);
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Span<int> test = stackalloc int[1];
                RtcBufferView<int> z = new(ref test[0], 1);
                z.Slice(0, 2);
            });
        }
    }
}
