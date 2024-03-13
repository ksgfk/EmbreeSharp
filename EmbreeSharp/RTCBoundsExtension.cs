using EmbreeSharp.Native;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public static class RTCBoundsExtension
    {
        public static Vector3 GetLowerVector3(ref readonly this RTCBounds bounds)
        {
            return new Vector3(bounds.lower_x, bounds.lower_y, bounds.lower_z);
        }

        public static Vector3 GetUpperVector3(ref readonly this RTCBounds bounds)
        {
            return new Vector3(bounds.upper_x, bounds.upper_y, bounds.upper_z);
        }

        public static void SetLowerVector3(ref this RTCBounds bounds, Vector3 v)
        {
            bounds.lower_x = v.X;
            bounds.lower_y = v.Y;
            bounds.lower_z = v.Z;
        }

        public static void SetUpperVector3(ref this RTCBounds bounds, Vector3 v)
        {
            bounds.upper_x = v.X;
            bounds.upper_y = v.Y;
            bounds.upper_z = v.Z;
        }

        public static float GetLowerDimension(ref readonly this RTCBounds bounds, int dimension)
        {
            if (((uint)dimension) > 2)
            {
                ThrowUtility.ArgumentOutOfRange(nameof(dimension));
            }
            return Unsafe.Add(ref Unsafe.AsRef(in bounds.lower_x), dimension);
        }

        public static float GetUpperDimension(ref readonly this RTCBounds bounds, int dimension)
        {
            if (((uint)dimension) > 2)
            {
                ThrowUtility.ArgumentOutOfRange(nameof(dimension));
            }
            return Unsafe.Add(ref Unsafe.AsRef(in bounds.upper_x), dimension);
        }

        public static void SetLowerDimension(ref this RTCBounds bounds, int dimension, float value)
        {
            if (((uint)dimension) > 2)
            {
                ThrowUtility.ArgumentOutOfRange(nameof(dimension));
            }
            Unsafe.Add(ref bounds.lower_x, dimension) = value;
        }

        public static void SetUpperDimension(ref this RTCBounds bounds, int dimension, float value)
        {
            if (((uint)dimension) > 2)
            {
                ThrowUtility.ArgumentOutOfRange(nameof(dimension));
            }
            Unsafe.Add(ref bounds.upper_x, dimension) = value;
        }

        public static float SurfaceArea(ref readonly this RTCBounds bounds)
        {
            Vector3 min = bounds.GetLowerVector3();
            Vector3 max = bounds.GetUpperVector3();
            Vector3 d = max - min;
            return 2 * (d.X * d.Y + d.X * d.Z + d.Y * d.Z);
        }

        public static RTCBounds Union(ref readonly this RTCBounds bounds, ref readonly RTCBounds target)
        {
            Vector3 min = Vector3.Min(bounds.GetLowerVector3(), target.GetLowerVector3());
            Vector3 max = Vector3.Max(bounds.GetUpperVector3(), target.GetUpperVector3());
            RTCBounds result = new();
            result.SetLowerVector3(min);
            result.SetUpperVector3(max);
            return result;
        }
    }
}
