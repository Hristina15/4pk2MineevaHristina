#include <windows.h> 
#include <string>
#include "MathLib.h"

HWND hwnd;

// Прототип обработчика окна
LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

HWND hButton1;
HWND hButton2;
HWND hButton3;
HWND hButton4;
HWND hButton5;
HWND hButton6;
HWND hInput1;
HWND hInput2;
HWND hOutput;

// Главная функция
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    // Создание структуры WNDCLASSEX
    WNDCLASSEX wcex;
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WindowProc;
    wcex.cbClsExtra = 0;
    wcex.cbWndExtra = 0;
    wcex.hInstance = hInstance;
    wcex.hIcon = NULL;
    wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszMenuName = NULL;
    wcex.lpszClassName = L"MyWin32Class";
    wcex.hIconSm = NULL;

    // Регистрация класса окна
    if (!RegisterClassEx(&wcex))
    {
        MessageBox(NULL, L"Не удалось зарегистрировать класс окна.", L"Ошибка", MB_ICONERROR);
        return 1;
    }

    // Создание главного окна
    hwnd = CreateWindowEx(
        0,
        L"MyWin32Class",
        L"Классическое приложение Win32",
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT, 800, 600,
        NULL,
        NULL,
        hInstance,
        NULL
    );

    if (!hwnd)
    {
        MessageBox(NULL, L"Не удалось создать главное окно.", L"Ошибка", MB_ICONERROR);
        return 1;
    }

    //Создание кнопок
    hButton1 = CreateWindow(TEXT("BUTTON"), TEXT("+"), WS_VISIBLE | WS_CHILD,
        10, 10, 50, 30, hwnd, NULL, hInstance, NULL);
    hButton2 = CreateWindow(TEXT("BUTTON"), TEXT("-"), WS_VISIBLE | WS_CHILD,
        60, 10, 50, 30, hwnd, NULL, hInstance, NULL);
    hButton3 = CreateWindow(TEXT("BUTTON"), TEXT("*"), WS_VISIBLE | WS_CHILD,
        110, 10, 50, 30, hwnd, NULL, hInstance, NULL);
    hButton4 = CreateWindow(TEXT("BUTTON"), TEXT("/"), WS_VISIBLE | WS_CHILD,
        160, 10, 50, 30, hwnd, NULL, hInstance, NULL);
    hButton5 = CreateWindow(TEXT("BUTTON"), TEXT("pow"), WS_VISIBLE | WS_CHILD,
        210, 10, 50, 30, hwnd, NULL, hInstance, NULL);
    hButton6 = CreateWindow(TEXT("BUTTON"), TEXT("sqrt"), WS_VISIBLE | WS_CHILD,
        260, 10, 50, 30, hwnd, NULL, hInstance, NULL);

    //Поля ввода
    hInput1 = CreateWindow(TEXT("EDIT"), TEXT(""), WS_VISIBLE | WS_CHILD | WS_BORDER | ES_AUTOHSCROLL,
        10, 50, 200, 30, hwnd, NULL, hInstance, NULL);
    hInput2 = CreateWindow(TEXT("EDIT"), TEXT(""), WS_VISIBLE | WS_CHILD | WS_BORDER | ES_AUTOHSCROLL,
        10, 90, 200, 30, hwnd, NULL, hInstance, NULL);

    //Поле вывода
    hOutput = CreateWindow(TEXT("EDIT"), TEXT(""), WS_VISIBLE | WS_CHILD | WS_BORDER | ES_READONLY | ES_AUTOHSCROLL,
        10, 130, 200, 30, hwnd, NULL, hInstance, NULL);

    // Отображение и обновление главного окна
    ShowWindow(hwnd, nCmdShow);
    UpdateWindow(hwnd);

    // Цикл обработки сообщений
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}

HDC hDC; // Контекст устройства
PAINTSTRUCT ps;
RECT rect;
bool a = true;

// Обработчик окна
LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    WCHAR buffer[] = L"Выход из очереди";
    WCHAR buffer2[] = L"Заметка";
    WCHAR buffer3[] = L"Окно перемещено";
    WCHAR buffer4[] = L"Заметка";
    WCHAR buffer5[] = L"Вычисление математических функций";
    WCHAR buffer6[] = L"Заметка";

   
        switch (uMsg)
        {
        case WM_CLOSE:
            DestroyWindow(hwnd);
            break;
        case WM_RBUTTONDBLCLK:
            MessageBox(
                hwnd, // Родительское окно. Или NULL, если его нет.
                buffer, // Выводимое сообщение.
                buffer2, // Текст заголовка окна.
                MB_ICONINFORMATION); // Настройки кнопок и значка окна сообщения.
            return 1;
        case WM_DESTROY:
            PostQuitMessage(0);
            break;
        case WM_MOVE:
            MessageBox(
                hwnd, // Родительское окно. Или NULL, если его нет.
                buffer3, // Выводимое сообщение.
                buffer4, // Текст заголовка окна.
                MB_ICONINFORMATION); // Настройки кнопок и значка окна сообщения.
            break;
        case WM_ACTIVATE:
            if (a)
            {
                a = false;
                MessageBox(
                    hwnd, // Родительское окно. Или NULL, если его нет.
                    buffer5, // Выводимое сообщение.
                    buffer6, // Текст заголовка окна.
                    MB_ICONINFORMATION); // Настройки кнопок и значка окна сообщения.
            }
            break;
        case WM_COMMAND:
            //реакция на команды элементов управления
            if (HIWORD(wParam) == BN_CLICKED && (HWND)lParam == hButton1)
            {
                //получение значений из полей ввода
                TCHAR inputText1[256];
                GetWindowText(hInput1, inputText1, sizeof(inputText1));
                TCHAR inputText2[256];
                GetWindowText(hInput2, inputText2, sizeof(inputText2));
                //выполнение сложения
                double result;
                double value1 = std::stod(inputText1);
                double value2 = std::stod(inputText2);
                result = MathLibrary::Arithmetic::Add(value1, value2);
                //запись результата в поле вывода
                std::string outputText = std::to_string(result);
                SetWindowText(hOutput, (LPCWSTR)outputText.c_str());
            }
            if (HIWORD(wParam) == BN_CLICKED && (HWND)lParam == hButton2)
            {
                //получение значений из полей ввода
                TCHAR inputText1[256];
                GetWindowText(hInput1, inputText1, sizeof(inputText1));
                TCHAR inputText2[256];
                GetWindowText(hInput2, inputText2, sizeof(inputText2));
                //выполнение вычитания
                double result;
                double value1 = std::stod(inputText1);
                double value2 = std::stod(inputText2);
                result = MathLibrary::Arithmetic::Subtract(value1, value2);
                //запись результата в поле вывода
                std::string outputText = std::to_string(result);
                SetWindowText(hOutput, (LPCWSTR)outputText.c_str());
            }
            if (HIWORD(wParam) == BN_CLICKED && (HWND)lParam == hButton3)
            {
                //получение значений из полей ввода
                TCHAR inputText1[256];
                GetWindowText(hInput1, inputText1, sizeof(inputText1));
                TCHAR inputText2[256];
                GetWindowText(hInput2, inputText2, sizeof(inputText2));
                //выполнение умножения
                double result;
                double value1 = std::stod(inputText1);
                double value2 = std::stod(inputText2);
                result = MathLibrary::Arithmetic::Multiply(value1, value2);
                //запись результата в поле вывода
                std::string outputText = std::to_string(result);
                SetWindowText(hOutput, (LPCWSTR)outputText.c_str());
            }
            if (HIWORD(wParam) == BN_CLICKED && (HWND)lParam == hButton4)
            {
                //получение значений из полей ввода
                TCHAR inputText1[256];
                GetWindowText(hInput1, inputText1, sizeof(inputText1));
                TCHAR inputText2[256];
                GetWindowText(hInput2, inputText2, sizeof(inputText2));
                //выполнение деления
                double result;
                double value1 = std::stod(inputText1);
                double value2 = std::stod(inputText2);
                result = MathLibrary::Arithmetic::Divide(value1, value2);
                //запись результата в поле вывода
                std::string outputText = std::to_string(result);

                
                    SetWindowText(hOutput, (LPCWSTR)outputText.c_str());
            }
            if (HIWORD(wParam) == BN_CLICKED && (HWND)lParam == hButton5)
            {
                //получение значений из полей ввода
                TCHAR inputText1[256];
                GetWindowText(hInput1, inputText1, sizeof(inputText1));
                TCHAR inputText2[256];
                GetWindowText(hInput2, inputText2, sizeof(inputText2));
                //выполнение возведения в степень
                double result;
                double value1 = std::stod(inputText1);
                double value2 = std::stod(inputText2);
                result = MathLibrary::Arithmetic::Exponentiation(value1, value2);
                //запись результата в поле вывода
                std::string outputText = std::to_string(result);
                SetWindowText(hOutput, (LPCWSTR)outputText.c_str());
            }
            if (HIWORD(wParam) == BN_CLICKED && (HWND)lParam == hButton6)
            {
                //получение значений из полей ввода
                TCHAR inputText1[256];
                GetWindowText(hInput1, inputText1, sizeof(inputText1));
                TCHAR inputText2[256];
                GetWindowText(hInput2, inputText2, sizeof(inputText2));
                //выполнение извлечения корня
                double result;
                double value1 = std::stod(inputText1);
                double value2 = std::stod(inputText2);
                result = MathLibrary::Arithmetic::Root(value1, value2);
                //запись результата в поле вывода
                std::string outputText = std::to_string(result);
                SetWindowText(hOutput, (LPCWSTR)outputText.c_str());
            }
            break;

        case WM_PAINT:
            hDC = BeginPaint(hwnd, &ps);
            GetClientRect(hwnd, &rect);
            DrawText(hDC, TEXT("Математические функции"), -1, &rect, DT_SINGLELINE |
                DT_CENTER | DT_VCENTER);
            EndPaint(hwnd, &ps);
            return 0;
        default:
            return DefWindowProc(hwnd, uMsg, wParam, lParam);
        }

    return 0;
}
