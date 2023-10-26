#include <windows.h>

HWND hwnd;

// Прототип обработчика окна
LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

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
        L"Мое классическое приложение Win32",
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
    WCHAR buffer[] = L"Привет1";
    WCHAR buffer2[] = L"Привет2";
    WCHAR buffer3[] = L"Привет3";
    WCHAR buffer4[] = L"Привет4";
    WCHAR buffer5[] = L"Привет5";
    WCHAR buffer6[] = L"Привет6";

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
    case WM_PAINT:
        hDC = BeginPaint(hwnd, &ps);
        GetClientRect(hwnd, &rect);
        DrawText(hDC, TEXT("ваш текст!"), -1, &rect, DT_SINGLELINE |
            DT_CENTER | DT_VCENTER);
        EndPaint(hwnd, &ps);
        return 0;
    default:
        return DefWindowProc(hwnd, uMsg, wParam, lParam);
    }

    return 0;
}