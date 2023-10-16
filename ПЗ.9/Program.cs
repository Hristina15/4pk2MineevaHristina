using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ПЗ._9
{
    class Program
    {
        // Задаем константы для размера страницы памяти
        const int PageSize = 4096; // размер страницы в байтах

        // Импортируем функции из библиотеки kernel32.dll
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern void ZeroMemory(IntPtr addr, uint size);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern void FillMemory(IntPtr addr, uint size, byte value);

        static void Main()
        {
            // Размер региона памяти (2 страницы)
            uint regionSize = 2 * PageSize;

            // Резервируем память для первого региона
            IntPtr region1 = VirtualAlloc(IntPtr.Zero, regionSize, 0x1000, 0x4);
            if (region1 == IntPtr.Zero)
            {
                Console.WriteLine("Ошибка при резервировании памяти для региона 1");
                return;
            }

            // Резервируем память для второго региона
            IntPtr region2 = VirtualAlloc(IntPtr.Zero, regionSize, 0x1000, 0x4);
            if (region2 == IntPtr.Zero)
            {
                Console.WriteLine("Ошибка при резервировании памяти для региона 2");
                return;
            }

            Console.WriteLine($"Адрес первого региона: {region1}");
            Console.WriteLine($"Адрес второго региона: {region2}");

            // Обнуляем данные в первом регионе
            ZeroMemory(region1, regionSize);

            // Считываем целое число от пользователя
            Console.Write("Введите целое число в диапазоне 0..127: ");
            int number = int.Parse(Console.ReadLine());

            // Заполняем второй регион числом, преобразованным в байт
            byte value = (byte)number;
            FillMemory(region2, regionSize, value);

            // Отображаем содержимое регионов
            Console.WriteLine("Содержимое первого региона:");
            ReadMemory(region1, regionSize);

            Console.WriteLine("Содержимое второго региона:");
            ReadMemory(region2, regionSize);

            Console.ReadLine();
        }

        static void ReadMemory(IntPtr address, uint size)
        {
            byte[] buffer = new byte[size];
            Marshal.Copy(address, buffer, 0, (int)size);

            for (int i = 0; i < buffer.Length; i++)
            {
                Console.WriteLine("Адрес: {0}, Значение: {1}", address + i, buffer[i]);
            }
        }
    }
}
