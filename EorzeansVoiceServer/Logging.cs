using System;
using System.IO;

namespace EorzeansVoiceServer {
	public static class Logging {
		public enum LogType : int {
			Debug = 0,
			Info = 1,
			Warn = 2,
			Error = 3
		}

		public static LogType console;
		public static LogType file;
		public static string fileName;

		private static void Log(LogType type, string message) {
			if ((int)console <= (int)type) {
				Console.WriteLine(Prefix(type) + message);
			}

			if ((int)file <= (int)type) {
				string fname = fileName + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
				File.AppendAllLinesAsync(fname, new string[] { Prefix(type) + message });
			}
		}

		private static string Prefix(LogType type) {
			return "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "][" + type.ToString() + "] ";
		}

		public static void Debug(string message) {
			Log(LogType.Debug, message);
		}

		public static void Info(string message) {
			Log(LogType.Info, message);
		}

		public static void Warn(string message) {
			Log(LogType.Warn, message);
		}

		public static void Error(string message) {
			Log(LogType.Error, message);
		}
	}
}
