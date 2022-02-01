using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EorzeansVoice {
	public static class HotkeyController {
		public class KeyAction {
			public Keys key;
			public KeyUpDown upDown;
			public bool shift;
			public bool control;
			public bool alt;
			[JsonIgnore]
			public Action callbackDown;
			[JsonIgnore]
			public Action callbackUp;
		}

		[Flags]
		public enum KeyUpDown {
			KeyUp,
			KeyDown
		}

		[DllImport("user32.dll")]
		internal static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);
		[DllImport("user32.dll")]
		internal static extern bool UnhookWindowsHookEx(IntPtr hInstance);
		[DllImport("user32.dll")]
		internal static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr LoadLibrary(string lpFileName);

		internal delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);

		internal struct KeyboardHookStruct {
			public int vkCode;
			public int scanCode;
			public int flags;
			public int time;
			public int dwExtraInfo;
		}

		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x100;
		private const int WM_KEYUP = 0x101;
		private const int WM_SYSKEYDOWN = 0x104;
		private const int WM_SYSKEYUP = 0x105;

		public static readonly List<KeyAction> hookedKeys = new List<KeyAction>();

		private static readonly Keys[] control = { Keys.LControlKey, Keys.RControlKey };
		private static readonly Keys[] shift = { Keys.LShiftKey, Keys.RShiftKey };
		private static readonly Keys[] alt = { Keys.LMenu, Keys.RMenu };

		private static IntPtr hhook = IntPtr.Zero;
		private static KeyboardHookProc SAFE_delegate_callback;
		private static bool controlDown = false;
		private static bool shiftDown = false;
		private static bool altDown = false;

		public static void StartListening() {
			IntPtr hInstance = LoadLibrary("User32");
			SAFE_delegate_callback = new KeyboardHookProc(HookProc);
			hhook = SetWindowsHookEx(WH_KEYBOARD_LL, SAFE_delegate_callback, hInstance, 0);
		}

		private static int HookProc(int code, int wParam, ref KeyboardHookStruct lParam) {
			try {
				if (code >= 0) { // idk
					Keys key = (Keys)lParam.vkCode;

					if (control.Contains(key)) {
						if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) {
							controlDown = true;
						} else if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP) {
							controlDown = false;
						}
					} else if (shift.Contains(key)) {
						if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) {
							shiftDown = true;
						} else if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP) {
							shiftDown = false;
						}
					} else if (alt.Contains(key)) {
						if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) {
							altDown = true;
						} else if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP) {
							altDown = false;
						}
					} else {
						foreach (KeyAction ka in hookedKeys) {
							if (ka.key == key && ka.control == controlDown && ka.shift == shiftDown && ka.alt == altDown) {
								if (ka.upDown.HasFlag(KeyUpDown.KeyDown) && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)) {
									ka.callbackDown?.Invoke();
								}
								if (ka.upDown.HasFlag(KeyUpDown.KeyUp) && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)) {
									ka.callbackUp?.Invoke();
								}
							}
						}
					}
				}

				return CallNextHookEx(hhook, code, wParam, ref lParam);
			} catch { return 0; }
		}

		public static void StopListening() {
			UnhookWindowsHookEx(hhook);
		}
	}
}
