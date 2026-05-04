# EmbreeSharp

[![NuGet](https://img.shields.io/nuget/v/EmbreeSharp)](https://www.nuget.org/packages/EmbreeSharp)

EmbreeSharp is an unofficial C# low-level binding for [embree](https://github.com/embree/embree)

Provide a tiny safe wrapper in C#

Target framework is `net8.0/net10.0`

This project is still under development. API are not stable.

## Use

Now target embree version is `v4.4.1`

Install this package from nuget.

Install the native library package for your platform from nuget. Or, compile embree by yourself and put native library into `runtimes/${rid}/native`

OS    | x64 | arm64
--    | -- | --
win   |[![NuGet](https://img.shields.io/nuget/v/embree-win-x64)](https://www.nuget.org/packages/embree-win-x64) | /
linux |[![NuGet](https://img.shields.io/nuget/v/embree-linux-x64)](https://www.nuget.org/packages/embree-linux-x64) | /
osx   |[![NuGet](https://img.shields.io/nuget/v/embree-osx-x64)](https://www.nuget.org/packages/embree-osx-x64) | [![NuGet](https://img.shields.io/nuget/v/embree-osx-arm64)](https://www.nuget.org/packages/embree-osx-arm64)

If you are using version `4.3.0`. osx-x64 should first install tbb in system.

## Directory structure

* `EmbreeSharp`: source code, tiny safe wrapper also in this dir
  * `Native`: low-level bindings
* `EmbreeSharp.Test`: some tests
* `Samples`: some simple examples
* `Script`: some scripts to package the official native libraries

## Details

### About native library

The official only provides binary files for `win-x64`, `linux-x64`,  `osx-x64` and `osx-arm64`. If you want to run on other platforms, you should compile embree by yourself.

Because native libraries uploaded to nuget are separate, everyone can upload their compiled native libraries for others to use.

If you upload your compiled library, you can tell me the package name. I will display it in readme.

The `Script` folder contains some help scripts to package the official compiled native libraries into the nuget package.

### About load native library in C#

Loading native dynamic library of embree is complicated. Because [official release](https://github.com/embree/embree/releases) depends on TBB. P/Invoke cannot find its path automatically. There is no way to set library search path (as far as I know). So I use `System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver` to custom loading logic.

### Shared Buffer

The memory for shared buffer is managed by user.

Accouding to the API reference: [rtcSetSharedGeometryBuffer](https://github.com/embree/embree#rtcsetsharedgeometrybuffer)

> The buffer data must remain valid for as long as the buffer may be used, and the user is responsible for freeing the buffer data when no longer required.

This is a lifecycle problem.

Currently, I provide `EmbreeSharp.ISharedBufferAllocation` as buffer allocator. It ensures that the allocated buffer address is available until it is released. `EmbreeSharp.ISharedBufferAllocation` as allocate result. It provides the buffer address and length.

The most crucial part is ` EmbreeSharp.SharedBufferHandle`. It inherits from `System.Runtime.InteropServices.SafeHandle`. The original purpose of `SafeHandle` was serve as a secure native handle when interop with C API. It has built-in reference counting function. Therefore, here we borrow its reference count function.

`SharedBufferHandle` is used to manage the lifecycle of `ISharedBufferAllocation`. For example, pass it to construct `EmbreeSharp.EmbreeSharedBuffer`. It will increase the reference count. Due to `EmbreeSharedBuffer` implements the dispose pattern, GC will use finalizer when the user forgets to release it. At the same time decrease the reference count. When the reference count returns to zero, it will free shared buffer

Ideally, it can avoid memory leaks caused by users forgetting to free. But it has not been tested

### MXCSR control and status register

[link](https://github.com/embree/embree#mxcsr-control-and-status-register)

This is x86-64 instruction. C# API does not provide them.

You can call `SseUtility.EmbreeMxcsrRegisterControl` to enable `Flush to Zero` and `Denormals are Zero` mode for pre thread.

## Limitation

Cannot use all APIs related to SYCL.

## About EmbreeSharp.SIMD

`EmbreeSharp.SIMD` is an experimental SIMD abstraction library. The original goal was to build a close-to-zero-cost C# abstraction where the same generic algorithm could be statically dispatched to different ISA backends, such as SSE, AVX, NEON, and an explicit scalar fallback.

In practice, C# and the .NET compilation model make this extremely hard. Almost impossible, at least as of .NET 10. The current library shows the pain pretty clearly:

1. C# has no associated types. Every abstraction has to spell out all the types it may depend on. A primitive packet only needs `TSelf`, `TScalar`, and `TMask`, but a vector built on top of that packet needs `TSelf`, `TPacket`, `TScalar`, `TVectorMask`, and `TPacketMask`. A matrix grows again into `TSelf`, `TVector`, `TPacket`, `TScalar`, `TMatrixMask`, `TVectorMask`, and `TPacketMask`. These relationships should belong to the backend itself, but instead they have to be repeated through every interface, every constraint, and every generic algorithm.
2. C# does not have const generics in a form that helps here. Lane count, vector dimension, and matrix dimension should really be type-level constants. Right now they are expressed through `static abstract int LaneCount` plus hand-written families such as `Vector2`, `Vector3`, `Vector4`, `Matrix2`, `Matrix3`, and `Matrix4`. That means 2/3/4D vectors and 2x2/3x3/4x4 matrices cannot share enough structure, so a lot of APIs are copied by hand.
3. Static abstract interface members make the API expressible, but they do not guarantee zero cost. Whether a generic call is fully inlined, whether the abstraction disappears, and whether the generated code gets the register allocation and instruction selection you hoped for is up to the JIT or AOT compiler. The library cannot make that a stable contract. Change the runtime version, target platform, or call shape, and the generated code may change with it.
4. C# hardware intrinsics are concrete ISA APIs, not a composable portable SIMD backend. SSE, AVX, and NEON differ in register width, available instructions, mask representation, comparison results, and load/store rules. To present one `Packet...` API, every backend needs a lot of glue code. Some operations also have no exact hardware instruction, so they have to be approximated, built from several instructions, or moved back to scalar code.
5. Masks become a second type system. Packet masks, vector masks, and matrix masks grow together with primitives, vectors, and matrices. `Select`, `All`, `Any`, `None`, `AndNot`, bool span load/store, and bitwise mask behavior all have to stay consistent across backends, even though the native mask semantics are not the same across ISAs. This produces a lot of code, and it is easy for small semantic differences to sneak in.
6. Composite types multiply the backend count. There are primitive families such as float32, float64, int32, and uint32. Each of those can have SSE, AVX, NEON, and Scalar backends. On top of that come vector2/3/4, matrix2/3/4, quaternion, and all their masks. Adding one primitive operation or one backend usually does not mean touching one file; it means updating a whole set of implementations, tests, and docs.
7. Math functions do not have one shared low-level contract. APIs like `Sin`, `Cos`, `SinCos`, `Reciprocal`, `ReciprocalSqrt`, and `FusedMultiplyAdd` have different costs and precision stories on different ISAs. Some ISAs do not have matching instructions at all, so the implementation has to use approximations. Some operations need their own precision contract. The more uniform the abstraction looks, the easier it is to hide these backend differences.
8. Source generators reduce boilerplate, but they do not change the language. They can generate repeated code, but they cannot add associated types, const generics, reliable cross-ISA specialization, or proof that the JIT will treat the generated code as zero-cost abstraction. The maintainer still has to understand and validate every generated backend.
9. The testing cost is almost as high as the implementation cost. This library has to test not only numeric results, but also mask rules, lane behavior, load/store paths, aligned and unsafe variants, matrix row/transform semantics, backend consistency, and error bounds for approximate math. Many bugs will not show up as compile errors. They show up as a tiny difference in one ISA, one lane, or one matrix convention.
10. In the end, it is very hard for this to become the foundation for Embree-style kernels. Embree's SIMD layer relies on C++ templates, target-specific object code, compiler inlining, and a mature low-level codebase. C# can wrap Embree, and it can be used for local SIMD experiments, but rebuilding an equally expressive, equally controllable, equally maintainable SIMD abstraction in C# is not realistic today.

So the more honest role for `EmbreeSharp.SIMD` is as a research project and a correctness playground. `Scalar...` types can be an explicitly selected fallback and test baseline. Some `Packet...` types can explore the edge of .NET intrinsics. But this should not pretend to be an automatic runtime-dispatched SIMD layer, and it should not promise to replace Embree's native vectorized kernels. If C# or .NET eventually gets associated types, const generics, stronger specialization/AOT control, and a more stable portable SIMD model, the interface and test experience from this experiment may still be useful for a cleaner design.

## License

The copyright of Embree belongs to the Embree development team

This project is under MIT
