using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    public static extern bool VirtualQuery(IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

    [Flags]
    public enum AllocationType : uint
    {
        COMMIT = 0x1000,
        RESERVE = 0x2000,
        RELEASE = 0x8000,
        RESET = 0x80000,
        PHYSICAL = 0x400000,
        TOP_DOWN = 0x100000,
        WRITE_WATCH = 0x200000
    }

    [Flags]
    public enum MemoryProtection : uint
    {
        EXECUTE = 0x10,
        EXECUTE_READ = 0x20,
        EXECUTE_READWRITE = 0x40,
        EXECUTE_WRITECOPY = 0x80,
        NOACCESS = 0x01,
        READONLY = 0x02,
        READWRITE = 0x04,
        WRITECOPY = 0x08,
        GUARD_Modifierflag = 0x100,
        NOCACHE_Modifierflag = 0x200,
        WRITECOMBINE_Modifierflag = 0x400
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public AllocationType AllocationProtect;
        public IntPtr RegionSize;
        public MemoryProtection Protect;
        public uint Type;
    }

    static void Main(string[] args)
    {
        int pageSize = 4096; // Размер страницы

        // Резервируем память
        IntPtr memoryRegion = VirtualAlloc(IntPtr.Zero, (uint)pageSize, (uint)AllocationType.RESERVE, (uint)MemoryProtection.NOACCESS);
        if (memoryRegion == IntPtr.Zero)
        {
            Console.WriteLine("Ошибка при резервировании памяти");
            return;
        }

        // Отображаем память
        IntPtr mappedMemory = VirtualAlloc(memoryRegion, (uint)pageSize, (uint)AllocationType.COMMIT, (uint)MemoryProtection.READWRITE);
        if (mappedMemory == IntPtr.Zero)
        {
            Console.WriteLine("Ошибка при отображении памяти");
            return;
        }

        // Заполняем память
        byte value = 0x7F;
        Marshal.WriteByte(mappedMemory, value);

        // Получаем информацию об участке памяти
        MEMORY_BASIC_INFORMATION memoryInfo;
        if (!VirtualQuery(mappedMemory, out memoryInfo, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))))
        {
            Console.WriteLine("Ошибка при получении информации об участке памяти");
            return;
        }

        // Выводим информацию
        Console.WriteLine("Размер участка памяти: " + memoryInfo.RegionSize);
        Console.WriteLine("Защита участка памяти: " + memoryInfo.AllocationProtect);
        Console.WriteLine("Базовый адрес участка памяти: " + memoryInfo.BaseAddress);
        Console.WriteLine("Состояние участка памяти: " + memoryInfo.Protect);
    }
}