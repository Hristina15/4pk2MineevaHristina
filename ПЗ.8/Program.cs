using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ПЗ._8
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr HeapCreate(uint flOptions, UIntPtr dwInitialSize, UIntPtr dwMaximumSize);
        static void Main(string[] args)
        {
            // Вызываем HeapCreate для создания heap
            IntPtr heap = HeapCreate(0, UIntPtr.Zero, UIntPtr.Zero);

            // Проверяем успешность вызова HeapCreate
            if (heap != IntPtr.Zero)
            {
                Console.WriteLine("Успешно создано!");

                // Запускаем потоки
                Thread thread1 = new Thread(LongRunningAction);
                Thread thread2 = new Thread(LongRunningAction);
                thread1.Start();
                thread2.Start();

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Не удалось создать!");
            }
        }

        static void LongRunningAction()  // Выполняем бесконечный цикл
        {
            
            while (true)
            {
                Console.WriteLine("Бесконечный цикл!");
            }
        }
    }

}


