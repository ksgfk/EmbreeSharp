using EmbreeSharp.Native;
using System.Text;

internal class Program
{
    private static unsafe void Main(string[] args)
    {
        string config = "verbose=1";
        var bytes = Encoding.UTF8.GetBytes(config);
        fixed (byte* ptr = bytes)
        {
            RTCDevice device = GlobalFunctions.rtcNewDevice(ptr);
            GlobalFunctions.rtcReleaseDevice(device);
        }
    }
}
