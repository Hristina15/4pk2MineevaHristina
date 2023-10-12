#include <iostream>
#include <thread>
#include <mutex>
#include <condition_variable>

std::mutex mtx; // примитив синхронизации, который предоставляет монопольный доступ к общему ресурсу только одному потоку
std::condition_variable cv;
bool isValueReached = false;

void thread1()
{
    while (true)
    {
        std::unique_lock<std::mutex> lock(mtx);
        cv.wait(lock, [] { return isValueReached; });
        std::cout << "Поток 1: Получено значение\n";
        isValueReached = false;
        lock.unlock();
        cv.notify_one();
    }
}

void thread2()
{
    int value = 1;
    while (value < 100)
    {
        std::unique_lock<std::mutex> lock(mtx);
        if (value == 42)
        {
            std::cout << "Поток 2: Достигнуто критическое значение, аварийное завершение\n";
            std::terminate();
        }
        std::cout << "Поток 2: Генерируется прогрессия - " << value << "\n";
        value++;
        isValueReached = true;
        lock.unlock();
        cv.notify_one();
    }
}

int main()
{
    setlocale(LC_ALL, "Russian"); 
    std::thread t1(thread1);
    std::thread t2(thread2);

    t1.join();
    t2.join();

    return 0;
}