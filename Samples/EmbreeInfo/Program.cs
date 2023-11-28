using EmbreeSharp;

internal class Program
{
    private static unsafe void Main(string[] args)
    {
        string config = "verbose=3";
        using RtcDevice _ = new(config);
    }
}
