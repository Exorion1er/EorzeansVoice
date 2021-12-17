using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class ProcessFinder : Form {
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

		private const string gameProcessName = "ffxiv_dx11";
		private readonly List<FFXIVProcess> processes = new List<FFXIVProcess>();
		private readonly Timer refreshListTimer = new Timer();

		public ProcessFinder() {
			InitializeComponent();
		}

		private void ProcessFinder_Load(object sender, EventArgs e) {
			FFXIVProcess def = new FFXIVProcess {
				def = true
			};
			processes.Add(def);

			RefreshList(null, null);
			refreshListTimer.Interval = 3000; // 3s
			refreshListTimer.Tick += RefreshList;
			refreshListTimer.Enabled = true;
		}

		private void RefreshList(object sender, EventArgs e) {
			foreach (Process p in Process.GetProcessesByName(gameProcessName)) {
				FFXIVProcess found = processes.Find(x => x.process != null && x.process.Id == p.Id);
				if (found != null) {
					found.found = true;
					found.name = RefreshName(found);
				} else {
					FFXIVProcess newP = GetProcessInformation(p);
					newP.found = true;
					CBB_PF_Processes.Items.Add(newP);
					processes.Add(newP);
				}
			}

			foreach (FFXIVProcess p in processes.ToArray()) {
				if (p.def) {
					continue;
				}

				if (!p.found) {
					processes.Remove(p);
					CBB_PF_Processes.Items.Remove(p);
				} else {
					p.found = false;
				}
			}

			RefreshCBB();
		}

		private void RefreshCBB() {
			FFXIVProcess def = processes.Find(x => x.def);
			if (processes.Count > 1 && CBB_PF_Processes.Items.Contains(def)) {
				CBB_PF_Processes.Items.Remove(def);
				BT_PF_Confirm.Enabled = true;
			} else if (CBB_PF_Processes.Items.Count == 0) {
				CBB_PF_Processes.Items.Add(def);
				BT_PF_Confirm.Enabled = false;
			}

			int count = CBB_PF_Processes.Items.Count;
			CBB_PF_Processes.SuspendLayout();
			for (int i = 0; i < count; i++) {
				CBB_PF_Processes.Items[i] = CBB_PF_Processes.Items[i];
			}
			CBB_PF_Processes.ResumeLayout();
		}

		private static FFXIVProcess GetProcessInformation(Process p) {
			GameData.gameProcess = p;

			string name = "Logged off";
			if (GameData.IsLoggedIn()) {
				name = GameData.GetName();
			}

			FFXIVProcess newP = new FFXIVProcess {
				process = p,
				name = name,
				found = false
			};

			GameData.gameProcess = null;
			return newP;
		}

		private static string RefreshName(FFXIVProcess p) {
			GameData.gameProcess = p.process;

			string name = "Logged off";
			if (GameData.IsLoggedIn()) {
				name = GameData.GetName();
			}

			GameData.gameProcess = null;
			return name;
		}

		private void CBB_PF_Processes_SelectedIndexChanged(object sender, EventArgs e) {
			if (CBB_PF_Processes.SelectedItem != null && !((FFXIVProcess)CBB_PF_Processes.SelectedItem).def) {
				BT_PF_Confirm.Enabled = true;
			} else {
				BT_PF_Confirm.Enabled = false;
			}
		}

		private void BT_PF_Confirm_Click(object sender, EventArgs e) {
			if (CBB_PF_Processes.SelectedItem == null) {
				return;
			}

			FFXIVProcess p = (FFXIVProcess)CBB_PF_Processes.SelectedItem;

			if (!p.def) {
				GameData.gameProcess = p.process;
				DialogResult = DialogResult.OK;
			}
		}
	}
}
