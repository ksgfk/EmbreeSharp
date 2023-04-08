using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public partial struct RTCQuaternionDecomposition
{
    public float scale_x;

    public float scale_y;

    public float scale_z;

    public float skew_xy;

    public float skew_xz;

    public float skew_yz;

    public float shift_x;

    public float shift_y;

    public float shift_z;

    public float quaternion_r;

    public float quaternion_i;

    public float quaternion_j;

    public float quaternion_k;

    public float translation_x;

    public float translation_y;

    public float translation_z;
}
