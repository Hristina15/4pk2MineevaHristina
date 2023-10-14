#include <iostream>
#include <windows.h>

int main() {
    setlocale(LC_ALL, "Russian");
    MEMORYSTATUSEX memoryStatus;
    memoryStatus.dwLength = sizeof(memoryStatus);

    GlobalMemoryStatusEx(&memoryStatus);

    DWORDLONG physicalMemory = memoryStatus.ullTotalPhys;
    DWORDLONG availableMemory = memoryStatus.ullAvailPhys;
    DWORDLONG pageFile = memoryStatus.ullTotalPageFile;
    DWORDLONG availablePageFile = memoryStatus.ullAvailPageFile;
    DWORDLONG virtualMemory = memoryStatus.ullTotalVirtual;
    DWORDLONG availableVirtualMemory = memoryStatus.ullAvailVirtual;
    DWORD processMemory = memoryStatus.dwMemoryLoad;

    std::cout << "Объем физической памяти: " << physicalMemory << " байт" << std::endl;
    std::cout << "Объем памяти, доступной в данный момент: " << availableMemory << " байт" << std::endl;
    std::cout << "Объем файла подкачки: " << pageFile << " байт" << std::endl;
    std::cout << "Объем файла подкачки, доступного в данный момент: " << availablePageFile << " байт" << std::endl;
    std::cout << "Всего виртуальной памяти: " << virtualMemory << " байт" << std::endl;
    std::cout << "Объем виртуальной памяти, доступной в данный момент: " << availableVirtualMemory << " байт" << std::endl;
    std::cout << "Объем памяти, используемой процессом: " << processMemory << "%" << std::endl;

    return 0;
}