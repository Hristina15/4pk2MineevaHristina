// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using System.Runtime.InteropServices;

class Program
{
    // Импорт функций Win32 API
    [DllImport("kernel32.dll")]
    static extern void InitializeCriticalSection(out IntPtr lpCriticalSection);

    [DllImport("kernel32.dll")]
    static extern void EnterCriticalSection(IntPtr lpCriticalSection);

    [DllImport("kernel32.dll")]
    static extern void LeaveCriticalSection(IntPtr lpCriticalSection);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool initialOwner, string lpName);

    [DllImport("kernel32.dll")]
    static extern bool ReleaseMutex(IntPtr hMutex);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

    [DllImport("kernel32.dll")]
    static extern IntPtr OpenEvent(uint dwDesiredAccess, bool bInheritHandle, string lpName);

    [DllImport("kernel32.dll")]
    static extern bool SetEvent(IntPtr hEvent);

    [DllImport("kernel32.dll")]
    static extern bool ResetEvent(IntPtr hEvent);

    [DllImport("kernel32.dll")]
    static extern bool CloseHandle(IntPtr hObject);

    static IntPtr criticalSection;
    static IntPtr mutex;
    static IntPtr eventHandle;

    static void Main()
    {
        // Пример использования критической секции
        InitializeCriticalSection(out criticalSection);
        Thread t1 = new Thread(UseCriticalSection);
        Thread t2 = new Thread(UseCriticalSection);
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
        Console.WriteLine("Использование критической секции завершено");

        // Пример использования мьтекса
        mutex = CreateMutex(IntPtr.Zero, false, "MyMutex");
        Thread t3 = new Thread(UseMutex);
        Thread t4 = new Thread(UseMutex);
        t3.Start();
        t4.Start();
        t3.Join();
        t4.Join();
        CloseHandle(mutex);
        Console.WriteLine("Использование мьтекса завершено");

        // Пример использования события
        eventHandle = CreateEvent(IntPtr.Zero, true, false, "MyEvent");
        Thread t5 = new Thread(UseEvent);
        Thread t6 = new Thread(WaitForEvent);
        t5.Start();
        t6.Start();
        t5.Join();
        t6.Join();
        CloseHandle(eventHandle);
        Console.WriteLine("Использование события завершено");
    }

    static void UseCriticalSection()
    {

        Console.WriteLine("Критическая секция: Поток " + Thread.CurrentThread.ManagedThreadId + " вошел");
        Thread.Sleep(1000); // Simulate some work

    }

    static void UseMutex()
    {
        if (mutex != IntPtr.Zero)
        {
            if (WaitOne(mutex))
            {
                Console.WriteLine("Мьтекс: Поток " + Thread.CurrentThread.ManagedThreadId + " заблокировал мьтекс");
                Thread.Sleep(1000); // Simulate some work
                ReleaseMutex(mutex);
            }
        }
    }

    static void UseEvent()
    {
        SetEvent(eventHandle);
        Console.WriteLine("Событие: Событие установлено");
    }

    static void WaitForEvent()
    {
        if (eventHandle != IntPtr.Zero)
        {
            if (WaitOne(eventHandle))
            {
                Console.WriteLine("Событие: Поток " + Thread.CurrentThread.ManagedThreadId + " получил сигнал");
                Thread.Sleep(1000); // Simulate some work
                ResetEvent(eventHandle);
            }
        }
    }

    static bool WaitOne(IntPtr handle)
    {
        return WaitForSingleObject(handle, 0xFFFFFFFF) == 0;
    }

    const uint WAIT_TIMEOUT = 0x00000102;
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
}
