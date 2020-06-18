using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection
{
    public enum MouseKeys
    {
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,

        WM_XBUTTONDBLCLK = 0x020D,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C   ,
    }
    static class Hook
    {
        
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate void MouseDelegate(IntPtr code);

        const int WH_KEYBOARD_LL = 13; // Номер глобального LowLevel-хука на клавиатуру
        const int WH_MOUSE_LL = 14; // Номер глобального LowLevel-хука на клавиатуру
        const int WM_KEYDOWN = 0x100; // Сообщения нажатия клавиши

        public static  LowLevelKeyboardProc _kproc = hookKeyboardProc;
        public static LowLevelKeyboardProc _mproc = hookMouseProc;

        private static IntPtr hhookk = IntPtr.Zero;
        private static IntPtr hhookm = IntPtr.Zero;
        public static List<IntPtr> MouseHooks = new List<IntPtr>();
        public static MouseDelegate OnMouseEvent;

        
        public static void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhookk = SetWindowsHookEx(WH_KEYBOARD_LL, _kproc, hInstance, 0);
            hhookm = SetWindowsHookEx(WH_MOUSE_LL, _mproc, hInstance, 0);
            Console.WriteLine(hhookm);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhookk);
            UnhookWindowsHookEx(hhookm);
        }


        public static IntPtr hookKeyboardProc(int code, IntPtr wParam, IntPtr lParam)
        {
            
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Console.WriteLine(vkCode);
                return CallNextHookEx(hhookk, code, (int)wParam, lParam);
            }
            else
                return CallNextHookEx(hhookk, code, (int)wParam, lParam);
        }
        public static IntPtr hookMouseProc(int code, IntPtr wParam, IntPtr lParam)
        {
            
            if (code >= 0 && MouseHooks.Contains(wParam))
            {
                OnMouseEvent?.Invoke(wParam);
                int vkCode = Marshal.ReadInt32(lParam);
                return CallNextHookEx(hhookm, code, (int)wParam, lParam);
            }
            else
                return CallNextHookEx(hhookm, code, (int)wParam, lParam);
        }


    }
}

