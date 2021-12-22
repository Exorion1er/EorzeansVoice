using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EorzeansVoice {
	public static class MemoryReader {
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(IntPtr hProcess, ulong lpBaseAddress, byte[] lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

		public static T? GetValue<T>(Process p, ulong offset) where T : struct {
			int length = Marshal.SizeOf(typeof(T));
			byte[] data = ReadMemory(p, offset, length);

			GCHandle pinnedStruct = GCHandle.Alloc(data, GCHandleType.Pinned);
			try {
				return Marshal.PtrToStructure<T>(pinnedStruct.AddrOfPinnedObject());
			} catch (Exception e) {
				MessageBox.Show("Error converting read memory : " + e.Message);
				return null;
			} finally {
				pinnedStruct.Free();
			}
		}

		public static string GetString(Process p, ulong offset, int maxLength) {
			string final = "";
			for (int i = 1; i < maxLength; i++) {
				byte[] data = ReadMemory(p, offset, i);
				if (data[(i-1)] == 0x00) {
					return final;
				}
				final = Encoding.ASCII.GetString(data);
			}
			return final;
		}

		private static byte[] ReadMemory(Process p, ulong offset, int length) {
			byte[] data = new byte[length];

			try {
				ulong address = (ulong)p.MainModule.BaseAddress + offset;
				ReadProcessMemory(p.Handle, address, data, data.Length, IntPtr.Zero);
			} catch (Exception e) {
				MessageBox.Show("Error reading memory : " + e.Message);
			}

			return data;
		}
	}
}
