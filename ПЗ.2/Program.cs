using System.Text;
using System.Runtime.InteropServices;
using static Program;
using System;
using System.Drawing;

class Program
{
    [DllImport("kernel32.dll")]
    public static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

    [DllImport("kernel32.dll")]
    public static extern void GetVersionEx(ref OSVERSIONINFO osVersionInfo);

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_INFO
    {
        public ushort processorArchitecture;
        public ushort reserved;
        public uint pageSize;
        public IntPtr minimumApplicationAddress;
        public IntPtr maximumApplicationAddress;
        public IntPtr activeProcessorMask;
        public uint numberOfProcessors;
        public uint processorType;
        public uint allocationGranularity;
        public ushort processorLevel;
        public ushort processorRevision;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OSVERSIONINFO
    {
        public int dwOSVersionInfoSize;
        public int dwMajorVersion;
        public int dwMinorVersion;
        public int dwBuildNumber;
        public int dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
    }
    static void Main(string[] args)
    {
        string GetComputerName = Environment.MachineName;
        Console.WriteLine("Имя компьютера: " + GetComputerName);

        string GetSystemDirectory = Environment.SystemDirectory;
        string GetWindowsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        Console.WriteLine("Путь к системному каталогу Windows: " + GetWindowsDirectory);
        Console.WriteLine("Путь к системному каталогу: " + GetSystemDirectory);
        string tempDirectory = Path.GetTempPath();
        Console.WriteLine("Путь к каталогу временных файлов: " + tempDirectory);

        OSVERSIONINFO GetVersionInfo = new OSVERSIONINFO();
        GetVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFO));
        GetVersionEx(ref GetVersionInfo);
        string GetVersion = GetVersionInfo.dwMajorVersion + "." + GetVersionInfo.dwMinorVersion;
        Console.WriteLine("Версия операционной системы: " + GetVersion);

        int processorCount = Environment.ProcessorCount;
        int workingSetSize = (int)Environment.WorkingSet;
        Console.WriteLine("Количество процессоров: " + processorCount);
        Console.WriteLine("Размер рабочего стола: " + workingSetSize);

        Console.ReadLine();
    }
}
