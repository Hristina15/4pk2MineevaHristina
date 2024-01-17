using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Lr_2
{
    class Program
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetModuleFileName(IntPtr hModule, System.Text.StringBuilder lpFilename, uint nSize);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcessId();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle,
            IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, bool bInheritHandle, uint dwOptions);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        static void Main(string[] args)
        {
            IntPtr hModule = System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress;
            StringBuilder filename = new StringBuilder(1024);
            GetModuleFileName(hModule, filename, (uint)filename.Capacity);
            string moduleName = filename.ToString();
            Console.WriteLine("Имя модуля: " + moduleName);

            hModule = LoadLibrary("kernel32.dll");
            filename = new StringBuilder(1024);
            GetModuleFileName(hModule, filename, (uint)filename.Capacity);
            string moduleNameForLibrary = filename.ToString();
            Console.WriteLine("Имя модуля для kernel32: " + moduleNameForLibrary);

            int processId = GetCurrentProcessId();
            Console.WriteLine("Идентификатор текущего процесса: " + processId);

            IntPtr pseudoHandle = GetCurrentProcess();
            Console.WriteLine("Псевдодескриптор текущего процесса: " + pseudoHandle);

            IntPtr processHandle = GetCurrentProcess();
            Console.WriteLine("Дескриптор текущего процесса: " + processHandle);

            // Копия дескриптора
            IntPtr duplicateHandle;
            bool success = DuplicateHandle(pseudoHandle, pseudoHandle, pseudoHandle, out duplicateHandle, 0, false, 2);

            if (success)
            {
                Console.WriteLine("Дублированный дескриптор: " + duplicateHandle);
                // Закрытие дескриптора
                CloseHandle(duplicateHandle);
            }

            // Открытие копии дескриптора 
            IntPtr openedProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, false, processId);
            if (openedProcessHandle != IntPtr.Zero)
            {
                Console.WriteLine("Открытый дескриптор: " + openedProcessHandle);
                // Закрытие открытого дескриптора
                CloseHandle(openedProcessHandle);
            }

            // Информация о процессах, потоках, модулях
            Process currentProcess = Process.GetCurrentProcess();

            Console.WriteLine("\nИнформация о процессах:");
            foreach (Process process in Process.GetProcesses())
            {
                Console.WriteLine("ID: " + process.Id + " Имя: " + process.ProcessName);
            }

            Console.WriteLine("Информация о потоках процесса " + currentProcess.ProcessName + ":");
            foreach (ProcessThread thread in currentProcess.Threads)
            {
                Console.WriteLine("ID: " + thread.Id + " Длина: " + thread.TotalProcessorTime);
            }
            Console.WriteLine("\nИнформация о модулях процесса " + currentProcess.ProcessName + ":");
            foreach (ProcessModule module in currentProcess.Modules)
            {
                Console.WriteLine("Имя: " + module.ModuleName + ", Путь: " + module.FileName);
            }

            Console.ReadLine();
        }
    }
}
