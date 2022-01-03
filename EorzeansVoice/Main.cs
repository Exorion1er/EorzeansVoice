using EorzeansVoice.Utils;
using EorzeansVoiceLib.Enums;
using EorzeansVoiceLib.NetworkMessageContent;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class Main : Form {
		public class ClientAround {
			public int id;
			public string name;
			public Vector3 position;
			public BufferedWaveProvider waveProvider;
			public WaveChannel32 channel;
			public USC_ClientAround controls;
		}

		public static Main instance;

		public int userID;
		public List<ClientAround> around = new List<ClientAround>();

		private Process gameProcess;
		private bool processUpdateName = false;

		public Main() {
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			instance = this;

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
			AudioController.Device output = (AudioController.Device)CBB_AudioOutputs.SelectedItem;
			AudioController.Init(input, output);

			LogInAndConnect();
		}

		private void LogInAndConnect() {
			if (GameData.IsLoggedIn(gameProcess)) {
				TIM_LoginWait.Enabled = false;
				LBL_Status.Text = "Connecting to server...";

				short worldID = GameData.GetCurrentWorldID(gameProcess);
				string name = GameData.GetName(gameProcess);
				int mapID = GameData.GetMapID(gameProcess);
				int instanceID = GameData.GetInstanceID(gameProcess);
				Vector3 position = GameData.GetPosition(gameProcess);
				userID = Network.ConnectToVoiceChat(worldID, name, mapID, instanceID, position);

				if (userID == 0) {
					MessageBox.Show("Couldn't connect to voice chat server.");
					Application.Exit();
					return;
				}

				AudioController.StartAudio();
				Network.StartReceivingData();

				LBL_Status.Text = "Connected !";
				TIM_KeepAlive.Enabled = true;
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
			// OPTIMIZATION : Cache data and send only if modified

			Vector3 position = GameData.GetPosition(gameProcess);
			short worldID = GameData.GetCurrentWorldID(gameProcess);
			int mapID = GameData.GetMapID(gameProcess);
			int instanceID = GameData.GetInstanceID(gameProcess);

			Network.SendInfoToServer(userID, position, worldID, mapID, instanceID);
		}

		public void UpdateAround(List<ClientInfo> info) {
			foreach (ClientInfo i in info) {
				ClientAround c = around.FirstOrDefault(x => x.id == i.id);

				if (c == null) {
					Tuple<BufferedWaveProvider, WaveChannel32> audio = AudioController.AddNewProvider();

					ClientAround newAround = new ClientAround {
						id = i.id,
						name = i.name,
						position = i.position,
						waveProvider = audio.Item1,
						channel = audio.Item2
					};

					around.Add(newAround);
				} else {
					c.position = i.position;
				}
			}
		}

		private void UpdateControls(object sender, EventArgs e) {
			foreach (ClientAround c in around) {
				if (c.controls == null) {
					USC_ClientAround newClientControls = new USC_ClientAround {
						Dock = DockStyle.Top,
						Userame = c.name
					};
					PAN_AroundContent.Controls.Add(newClientControls);

					c.controls = newClientControls;
				}
			}
		}

		private void KeepAliveTick(object sender, EventArgs e) {
			// OPTIMIZATION : Only send if nothing was sent in the last 5s
			Network.SendKeepAlive(userID);
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e) {
			AudioController.StopAudio();
			Network.Disconnect(userID);
		}
	}
}
