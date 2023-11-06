using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll")]
    public static extern bool GetComputerNameEx(COMPUTER_NAME_FORMAT nameType, char[] buffer, ref uint bufferSize);

    [DllImport("kernel32.dll")]
    public static extern void GetSystemDirectory(char[] buffer, int size);

    [DllImport("kernel32.dll")]
    public static extern void GetWindowsDirectory(char[] buffer, int size);

    [DllImport("kernel32.dll")]
    public static extern bool GetVersionEx(ref OSVERSIONINFO osvi);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(SystemMetric smIndex);

    [DllImport("user32.dll")]
    public static extern int GetCaretBlinkTime();

    [DllImport("user32.dll")]
    public static extern int SystemParametersInfo(int uiAction, int uiParam, ref MOUSE_PARAMETERS pvParam, int fWinIni);

    [DllImport("user32.dll")]
    public static extern int GetSysColor(int nIndex);

    [DllImport("kernel32.dll")]
    public static extern void GetLocalTime(ref SYSTEMTIME lpSystemTime);

    public enum COMPUTER_NAME_FORMAT
    {
        ComputerNameNetBIOS,
        ComputerNameDnsHostname,
        ComputerNameDnsDomain,
        ComputerNameDnsFullyQualified,
        ComputerNamePhysicalNetBIOS,
        ComputerNamePhysicalDnsHostname,
        ComputerNamePhysicalDnsDomain,
        ComputerNamePhysicalDnsFullyQualified
    }

    public enum SystemMetric
    {
        SM_CXSCREEN = 0,
        SM_CYSCREEN = 1,
    }

    public struct OSVERSIONINFO
    {
        public int dwOSVersionInfoSize;
        public int dwMajorVersion;
        public int dwMinorVersion;
        public int dwBuildNumber;
        public int dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
    }

    public struct MOUSE_PARAMETERS
    {
        public int cbSize;
        public int dwFlags;
        public int iMaxSpeed;
        public int iMinSpeed;
        public int iAccelSpeed;
        public int iThreshold;
        public int bSwapButtons;
    }

    public struct SYSTEMTIME
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
    }

    public struct NUMBERFMT
    {
        public int NumDigits;
        public int LeadingZero;
        public int Grouping;
        public string lpDecimalSep;
        public string lpThousandSep;
        public int NegativeOrder;
    }
    static void Main(string[] args)
    {
        // Имя компьютера
        string computerName = Environment.MachineName;

        // Имя пользователя
        string userName = Environment.UserName;

        // Путь к системному каталогу
        char[] systemDirectory = new char[260];
        GetSystemDirectory(systemDirectory, systemDirectory.Length);

        // Путь к каталогу Windows
        char[] windowsDirectory = new char[260];
        GetWindowsDirectory(windowsDirectory, windowsDirectory.Length);

        // Версия операционной системы
        OSVERSIONINFO osVersionInfo = new OSVERSIONINFO();
        osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFO));
        GetVersionEx(ref osVersionInfo);
        string osVersion = osVersionInfo.dwMajorVersion + "." + osVersionInfo.dwMinorVersion;

        // Системные метрики
        int screenWidth = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        int screenHeight = GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        // Системные параметры
        int caretBlinkTime = GetCaretBlinkTime();
        MOUSE_PARAMETERS mouseParameters = new MOUSE_PARAMETERS();
        mouseParameters.cbSize = Marshal.SizeOf(typeof(MOUSE_PARAMETERS));
        SystemParametersInfo(0x0061, mouseParameters.cbSize, ref mouseParameters, 0);

        // Системные цвета
        int highlightColor = GetSysColor(13); // Цвет выделения
                                              // Изменение цвета

        // Функции для работы со временем
        SYSTEMTIME localTime = new SYSTEMTIME();
        GetLocalTime(ref localTime);
        string currentTime = localTime.wHour.ToString("D2") + ":" +
                             localTime.wMinute.ToString("D2") + ":" +
                             localTime.wSecond.ToString("D2");

        // Вывод полученной информации
        Console.WriteLine("Имя компьютера: " + computerName);
        Console.WriteLine("Имя пользователя: " + userName);
        Console.WriteLine("Путь к системному каталогу: " + new string(systemDirectory));
        Console.WriteLine("Путь к каталогу Windows: " + new string(windowsDirectory));
        Console.WriteLine("Версия операционной системы: " + osVersion);
        Console.WriteLine("Системные метрики - Ширина экрана: " + screenWidth + ", Высота экрана: " + screenHeight);
        Console.WriteLine("Системные параметры - Время мигания курсора: " + caretBlinkTime + ", Параметры мыши: " + mouseParameters.dwFlags);
        Console.WriteLine("Системный цвет - Цвет выделения: " + highlightColor);
        Console.WriteLine("Текущее время: " + currentTime);

        Console.ReadKey();
    }
}
