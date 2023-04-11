using EmbreeSharp.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public enum RtcISA
{
    Sse2,
    Sse42,
    Avx,
    Avx2,
    Avx512
}

public enum RtcFrequencyLevel
{
    Simd128,
    Simd256,
    Simd512
}

public class RtcDeviceConfig
{
    public int? Threads { get; set; } = null;
    public int? UserThreads { get; set; } = null;
    public bool? SetAffinity { get; set; } = null;
    public bool? StartThreads { get; set; } = null;
    public RtcISA? ISA { get; set; } = null;
    public RtcISA? MaxISA { get; set; } = null;
    public bool? HugePages { get; set; } = null;
    public bool? EnableSeLockMemoryPrivilege { get; set; } = null;
    public int? Verbose { get; set; } = null;
    public RtcFrequencyLevel? FrequencyLevel { get; set; } = null;

    public override string ToString()
    {
        List<(string, string)> cfg = new();
        if (Threads != null)
        {
            cfg.Add(("threads", Threads.Value.ToString()));
        }
        if (UserThreads != null)
        {
            cfg.Add(("user_threads", UserThreads.Value.ToString()));
        }
        if (SetAffinity != null)
        {
            cfg.Add(("set_affinity", SetAffinity.Value ? "1" : "0"));
        }
        if (StartThreads != null)
        {
            cfg.Add(("start_threads", StartThreads.Value ? "1" : "0"));
        }
        if (ISA != null)
        {
            cfg.Add(("isa", IsaToString(ISA.Value)));
        }
        if (MaxISA != null)
        {
            cfg.Add(("max_isa", IsaToString(MaxISA.Value)));
        }
        if (HugePages != null)
        {
            cfg.Add(("hugepages", HugePages.Value ? "1" : "0"));
        }
        if (EnableSeLockMemoryPrivilege != null)
        {
            cfg.Add(("enable_selockmemoryprivilege", EnableSeLockMemoryPrivilege.Value ? "1" : "0"));
        }
        if (Verbose != null)
        {
            cfg.Add(("verbose", Math.Min(Math.Max(Verbose.Value, 0), 3).ToString()));
        }
        if (FrequencyLevel != null)
        {
            cfg.Add(("frequency_level", FrequencyLevel.Value.ToString().ToLower()));
        }
        StringBuilder sb = new();
        for (int i = 0; i < cfg.Count; i++)
        {
            var (name, value) = cfg[i];
            sb.Append(name).Append('=').Append(value);
            if (i < cfg.Count - 1)
            {
                sb.Append(',');
            }
        }
        return sb.ToString();

        static string IsaToString(RtcISA isa)
        {
            return isa switch
            {
                RtcISA.Sse2 => "sse2",
                RtcISA.Sse42 => "sse4.2",
                RtcISA.Avx => "avx",
                RtcISA.Avx2 => "avx2",
                RtcISA.Avx512 => "avx512",
                _ => string.Empty,
            };
        }
    }
}

public delegate void RtcDeviceErrorCallback(RTCError error, string message);
public delegate bool RtcMemoryMonitorCallback(long bytes, bool post);

public class RtcDevice : IDisposable
{
    RTCDevice _device;
    RTCErrorFunction? _errorFunc;
    RTCMemoryMonitorFunction? _memoryMoitor;
    bool _disposedValue;

    public RTCDevice NativeHandler => _device;

    public RtcDevice(RtcDeviceConfig? config = null)
    {
        unsafe
        {
            byte[]? cfgBytes = null;
            if (config != null)
            {
                string cfgStr = config.ToString();
                cfgBytes = Encoding.UTF8.GetBytes(cfgStr);
            }
            if (cfgBytes == null)
            {
                _device = rtcNewDevice(null);
            }
            else
            {
                fixed (byte* ptr = cfgBytes)
                {
                    _device = rtcNewDevice((sbyte*)ptr);
                }
            }
            if (_device.Ptr == IntPtr.Zero)
            {
                RTCError err = rtcGetDeviceError(new() { Ptr = IntPtr.Zero });
                ThrowInvalidOperation($"cannot create device, error: {err.ToString()}");
            }
        }
    }

    ~RtcDevice()
    {
        Dispose(disposing: false);
    }

    public RtcScene NewScene()
    {
        return new RtcScene(this);
    }

    public RtcGeometry NewGeometry(RTCGeometryType type)
    {
        return new RtcGeometry(this, type);
    }

    public RtcUniqueBuffer NewBuffer(long byteSize)
    {
        return new RtcUniqueBuffer(this, byteSize);
    }

    public RtcSharedBuffer NewSharedBuffer(IntPtr ptr, long byteSize)
    {
        return new RtcSharedBuffer(this, ptr, byteSize);
    }

    public ManagedRtcSharedBuffer<T> NewSharedBuffer<T>(T[] array) where T : struct
    {
        return new ManagedRtcSharedBuffer<T>(this, array);
    }

    public RtcBvh<TNode, TLeaf> NewBvh<TNode, TLeaf>() where TNode : struct where TLeaf : struct
    {
        return new RtcBvh<TNode, TLeaf>(this);
    }

    public void SetErrorCallback(RtcDeviceErrorCallback? callback)
    {
        unsafe
        {
            if (callback == null)
            {
                rtcSetDeviceErrorFunction(_device, IntPtr.Zero, null);
                _errorFunc = null;
            }
            else
            {
                _errorFunc = (void* userPtr, RTCError code, sbyte* str) =>
                {
                    //simple strlen() :)
                    int i = 0;
                    for (; i < 4096; i++)
                    {
                        if (str[i] == 0)
                        {
                            break;
                        }
                    }
                    string msg = Encoding.UTF8.GetString((byte*)str, i);
                    callback(code, msg);
                };
                rtcSetDeviceErrorFunction(_device, Marshal.GetFunctionPointerForDelegate(_errorFunc), null);
            }
        }
    }

    public void SetMemoryMonitorCallback(RtcMemoryMonitorCallback? callback)
    {
        unsafe
        {
            if (callback == null)
            {
                rtcSetDeviceMemoryMonitorFunction(_device, IntPtr.Zero, null);
                _memoryMoitor = null;
            }
            else
            {
                _memoryMoitor = (void* ptr, long bytes, bool post) =>
                {
                    return callback(bytes, post);
                };
                rtcSetDeviceErrorFunction(_device, Marshal.GetFunctionPointerForDelegate(_memoryMoitor), null);
            }
        }
    }

    public long GetProperty(RTCDeviceProperty property)
    {
        return rtcGetDeviceProperty(_device, property);
    }

    public void SetProperty(RTCDeviceProperty property, long value)
    {
        rtcSetDeviceProperty(_device, property, value);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _errorFunc = null;
            }

            rtcReleaseDevice(_device);
            _device = default;

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
