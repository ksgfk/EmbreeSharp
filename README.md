# EmbreeSharp

![](https://github.com/ksgfk/EmbreeSharp/actions/workflows/test.yml/badge.svg) [![NuGet](https://img.shields.io/nuget/v/EmbreeSharp)](https://www.nuget.org/packages/EmbreeSharp)

EmbreeSharp is a C# Low-level binding for [embree](https://github.com/embree/embree)

Target framework is .NET 7

## Build

Clone this project.

Put the native libraries into the folder. For example, your OS is win11 x64. You should put `embree4.dll`, `tbb12.dll` and `tbbmalloc.dll` into `EmbreeSharp\runtimes\win-x64\native`

Now you can build the project by anyway. For example using the cmd `dotnet build --configuration Release`

This project use Github Action to build automatically. You can check out the build script in `.github\workflows\publish.yml` for more help

## Details

This project use [ClangSharp](https://github.com/dotnet/ClangSharp) to generate binding code. Generated code with some tweaks.

Currently clangsharp cannot generate bindings for opaque struct. Like `RTCDevice`, it's actually `struct RTCDeviceTy*`. We can use `System.IntPtr` to interop. But for type safety, I think create a C# struct contains a `System.IntPtr` is the best way.

Unfortunately, I don't know how to autoload the embree which is built by [official project](https://github.com/embree/embree/releases). Because it depends on tbb. And tbb is not included in the search path. I use `System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver` to set custom load logic to support load embree on `win`, `linux` and `osx`.

In the future, I may create some wrapper classes to avoid unsafe API. If you have great idea, welcome PRs!

## License

MIT
