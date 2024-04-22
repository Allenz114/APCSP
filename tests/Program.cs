using System;
using System.Runtime.InteropServices;
using System.Media;

class Program
{
    [DllImport("winmm.dll", SetLastError = true)]
    static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);

    static void Main()
    {
        PlaySound(@"C:\Users\28162\Desktop\C#\APCSP\tests\bin\Debug\net8.0\Ji.wav", UIntPtr.Zero, 0x00020000);
    }
}
