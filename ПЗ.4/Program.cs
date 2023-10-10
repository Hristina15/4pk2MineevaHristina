class Program
{
    private static object lockObject = new object();
    static void Main(string[] args)
    {
        // Создаем несколько потоков, которые будут выполнять код одновременно
        for (int i = 1; i <= 5; i++)
        {
            Thread thread = new Thread(SaveData);
            thread.Name = "Thread " + i;
            thread.Start();
        }

        Console.ReadLine();
    }
    private static void SaveData()
    {
        Console.WriteLine(Thread.CurrentThread.Name + " начал выполнение.");


        lock (lockObject) // Блокируем код для выполнения в единственном экземпляре
        {
            Console.WriteLine(Thread.CurrentThread.Name + " вошел в критическую секцию.");

            // Критическая секция кода
            Console.WriteLine(Thread.CurrentThread.Name + " выполняет работу...");
            Thread.Sleep(1000); // Имитируем выполнение работы

            Console.WriteLine(Thread.CurrentThread.Name + " вышел из критической секции.");
        }

        Console.WriteLine(Thread.CurrentThread.Name + " завершил выполнение.");
        Console.ReadLine();
    }
}





