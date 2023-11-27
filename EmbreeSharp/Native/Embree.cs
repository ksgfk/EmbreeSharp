using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public static unsafe partial class Embree
    {
        public const int RTC_MAX_INSTANCE_LEVEL_COUNT = 1;
        public const uint RTC_INVALID_GEOMETRY_ID = unchecked((uint)-1);
        public const string DynamicLibraryName = "embree4";

        static Embree()
        {
            NativeLibrary.SetDllImportResolver(typeof(Embree).Assembly, (string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
            {
                if (libraryName != DynamicLibraryName)
                {
                    return IntPtr.Zero;
                }
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                IntPtr target = IntPtr.Zero;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var nativeDir = Path.Combine(dir, "runtimes", $"win-{GetRid()}", "native");
                    NativeLibrary.Load(Path.Combine(nativeDir, "tbbmalloc.dll"), assembly, searchPath);
                    NativeLibrary.Load(Path.Combine(nativeDir, "tbb12.dll"), assembly, searchPath);
                    target = NativeLibrary.Load(Path.Combine(nativeDir, "embree4.dll"), assembly, searchPath);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    var nativeDir = Path.Combine(dir, "runtimes", $"linux-{GetRid()}", "native");
                    NativeLibrary.Load(Path.Combine(nativeDir, "libtbbmalloc.so"), assembly, searchPath);
                    NativeLibrary.Load(Path.Combine(nativeDir, "libtbb.so"), assembly, searchPath);
                    target = NativeLibrary.Load(Path.Combine(nativeDir, "libembree4.so"), assembly, searchPath);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    var nativeDir = Path.Combine(dir, "runtimes", $"osx-{GetRid()}", "native");
                    target = NativeLibrary.Load(Path.Combine(nativeDir, "libembree4.dylib"), assembly, searchPath);
                }
                else
                {
                    Console.Error.WriteLine($"cannot load platform {RuntimeInformation.RuntimeIdentifier}");
                }
                return target;
            });

            static string GetRid()
            {
                return RuntimeInformation.OSArchitecture switch
                {
                    Architecture.X64 => "x64",
                    Architecture.Arm64 => "arm64",
                    _ => string.Empty
                };
            }
        }
    }
}
