using System;
using System.Runtime.InteropServices;
using BOOL = int;
using DWORD = uint;
using LPVOID = nint;
using SIZE_T = nuint;

namespace EmbreeSharp
{
    public static partial class SseUtility
    {
        private const DWORD MEM_COMMIT = 0x00001000;
        private const DWORD MEM_RESERVE = 0x00002000;
        private const DWORD MEM_RELEASE = 0x00008000;

        private const DWORD PAGE_EXECUTE_READ = 0x20;
        private const DWORD PAGE_READWRITE = 0x04;

        [LibraryImport("kernel32.dll")]
        internal static partial LPVOID VirtualAlloc(LPVOID lpAddress, SIZE_T dwSize, DWORD flAllocationType, DWORD flProtect);
        [LibraryImport("kernel32.dll")]
        internal static partial BOOL VirtualProtect(LPVOID lpAddress, SIZE_T dwSize, DWORD flNewProtect, out DWORD lpflOldProtect);
        [LibraryImport("kernel32.dll")]
        internal static partial BOOL VirtualFree(LPVOID lpAddress, SIZE_T dwSize, DWORD dwFreeType);

        private const int PROT_READ = 0x1;
        private const int PROT_WRITE = 0x2;
        private const int PROT_EXEC = 0x4;

        private const int MAP_PRIVATE = 0x2;
        private const int MAP_ANONYMOUS = 0x100000;

        private const int MAP_FAILED = -1;

        [LibraryImport("libc")]
        internal static unsafe partial nint mmap(nint addr, nuint len, int prot, int flags, int fd, int offset);
        [LibraryImport("libc")]
        internal static unsafe partial int mprotect(void* start, nuint len, int prot);
        [LibraryImport("libc")]
        internal static unsafe partial int munmap(void* start, nuint len);

        private static unsafe void EmbreeMxcsrRegisterControlWin32(ReadOnlySpan<byte> code)
        {
            LPVOID pMemory = VirtualAlloc(nint.Zero, (nuint)code.Length, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            if (pMemory == LPVOID.Zero)
            {
                Console.Error.WriteLine("VirtualAlloc cannot alloc memory");
                return;
            }
            NativeMemoryView<byte> view = new(pMemory.ToPointer(), (nuint)code.Length);
            view.CopyFrom(code);
            if (VirtualProtect(pMemory, (nuint)code.Length, PAGE_EXECUTE_READ, out _) == 0)
            {
                VirtualFree(pMemory, 0, MEM_RELEASE);
                Console.Error.WriteLine("VirtualProtect cannot change memory to execute");
                return;
            }
            var funcPtr = (delegate* unmanaged<void>)pMemory.ToPointer();
            funcPtr();
            VirtualFree(pMemory, 0, MEM_RELEASE);
        }

        private static unsafe void EmbreeMxcsrRegisterControlUnix(ReadOnlySpan<byte> code)
        {
            nint pMemory = mmap(nint.Zero, (nuint)code.Length, PROT_READ | PROT_WRITE, MAP_PRIVATE | MAP_ANONYMOUS, -1, 0);
            if (pMemory == MAP_FAILED)
            {
                Console.Error.WriteLine("mmap cannot alloc memory");
                return;
            }
            NativeMemoryView<byte> view = new(pMemory.ToPointer(), (nuint)code.Length);
            view.CopyFrom(code);
            if (mprotect(pMemory.ToPointer(), (nuint)code.Length, PROT_READ | PROT_EXEC) == -1)
            {
                Console.Error.WriteLine("mprotect cannot change memory to execute");
                munmap(pMemory.ToPointer(), (nuint)code.Length);
                return;
            }
            var funcPtr = (delegate* unmanaged<void>)pMemory.ToPointer();
            funcPtr();
            munmap(pMemory.ToPointer(), (nuint)code.Length);
        }

        public static unsafe void EmbreeMxcsrRegisterControl()
        {
            if (RuntimeInformation.OSArchitecture != Architecture.X64 && RuntimeInformation.OSArchitecture != Architecture.X86)
            {
                return;
            }
            /**
               #include <xmmintrin.h>
               #include <pmmintrin.h>
               void Setup() {
                   _MM_SET_FLUSH_ZERO_MODE(_MM_FLUSH_ZERO_ON);
                   _MM_SET_DENORMALS_ZERO_MODE(_MM_DENORMALS_ZERO_ON);
               }
             */
            //0000000000000000: 0F AE 5C 24 08     stmxcsr dword ptr[rsp + 8]
            //0000000000000005: 8B 44 24 08        mov eax, dword ptr[rsp + 8]
            //0000000000000009: 0F BA E8 0F        bts eax,0Fh
            //000000000000000D: 89 44 24 08        mov dword ptr[rsp + 8],eax
            //0000000000000011: 0F AE 54 24 08     ldmxcsr dword ptr[rsp + 8]
            //0000000000000016: 0F AE 5C 24 08     stmxcsr dword ptr[rsp + 8]
            //000000000000001B: 8B 44 24 08        mov eax, dword ptr[rsp + 8]
            //000000000000001F: 83 C8 40           or eax,40h
            //0000000000000022: 89 44 24 08        mov dword ptr[rsp + 8],eax
            //0000000000000026: 0F AE 54 24 08     ldmxcsr dword ptr[rsp + 8]
            //000000000000002B: C3 ret
            byte[] code = [
                0x0F, 0xAE, 0x5C, 0x24, 0x08,
                0x8B, 0x44, 0x24, 0x08,
                0x0F, 0xBA, 0xE8, 0x0F,
                0x89, 0x44, 0x24, 0x08,
                0x0F, 0xAE, 0x54, 0x24, 0x08,
                0x0F, 0xAE, 0x5C, 0x24, 0x08,
                0x8B, 0x44, 0x24, 0x08,
                0x83, 0xC8, 0x40,
                0x89, 0x44, 0x24, 0x08,
                0x0F, 0xAE, 0x54, 0x24, 0x08,
                0xC3
            ];

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                EmbreeMxcsrRegisterControlWin32(code);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                EmbreeMxcsrRegisterControlUnix(code);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                EmbreeMxcsrRegisterControlUnix(code);
            }
        }
    }
}
