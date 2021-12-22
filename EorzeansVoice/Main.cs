using EorzeansVoice.Utils;
using EorzeansVoiceLib.Enums;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class Main : Form {
		private Process gameProcess;
		private bool processUpdateName = false;

		public Main() {
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			if (!Network.IsNetworkWorking()) {
				MessageBox.Show("Couldn't establish a connection with the internet. Please check your internet connection.");
				Application.Exit();
				return;
			}

			if (!Network.IsServerWorking()) {
				MessageBox.Show("Couldn't establish a connection with the server.");
				Application.Exit();
				return;
			}

			AudioController.LoadAudioDevices(CBB_AudioInputs, CBB_AudioOutputs);
		}

		private void Main_Shown(object sender, EventArgs e) {
			CheckVersion();
			if (FindProcess()) {
				InitAudio();
			}
		}

		private void InitAudio() {
			LBL_Status.Text = "Initiating audio";

			AudioController.Device input = (AudioController.Device)CBB_AudioInputs.SelectedItem;
			AudioController.Device output = (AudioController.Device)CBB_AudioInputs.SelectedItem;
			AudioController.Init(input, output);

			LogInAndConnect();
		}

		private void LogInAndConnect() {
			if (GameData.IsLoggedIn(gameProcess)) {
				TIM_LoginWait.Enabled = false;
				LBL_Status.Text = "Connecting to server...";

				string name = GameData.GetName(gameProcess);
				int mapID = GameData.GetMapID(gameProcess);
				int instanceID = GameData.GetInstance(gameProcess);
				Vector3 position = GameData.GetPosition(gameProcess);
				Network.ConnectToVoiceChat(name, mapID, instanceID, position);

				AudioController.StartAudio();

				TIM_SendPosition.Enabled = true;
			} else {
				LBL_Status.Text = "Please log into a character.";
				TIM_LoginWait.Interval = 3000; // 3s
				TIM_LoginWait.Enabled = true;
			}
		}

		private void CheckVersion() {
			VersionCheckAnswer versionCheck = Network.IsUpToDate();

			if (versionCheck == VersionCheckAnswer.ClientOutOfDate) {
				// Start Auto Update here
				LBL_Status.Text = "Update available !";
				MessageBox.Show("An update is available.");
				Application.Exit();
				return;
			} else if (versionCheck == VersionCheckAnswer.ServerOutOfDate) {
				LBL_Status.Text = "Server out of date.";
				MessageBox.Show("The server is out of date. Please wait a moment and try again.");
				Application.Exit();
				return;
			}

			LBL_Status.Text = "Up to date.";
		}

		private bool FindProcess() {
			LBL_Status.Text = "Looking for the game's process...";

			ProcessFinder.Result result = ProcessFinder.GetProcess();
			if (result.type == ProcessFinder.ResultType.NoneAvailable) {
				LBL_Process.Text = "Please open FFXIV.";
				LBL_Status.Text = "Please open FFXIV.";

				TIM_Process.Interval = 3000; // 3s
				TIM_Process.Enabled = true;

				return false;
			} else if (result.type == ProcessFinder.ResultType.ClosedSelector) {
				BT_SelectProcess.Enabled = true;

				return false;
			}

			gameProcess = result.process.process;
			LBL_Process.Text = result.process.ToString();
			BT_SelectProcess.Enabled = false;
			processUpdateName = true;
			TIM_Process.Interval = 15000; // 15s
			TIM_Process.Enabled = true;
			return true;
		}

		private void ProcessTimerTick(object sender, EventArgs e) {
			if (processUpdateName) {
				LBL_Process.Text = ProcessFinder.GetProcessInformation(gameProcess).ToString();
			} else if (FindProcess()) {
				InitAudio();
			}
		}

		private void BT_SelectProcess_Click(object sender, EventArgs e) {
			FindProcess();
		}

		private void LoginWaitTick(object sender, EventArgs e) {
			LogInAndConnect();
		}

		private void UpdatePositionTick(object sender, EventArgs e) {
			// Cache it and send only if modified
			Network.SendInfoToServer(GameData.GetPosition(gameProcess));
		}
	}
}
