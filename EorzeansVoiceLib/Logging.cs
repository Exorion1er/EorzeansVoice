using System;
using System.Collections.Generic;
using System.IO;

namespace EorzeansVoiceLib {
	public static class Logging {
		private struct Logger {
			public Logger (LogType type, LogLevel level, string fileName) {
				this.type = type;
				this.level = level;
				this.fileName = fileName;
			}

			public LogType type;
			public LogLevel level;
			public string fileName;
		}

		public enum LogType : int {
			Console = 0,
			File = 1
		}

		public enum LogLevel : int {
			Debug = 0,
			Info = 1,
			Warn = 2,
			Error = 3
		}

		private static readonly List<Logger> loggers = new List<Logger>();

		public static void AddLogger(LogType type, LogLevel level, string fileName = "Log") {
			loggers.Add(new Logger(type, level, fileName));
		}

		private static void Log(LogLevel level, string message) {
			foreach (Logger logger in loggers) {
				if ((int)logger.level <= (int)level) {
					switch (logger.type) {
						case LogType.Console:
							LogConsole(level, message);
							break;
						case LogType.File:
							LogFile(level, message, logger.fileName);
							break;
					}
				}
			}
		}

		private static void LogConsole(LogLevel level, string message) {
			Console.WriteLine(Prefix(level) + message);
		}

		private static void LogFile(LogLevel level, string message, string fileName) {
			string finalName = fileName + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
			File.AppendAllLinesAsync(finalName, new string[] { Prefix(level) + message });
		}

		private static string Prefix(LogLevel type) {
			return "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "][" + type.ToString() + "] ";
		}

		public static void Debug(string message) {
			Log(LogLevel.Debug, message);
		}

		public static void Info(string message) {
			Log(LogLevel.Info, message);
		}

		public static void Warn(string message) {
			Log(LogLevel.Warn, message);
		}

		public static void Error(string message) {
			Log(LogLevel.Error, message);
		}
	}
}
