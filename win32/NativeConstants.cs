using System;

namespace WindowsFormsApp1
{
    internal class NativeConstants
    {
        public const int WH_MOUSE_LL = 14;
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE = 7;
        public const int WH_KEYBOARD = 2;

        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_MBUTTONUP = 0x208;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_MBUTTONDBLCLK = 0x209;
        public const int WM_MOUSEWHEEL = 0x020A;

        public const int WM_VSCROLL = 0x0115;

        public const int MEF_LEFTDOWN = 0x00000002;
        public const int MEF_LEFTUP = 0x00000004;
        public const int MEF_MIDDLEDOWN = 0x00000020;
        public const int MEF_MIDDLEUP = 0x00000040;
        public const int MEF_RIGHTDOWN = 0x00000008;
        public const int MEF_RIGHTUP = 0x00000010;

        public const int KEF_EXTENDEDKEY = 0x1;
        public const int KEF_KEYUP = 0x2;

        public const byte VK_SHIFT = 0x10;
        public const byte VK_CAPITAL = 0x14;
        public const byte VK_NUMLOCK = 0x90;

        public const int WM_IME_SETCONTEXT = 0x0281;
        public const int WM_IME_COMPOSITION = 0x010F;
        public const int GCS_COMPSTR = 0x0008;

        //ShowWindow Arguments
        public const int SW_SHOWNORMAL = 1;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWNOACTIVATE = 4;

        //SendMessage Arguments
        public const int WM_KEYDOWN = 0X100;
        public const int WM_KEYUP = 0X101;
        public const int WM_SYSCHAR = 0X106;
        public const int WM_SYSKEYUP = 0X105;
        public const int WM_SYSKEYDOWN = 0X104;
        public const int WM_CHAR = 0X102;
        public const int WM_SETREDRAW = 0x000B;


        public const int DLGC_STATIC = 0x100;
        public const int GWL_EXSTYLE = -20;
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public const uint MOD_ALT = 1;
        public const uint MOD_CONTROL = 2;
        public const uint MOD_SHIFT = 4;
        public const uint OBJ_BITMAP = 7;
        public const int SRCCOPY = 0xcc0020;
        public const int SW_SHOWNA = 8;
        public const int SWP_NOACTIVATE = 0x10;
        public const int TOKEN_ELEVATION = 20;
        public const int TOKEN_ELEVATION_TYPE = 0x12;
        public const int TOKEN_ELEVATION_TYPE_DEFAULT = 1;
        public const int TOKEN_ELEVATION_TYPE_FULL = 2;
        public const int TOKEN_ELEVATION_TYPE_LIMITED = 3;
        public const int TOKEN_QUERY = 8;
        public const int VK_F1 = 0x70;
        public const uint VK_R = 0x52;
        public const int WM_GETDLGCODE = 0x87;
        public const int WM_HOTKEY = 0x312;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;
        public const int WS_EX_TOOLWINDOW = 0x80;
    }
}
