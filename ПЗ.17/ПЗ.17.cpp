#include <iostream>
#include <Windows.h>

void PrintRegistryValue(HKEY hKey, LPCWSTR subKey, LPCWSTR valueName) {
    HKEY hSubKey;
    if (RegOpenKeyEx(hKey, subKey, 0, KEY_READ, &hSubKey) == ERROR_SUCCESS) {
        DWORD dataType;
        DWORD dataSize = MAX_PATH;
        WCHAR data[MAX_PATH];

        if (RegQueryValueEx(hSubKey, valueName, nullptr, &dataType, reinterpret_cast<LPBYTE>(data), &dataSize) == ERROR_SUCCESS) {
            if (dataType == REG_SZ) {
                std::wcout << data << '\n';
            }
        }

        RegCloseKey(hSubKey);
    }
}

int main() {
    // Информация об ОС
    std::wcout << "OS information:\n";
    std::wcout << "Project Name: ";
    PrintRegistryValue(HKEY_LOCAL_MACHINE, L"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", L"ProductName");
    std::wcout << "Release Windows: ";
    PrintRegistryValue(HKEY_LOCAL_MACHINE, L"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", L"CurrentBuild");
    std::wcout << "Build Number: ";
    PrintRegistryValue(HKEY_LOCAL_MACHINE, L"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", L"BuildLabEx");
    std::wcout << "System folder: ";
    PrintRegistryValue(HKEY_LOCAL_MACHINE, L"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", L"SystemRoot");

    // Информация о БИОСе
    std::wcout << "\nBIOS information:\n";
    std::wcout << "BIOSVendor: ";
    PrintRegistryValue(HKEY_LOCAL_MACHINE, L"HARDWARE\\DESCRIPTION\\System\\BIOS", L"BIOSVendor");
    std::wcout << "BIOSVersion: ";
    PrintRegistryValue(HKEY_LOCAL_MACHINE, L"HARDWARE\\DESCRIPTION\\System\\BIOS", L"BIOSVersion");
    std::wcout << "SystemManufacturer: ";
    PrintRegistryValue(HKEY_LOCAL_MACHINE, L"HARDWARE\\DESCRIPTION\\System\\BIOS", L"SystemManufacturer");

    // Информация о приложениях из автозапуска
    std::wcout << "\nInformation about applications from autorun:\n";
    HKEY hKey;
    if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", 0, KEY_READ, &hKey) == ERROR_SUCCESS) {
        DWORD subKeysCount;
        DWORD maxSubKeyLen;
        DWORD subKeyLen;
        if (RegQueryInfoKey(hKey, nullptr, nullptr, nullptr, &subKeysCount, &maxSubKeyLen, nullptr, nullptr, nullptr, nullptr, nullptr, nullptr) == ERROR_SUCCESS) {
            WCHAR subKeyName[MAX_PATH];
            for (DWORD i = 0; i < subKeysCount; ++i) {
                subKeyLen = maxSubKeyLen + 1;
                if (RegEnumKeyEx(hKey, i, subKeyName, &subKeyLen, nullptr, nullptr, nullptr, nullptr) == ERROR_SUCCESS) {
                    std::wcout << subKeyName << '\n';
                }
            }
        }
        RegCloseKey(hKey);
    }

    return 0;
}
