using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_MAXIMIZE = 3;

    static void Main()
    {
        ShowWindow(GetConsoleWindow(), SW_MAXIMIZE);
        Console.WriteLine("Hello, World!");
    }
}
