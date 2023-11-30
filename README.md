# EmbreeSharp

EmbreeSharp is an unofficial C# low-level binding for [embree](https://github.com/embree/embree)

Provide a tiny safe wrapper in C#

Target framework is `net8.0`

This project is still under development. API are not stable.

## Use

Install this package from nuget.

Install the native library package for your platform from nuget. Or, compile embree by yourself and put native library into `runtimes/${rid}/native`

OS    | x64
--    | --
win   |[embree-win-x64](https://www.nuget.org/packages/embree-win-x64/)
linux |[embree-linux-x64](https://www.nuget.org/packages/embree-linux-x64/)
osx   |[embree-osx-x64](https://www.nuget.org/packages/embree-osx-x64/)

osx-x64 should first install tbb in system.

## Directory structure

* `EmbreeSharp`: source code, tiny safe wrapper also in this dir
  * `Native`: low-level bindings
* `EmbreeSharp.Test`: some tests
* `Samples`: some simple examples
* `Script`: some scripts to package the official native libraries

## Details

### About native library

The official only provides binary files for `win-x64`, `linux-x64` and `osx-x64`. If you want to run on other platforms, you should compile embree by yourself.

Because native libraries uploaded to nuget are separate, everyone can upload their compiled native libraries for others to use.

If you upload your compiled library, you can tell me the package name. I will display it in readme.

The `Script` folder contains some help scripts to package the official compiled native libraries into the nuget package.

### About load native library in C#

Loading native dynamic library of embree is complicated. Because [official release](https://github.com/embree/embree/releases) depends on TBB. P/Invoke cannot find its path automatically. There is no way to set library search path (as far as I know). So I use `System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver` to custom loading logic.

### 

## Limitation

Cannot use all APIs related to SYCL.

## TODO

I will provide more safe functions for C# wrapper. But limited by language difference, It is almost impossible to have both performance and safety.

If you have any idea, welcome issues and pull requests

### Shared Buffer

The memory for shared buffer is managed by user.

Accouding to the API reference: [rtcSetSharedGeometryBuffer](https://github.com/embree/embree#rtcsetsharedgeometrybuffer)

> The buffer data must remain valid for as long as the buffer may be used, and the user is responsible for freeing the buffer data when no longer required.

This is a lifecycle problem.

I think reference count or reference tracing is the way to solve it. Because memory should know if there is a buffer in use. This can avoid dangling pointer. If the memory is not used. We can free memory proactively and safely.

Or, determined by GC. Yes, we can let buffer reference memory. When all buffers are released. Memory will only be freed when all buffers are released. But I think this is a backup option.

### Geometry User Data

Same problem with shared buffer.

### SetGeometry[Intersect/Occluded/Bounds]Function

Just provide safe API

### BVH Build

TBC...

### MXCSR control and status register

[link](https://github.com/embree/embree#mxcsr-control-and-status-register)

This is x86-64 instruction. C# API does not provide them.

Make a native library and use P/Invoke?

Hmm...maybe we can use OS api. Write binary code to memory and call OS api to set memory as executable such as `VirtualProtectEx` on win32.

## License

The copyright of Embree belongs to the Embree development team

This project is under MIT
