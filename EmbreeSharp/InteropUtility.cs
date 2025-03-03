using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public static class InteropUtility
    {
        /// <summary>
        /// C-style string length
        /// </summary>
        public static unsafe nuint Strlen(byte* s)
        {
            byte* sc;
            for (sc = s; *sc != '\0'; ++sc) ;
            return new nuint((ulong)(sc - s));
        }

        /// <summary>
        /// Hepler function to alloc memory aligned on stack
        /// </summary>
        public static unsafe ref T StackAllocAligned<T>(Span<byte> stack, nuint alignment) where T : unmanaged
        {
            return ref Unsafe.AsRef<T>((void*)(((nint)Unsafe.AsPointer(ref MemoryMarshal.GetReference(stack)) + ((nint)alignment - 1)) & ~(nint)(alignment - 1)));
        }

        public static Matrix4x4 RTCFloat4x4ToMatrix4x4ColumnMajor(ReadOnlySpan<float> mat)
        {
            // C# matrix is row-major
            return new Matrix4x4(
                mat[0], mat[4], mat[8], mat[12],
                mat[1], mat[5], mat[9], mat[13],
                mat[2], mat[6], mat[10], mat[14],
                mat[3], mat[7], mat[11], mat[15]);
        }

        public static void Matrix4x4ToRTCFloat4x4ColumnMajor(Matrix4x4 mat, Span<float> v)
        {
            Matrix4x4 column = Matrix4x4.Transpose(mat);
            v[0] = column.M11;
            v[1] = column.M12;
            v[2] = column.M13;
            v[3] = column.M14;

            v[4] = column.M21;
            v[5] = column.M22;
            v[6] = column.M23;
            v[7] = column.M24;

            v[8] = column.M31;
            v[9] = column.M32;
            v[10] = column.M33;
            v[11] = column.M34;

            v[12] = column.M41;
            v[13] = column.M42;
            v[14] = column.M43;
            v[15] = column.M44;
        }
    }
}
