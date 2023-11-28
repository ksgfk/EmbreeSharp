using EmbreeSharp;
using EmbreeSharp.Native;

internal class Program
{
    private static unsafe void Main(string[] args)
    {
        string config = "verbose=1";
        RTCDevice device = RayTracingCore.NewDevice(config);
        RayTracingCore.ReleaseDevice(device);
    }
}
