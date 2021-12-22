using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace EorzeansVoice {
	public static class ProcessFinder {
		public class Result {
			public ResultType type;
			public FFXIVProcess process;
		}

		public enum ResultType {
			Found,
			NoneAvailable,
			ClosedSelector
		}

		public class FFXIVProcess {
			public Process process;
			public string name;
			public bool found;
			public bool def;

			public override string ToString() {
				if (!def) {
					return process.ProcessName + " (" + process.Id + ") - " + name;
				}
				return "Please open FFXIV.";
			}
		}

		public const string gameProcessName = "ffxiv_dx11";

		public static Result GetProcess() {
			Process[] processes = Process.GetProcessesByName(gameProcessName);
			Result result = new Result();

			if (processes.Length == 0) {
				result.type = ResultType.NoneAvailable;
				return result;
			} else if (processes.Length > 1) {
				ProcessSelectionWindow form = new ProcessSelectionWindow();
				DialogResult dialog = form.ShowDialog();

				if (dialog == DialogResult.OK) {
					result.process = form.foundProcess;
					result.type = ResultType.Found;
					return result;
				} else {
					result.type = ResultType.ClosedSelector;
					return result;
				}
			}

			result.type = ResultType.Found;
			result.process = GetProcessInformation(processes[0]);
			return result;
		}

		public static FFXIVProcess GetProcessInformation(Process p) {
			string name = "Logged off";
			if (GameData.IsLoggedIn(p)) {
				name = GameData.GetName(p);
			}

			FFXIVProcess newP = new FFXIVProcess {
				process = p,
				name = name
			};

			return newP;
		}

		public static string RefreshName(FFXIVProcess p) {
			string name = "Logged off";
			if (GameData.IsLoggedIn(p.process)) {
				name = GameData.GetName(p.process);
			}
			return name;
		}
	}
}
