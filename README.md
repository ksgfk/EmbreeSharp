# EmbreeSharp

EmbreeSharp is an unofficial C # low-level binding of [embree](https://github.com/embree/embree)

Target framework is `.NET Standard 2.1`

This project is still under development. API is not stable.

## Use

Install this package from nuget.

Install the native library package for your platform from nuget.

## Details

Embree is so massive that I uploaded native library separately.

Now available native library is `embree-win-x64` and `embree-linux-x64`. You can find them on nuget

Only support `win-x64` and `linux-x64` because the official only provides binary files for these platforms.

`Scripts` folder contains some helper scripts to packaging native libraries into nuget packages.

Loading native dynamic library of embree is complicated. Because [official release](https://github.com/embree/embree/releases) depends on TBB. P/Invoke cannot find its path automatically. There is no way to set library search path (as far as I know). So I use `System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver` to custom loading logic.

## License

The copyright of Embree belongs to the Embree development team

This project is under MIT
