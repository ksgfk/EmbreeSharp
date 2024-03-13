using EmbreeSharp;
using EmbreeSharp.Native;

internal class Program
{
    private static void Main(string[] args)
    {
        float[] data = new float[1024 * 1024 * 32];
        foreach (ref var i in data.AsSpan())
        {
            i = Random.Shared.NextSingle();
        }
        int[] intd = new int[4096];
        foreach (ref var i in intd.AsSpan())
        {
            i = Random.Shared.Next();
        }
        string config = "verbose=3";
        using EmbreeDevice device = new(config);
        DefaultSharedBufferAllocator allocator = new();
        for (int i = 0; i < 1000; i++)
        {
            //TestEmbreeSharedBuffer(device, allocator, data);
            //TestEmbreeSharedBufferForgetRelease(device, allocator, data);
        }

        for (int i = 0; i < 1000000; i++)
        {
            //TestGeo(device, allocator, data, intd);
            //TestGeoMultiSet(device, allocator, data, intd);
        }
    }

    static void TestEmbreeSharedBuffer(EmbreeDevice device, ISharedBufferAllocator allocator, float[] data)
    {
        using SharedBufferHandle handle = allocator.Allocate((nuint)data.Length * sizeof(float), 16);
        using EmbreeSharedBuffer buffer = new(device, handle);
        buffer.CopyFrom<float>(data.AsSpan());
        Console.WriteLine($"size={buffer.ByteSize}");
    }

    static void TestEmbreeSharedBufferForgetRelease(EmbreeDevice device, ISharedBufferAllocator allocator, float[] data)
    {
        SharedBufferHandle handle = allocator.Allocate((nuint)data.Length * sizeof(float), 16);
        EmbreeSharedBuffer buffer = new(device, handle);
        buffer.CopyFrom<float>(data.AsSpan());
        Console.WriteLine($"size={buffer.ByteSize}");
    }

    static void TestGeo(EmbreeDevice device, ISharedBufferAllocator allocator, float[] data, int[] intd)
    {
        using EmbreeGeometry geo = new(device, RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);
        SharedBufferHandle vert = allocator.Allocate((nuint)32 * 4 * 4 * sizeof(float), 16);
        vert.Buffer.View.Cast<byte, float>().CopyFrom(data.AsSpan()[..(32 * 4 * 4)]);
        SharedBufferHandle inde = allocator.Allocate((nuint)data.GetLength(0), 16);
        inde.Buffer.View.Cast<byte, int>().CopyFrom(intd.AsSpan()[..32]);
        geo.SetVertexAttributeCount(3);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, inde, 0, 4, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, vert, 0, 16, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE, 0, RTCFormat.RTC_FORMAT_FLOAT3, vert, 16, 16, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE, 1, RTCFormat.RTC_FORMAT_FLOAT3, vert, 32, 16, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE, 2, RTCFormat.RTC_FORMAT_FLOAT3, vert, 64, 16, 32);
        geo.Commit();
    }

    static void TestGeoMultiSet(EmbreeDevice device, ISharedBufferAllocator allocator, float[] data, int[] intd)
    {
        EmbreeGeometry geo = new(device, RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);
        SharedBufferHandle vert = allocator.Allocate((nuint)32 * 4 * 4 * sizeof(float), 16);
        vert.Buffer.View.Cast<byte, float>().CopyFrom(data.AsSpan()[..(32 * 4 * 4)]);
        SharedBufferHandle inde = allocator.Allocate((nuint)data.GetLength(0), 16);
        inde.Buffer.View.Cast<byte, int>().CopyFrom(intd.AsSpan()[..32]);
        geo.SetVertexAttributeCount(3);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, inde, 0, 4, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, vert, 0, 16, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE, 0, RTCFormat.RTC_FORMAT_FLOAT3, vert, 16, 16, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE, 1, RTCFormat.RTC_FORMAT_FLOAT3, vert, 32, 16, 32);
        geo.SetSharedBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE, 2, RTCFormat.RTC_FORMAT_FLOAT3, vert, 64, 16, 32);
        EmbreeBuffer buffer = new(device, 1024 * 1024 * 4);
        geo.SetBuffer(RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, buffer, 0, 4, 32);
        geo.SetBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, buffer, 0, 16, 64);
        geo.SetNewBuffer(RTCBufferType.RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE, 1, RTCFormat.RTC_FORMAT_FLOAT3, 32, 16);
        geo.Commit();
    }
}
