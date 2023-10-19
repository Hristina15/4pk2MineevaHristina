using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

class Program
{
    const int PAGE_SIZE = 4096; // Размер страницы

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, MemoryProtection flNewProtect, out MemoryProtection lpflOldProtect);

    [DllImport("kernel32.dll")]
    static extern int VirtualQuery(IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern uint GetLastError();

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, out string lpBuffer, uint nSize, IntPtr Arguments);

    // Тип выделения памяти
    [Flags]
    enum AllocationType
    {
        MEM_COMMIT = 0x1000,
        MEM_RESERVE = 0x2000,
        MEM_RESET = 0x80000,
        MEM_RESET_UNDO = 0x1000000,
        MEM_LARGE_PAGES = 0x20000000
    }

    // Защита памяти
    [Flags]
    enum MemoryProtection
    {
        PAGE_NOACCESS = 0x01,
        PAGE_READONLY = 0x02,
        PAGE_READWRITE = 0x04,
        PAGE_WRITECOPY = 0x08,
        PAGE_EXECUTE = 0x10,
        PAGE_EXECUTE_READ = 0x20,
        PAGE_EXECUTE_READWRITE = 0x40,
        PAGE_EXECUTE_WRITECOPY = 0x80,
        PAGE_GUARD = 0x100,
        PAGE_NOCACHE = 0x200,
        PAGE_WRITECOMBINE = 0x400
    }

    struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public MemoryProtection AllocationProtect;
        public IntPtr RegionSize;
        public uint State;
        public uint Protect;
        public uint Type;
    }

    static void Main()
    {
        try
        {
            IntPtr regionAddress = VirtualAlloc(IntPtr.Zero, PAGE_SIZE, AllocationType.MEM_RESERVE | AllocationType.MEM_COMMIT, MemoryProtection.PAGE_READWRITE);
            if (regionAddress == IntPtr.Zero)
            {
                throw new Win32Exception((int)GetLastError());
            }

            // Заполняем регион памяти значением 7Fh
            byte value = 0x7F;
            Marshal.WriteByte(regionAddress, value);

            MEMORY_BASIC_INFORMATION memInfo;
            if (VirtualQuery(regionAddress, out memInfo, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) == 0)
            {
                throw new Win32Exception((int)GetLastError());
            }

            Console.WriteLine("Размер региона памяти: {0}", memInfo.RegionSize);
            Console.WriteLine("Защита выделенной памяти: {0}", memInfo.AllocationProtect);
            Console.WriteLine("Базовый адрес региона памяти: {0}", memInfo.BaseAddress);
            Console.WriteLine("Состояние региона памяти: {0}", memInfo.State);

            // Демонстрация работы VirtualProtect
            if (VirtualProtect(regionAddress, PAGE_SIZE, MemoryProtection.PAGE_READWRITE | MemoryProtection.PAGE_GUARD, out MemoryProtection oldProtect) == false)
            {
                throw new Win32Exception((int)GetLastError());
            }

            Console.WriteLine("Старая защита региона памяти: {0}", oldProtect);

            // Возвращаем исходную защиту региона памяти
            if (VirtualProtect(regionAddress, PAGE_SIZE, memInfo.AllocationProtect, out oldProtect) == false)
            {
                throw new Win32Exception((int)GetLastError());
            }

            Console.WriteLine("Новая защита региона памяти: {0}", oldProtect);
        }
        catch (Win32Exception ex)
        {
            string errorMessage;
            if (FormatMessage(0x1300, IntPtr.Zero, (uint)ex.ErrorCode, 0, out errorMessage, 255, IntPtr.Zero) != 0)
            {
                Console.WriteLine("Ошибка: {0}", errorMessage.Trim());
            }
            else
            {
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }

        Console.ReadLine();
    }
}
