using EmbreeSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        string config = "verbose=3";
        using EmbreeDevice _ = new(config);
    }
}
