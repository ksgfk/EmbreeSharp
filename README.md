# EmbreeSharp

EmbreeSharp is an unofficial C # low-level binding of [embree](https://github.com/embree/embree)

Target framework is `net8.0`

This project is still under development. API is not stable.

## Use

Install this package from nuget.

Install the native library package for your platform from nuget. Or, compile embree by yourself and put native library into `runtimes/${rid}/native`

OS    | x64
--    | --
win   |[embree-win-x64](https://www.nuget.org/packages/embree-win-x64/)
linux |[embree-linux-x64](https://www.nuget.org/packages/embree-linux-x64/)
osx   |[embree-osx-x64](https://www.nuget.org/packages/embree-osx-x64/)

## Details

### About native library

The official only provides binary files for `win-x64`, `linux-x64` and `osx-x64`. If you want to run on other platforms, you should compile embree by yourself.

Because native libraries uploaded to nuget are separate, everyone can upload their compiled native libraries for others to use.

If you upload your compiled library, you can tell me the package name. I will display it in readme.

The `Script` folder contains some help scripts to package the official compiled native libraries into the nuget package.

### About load native library in C#

Loading native dynamic library of embree is complicated. Because [official release](https://github.com/embree/embree/releases) depends on TBB. P/Invoke cannot find its path automatically. There is no way to set library search path (as far as I know). So I use `System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver` to custom loading logic.

## License

The copyright of Embree belongs to the Embree development team

This project is under MIT
