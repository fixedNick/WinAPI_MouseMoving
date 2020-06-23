using System.Runtime.InteropServices;

namespace WinAPI_1
{
    class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int x, int y);
    } 
}
