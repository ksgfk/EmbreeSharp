using EmbreeSharp.Native;
using System;
using System.Collections.Generic;
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

public class RtcDevice : IDisposable
{
    RTCDevice _device;
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
        }
    }

    public RtcScene NewScene()
    {
        return new RtcScene(this);
    }

    public RtcGeometry NewGeometry(RTCGeometryType type)
    {
        return new RtcGeometry(this, type);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // 释放托管状态(托管对象)
            }

            rtcReleaseDevice(_device);
            _device = default;

            _disposedValue = true;
        }
    }

    ~RtcDevice()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
