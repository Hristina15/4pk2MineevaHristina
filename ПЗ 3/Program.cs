using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    // Импортируем функции из библиотеки kernel32.dll
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern int GetModuleFileName(IntPtr hModule, [Out] StringBuilder lpFilename, int nSize);

    [DllImport("kernel32.dll")]
    static extern int CloseHandle(IntPtr hObject);

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine(
            "1: Вывести все\n" +
            "2: По имени\n" +
            "3: По полному имени\n" +
            "4: По дескриптору\n" +
            "5: Информация о процессе\n" +
            "6: Информация о процессах, потоках, модулях\n"
            );

            // Ждем ввода команды от пользователя
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    GetModuleInfo(null);
                    break;
                case "2":
                    Console.WriteLine("Введите имя модуля: ");
                    string moduleName = Console.ReadLine();
                    GetModuleInfo(moduleName);
                    break;
                case "3":
                    Console.WriteLine("Введите полное имя модуля: ");
                    string fullModuleName = Console.ReadLine();
                    GetModuleInfo(fullModuleName);
                    break;
                case "4":
                    Console.WriteLine("Введите дескриптор модуля (в шестнадцатеричном формате): ");
                    string moduleHandleInput = Console.ReadLine();
                    IntPtr moduleHandle = new IntPtr(Convert.ToInt32(moduleHandleInput, 16));
                    GetModuleInfo(moduleHandle);
                    break;
                case "5":
                    Process process = Process.GetCurrentProcess();
                    Console.WriteLine($"Имя: {process.ProcessName}\n" +
                    $"ID: {process.Id}\n" +
                    $"Полное имя: {process.MainModule.FileName}\n" +
                    $"Дескриптор: {process.Handle}\n");
                    break;
                case "6":
                    Process[] processes = Process.GetProcesses();
                    foreach (Process p in processes)
                    {
                        Console.WriteLine($"Имя: {p.ProcessName}\n" +
                        $"ID: {p.Id}\n" +
                        $"Полное имя: {p.MainModule.FileName}\n" +
                        $"Дескриптор: {p.Handle}\n");
                    }
                    break;
                default:
                    return;
            }
        }
    }

    private static void GetModuleInfo(IntPtr moduleHandle)
    {
        throw new NotImplementedException();
    }

    static void GetModuleInfo(string moduleName)
    {
        IntPtr moduleHandle;
        if (moduleName != null)
        {
            moduleHandle = GetModuleHandle(moduleName);
        }
        else
        {
            moduleHandle = Process.GetCurrentProcess().MainModule.BaseAddress;
        }

        StringBuilder moduleNameBuilder = new StringBuilder(1024);
        int result = GetModuleFileName(moduleHandle, moduleNameBuilder, moduleNameBuilder.Capacity);

        string moduleNameString = moduleNameBuilder.ToString();
        IntPtr moduleHandleResult = moduleHandle;

        Console.WriteLine($"Имя: {moduleNameString}\n" +
        $"Дескриптор: {moduleHandleResult}\n");
         
    }
}

