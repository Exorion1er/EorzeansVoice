using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class ProcessSelectionWindow : Form {
		public ProcessFinder.FFXIVProcess foundProcess;

		private readonly List<ProcessFinder.FFXIVProcess> processes = new List<ProcessFinder.FFXIVProcess>();
		private readonly Timer refreshListTimer = new Timer();

		public ProcessSelectionWindow() {
			InitializeComponent();
		}

		private void ProcessFinder_Load(object sender, EventArgs e) {
			ProcessFinder.FFXIVProcess def = new ProcessFinder.FFXIVProcess {
				def = true
			};
			processes.Add(def);

			RefreshList(null, null);
			refreshListTimer.Interval = 3000; // 3s
			refreshListTimer.Tick += RefreshList;
			refreshListTimer.Enabled = true;
		}

		private void RefreshList(object sender, EventArgs e) {
			foreach (Process p in Process.GetProcessesByName(ProcessFinder.gameProcessName)) {
				ProcessFinder.FFXIVProcess found = processes.Find(x => x.process != null && x.process.Id == p.Id);
				if (found != null) {
					found.found = true;
					found.name = ProcessFinder.RefreshName(found);
				} else {
					ProcessFinder.FFXIVProcess newP = ProcessFinder.GetProcessInformation(p);
					newP.found = true;
					CBB_PF_Processes.Items.Add(newP);
					processes.Add(newP);
				}
			}

			foreach (ProcessFinder.FFXIVProcess p in processes.ToArray()) {
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
			ProcessFinder.FFXIVProcess def = processes.Find(x => x.def);
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

		private void CBB_PF_Processes_SelectedIndexChanged(object sender, EventArgs e) {
			if (CBB_PF_Processes.SelectedItem != null && !((ProcessFinder.FFXIVProcess)CBB_PF_Processes.SelectedItem).def) {
				BT_PF_Confirm.Enabled = true;
			} else {
				BT_PF_Confirm.Enabled = false;
			}
		}

		private void BT_PF_Confirm_Click(object sender, EventArgs e) {
			if (CBB_PF_Processes.SelectedItem == null) {
				return;
			}

			ProcessFinder.FFXIVProcess p = (ProcessFinder.FFXIVProcess)CBB_PF_Processes.SelectedItem;

			if (!p.def) {
				refreshListTimer.Enabled = false;
				foundProcess = p;
				DialogResult = DialogResult.OK;
			}
		}
	}
}
