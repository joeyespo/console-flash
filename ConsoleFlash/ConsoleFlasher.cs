using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ConsoleFlash
{
    /// <summary>
    /// Provides methods to flash the console window.
    /// </summary>
    public static class ConsoleFlasher
    {
        /// <summary>
        /// Flashes the console window until it is focused. If the window currently has focus, this method does nothing.
        /// </summary>
        /// <param name="count">The number of times to flash the window before keeping it inverted.</param>
        /// <param name="rate">The flash rate in milliseconds. A value of zero indicates the system default.</param>
        /// <param name="delay">The number of milliseconds to wait before flashing.</param>
        /// <exception cref="InvalidOperationException">A console window is not currently attached to the current process.</exception>
        public static void FlashConsoleWindow(int count = 3, int rate = 0, int delay = 0)
        {
            if(delay > 0)
                Thread.Sleep(delay);
            IntPtr hWnd = GetConsoleWindowHandle();
            if(GetForegroundWindow() == hWnd)
                return;
            FlashWindow(hWnd, count: (uint)count, rate: (uint)rate);
        }

        #region Helper Methods

        /// <summary>
        /// Gets the console window handle.
        /// </summary>
        /// <exception cref="InvalidOperationException">A console window is not currently attached to the current process.</exception>
        private static IntPtr GetConsoleWindowHandle()
        {
            IntPtr hWnd = GetConsoleWindow();

            // TODO: Fallback, if no console window?
            /*
            if(hWnd == IntPtr.Zero && fallbackToParentProcess)
            {
                // Try attaching to the parent process's console, if it exists
                if(AttachConsole(ATTACH_PARENT_PROCESS) != 0)
                    hWnd = GetConsoleWindow();
            }
            */

            // Check whether the console window can be found
            if(hWnd == IntPtr.Zero)
                throw new InvalidOperationException("No console window was found.");

            return hWnd;
        }

        /// <summary>
        /// Flashes the window.
        /// </summary>
        private static bool FlashWindow(IntPtr hWnd, FlashWindowFlags options = (FlashWindowFlags.FLASHW_ALL | FlashWindowFlags.FLASHW_TIMERNOFG), uint count = 3, uint rate = 0)
        {
            if(hWnd == IntPtr.Zero)
                return false;
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = (uint)Marshal.SizeOf(typeof(FLASHWINFO));
            fi.dwFlags = options;
            fi.uCount = count;
            fi.dwTimeout = rate;
            fi.hwnd = hWnd;
            return FlashWindowEx(ref fi);
        }

        #endregion

        #region Win32 Memebers

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus();

        [DllImport("kernel32", SetLastError = false)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32", SetLastError = true)]
        private static extern int AttachConsole(int processId);

        private const int ATTACH_PARENT_PROCESS = -1;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            /// <summary>
            /// The size of the structure in bytes.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// The Flash Status.
            /// </summary>
            public FlashWindowFlags dwFlags;

            /// <summary>
            /// The number of times to Flash the window.
            /// </summary>
            public uint uCount;

            /// <summary>
            /// The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
            /// </summary>
            public uint dwTimeout;
        }

        private enum FlashWindowFlags : uint
        {
            /// <summary>
            /// Stop flashing. The system restores the window to its original state.
            /// </summary>
            FLASHW_STOP = 0,

            /// <summary>
            /// Flash the window caption.
            /// </summary>
            FLASHW_CAPTION = 1,

            /// <summary>
            /// Flash the taskbar button.
            /// </summary>
            FLASHW_TRAY = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
            /// </summary>
            FLASHW_ALL = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            FLASHW_TIMER = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground.
            /// </summary>
            FLASHW_TIMERNOFG = 12
        }

        #endregion
    }
}
