# EmbreeSharp

![](https://github.com/ksgfk/EmbreeSharp/actions/workflows/test.yml/badge.svg) [![NuGet](https://img.shields.io/nuget/v/EmbreeSharp)](https://www.nuget.org/packages/EmbreeSharp)

EmbreeSharp is a C# Low-level binding for [embree](https://github.com/embree/embree)

Target framework is .NET 7

This project is still under development. API is not stable.

## Build

Clone this project.

Put the native libraries into the folder. For example, your OS is win11 x64. You should put `embree4.dll`, `tbb12.dll` and `tbbmalloc.dll` into `EmbreeSharp\runtimes\win-x64\native`

Build command: `dotnet build --configuration Release`

This project use Github Action. You can check out the build script in `.github\workflows\publish.yml` for more help

## Details

This project use [ClangSharp](https://github.com/dotnet/ClangSharp) to generate binding code with some tweaks.

Currently clangsharp cannot generate bindings for opaque struct. Like `RTCDevice`, it's actually `struct RTCDeviceTy*`. That's ok to use `System.IntPtr` to to pass parameters. But for type safety, create a C# struct contains a `System.IntPtr` is the best way.

Loading dynamic library of embree is complicated. Because [official release](https://github.com/embree/embree/releases) depends on TBB. P/Invoke cannot find its path automatically. There is no way to set library search path (as far as I know). So use `System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver` to custom loading logic. Now it's only support `win`, `linux` and `osx`.

## Known issues

### Cannot use `rtcBuildBVH` on macos

Unit test in `EmbreeSharp.Test/TestBvh.cs`

Call `rtcBuildBVH` function on macos will crash if filled `RTCBuildArguments` correctly. That's OK on win and linux. I don't have a mac so can't debug this issue...

## License

MIT
