using EorzeansVoice.Utils;
using EorzeansVoiceLib;
using EorzeansVoiceLib.Enums;
using EorzeansVoiceLib.NetworkMessageContent;
using EorzeansVoiceLib.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading;
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
			public bool remove;
		}

		public static Main instance;

		public int userID;
		public List<ClientAround> around = new List<ClientAround>();

		private UpdateServer infoCache;
		private DateTime lastSent;
		private Process gameProcess;
		private bool processUpdateName = false;

		public Main() {
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			instance = this;

			Logging.AddLogger(Logging.LogType.File, Logging.LogLevel.Debug, "Log"); // Replace with Settings
			Logging.Info("##### Eorzeans' Voice " + NetworkConsts.clientVersion + " #####\n");
			Logging.Info("Loading...");

			Logging.Debug("Checking admin permissions...");
			AppDomain domain = Thread.GetDomain();
			domain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
			WindowsPrincipal p = (WindowsPrincipal)Thread.CurrentPrincipal;
			if (!p.IsInRole(WindowsBuiltInRole.Administrator)) {
				MessageBox.Show("You need to run this app as an administrator for full functionality.");
				Logging.Warn("Missing admin permissions, closing.");
				Application.Exit();
				return;
			}

			Logging.Debug("Checking networking...");
			if (!Network.IsNetworkWorking()) {
				MessageBox.Show("Couldn't establish a connection with the internet. Please check your internet connection.");
				Logging.Warn("Network not working, closing.");
				Application.Exit();
				return;
			}

			Logging.Debug("Checking server...");
			if (!Network.IsServerWorking()) {
				MessageBox.Show("Couldn't establish a connection with the server.");
				Logging.Warn("Server not working, closing.");
				Application.Exit();
				return;
			}

			Logging.Debug("Loading Audio...");
			AudioController.LoadAudioDevices(CBB_AudioInputs, CBB_AudioOutputs);
			InitAudio();

			Logging.Info("Loading complete !");
		}

		private void Main_Shown(object sender, EventArgs e) {
			CheckVersion();
			if (FindProcess()) {
				LogInAndConnect();
			}
		}

		private void CheckVersion() {
			Logging.Info("Checking version...");
			VersionCheckAnswer versionCheck = Network.IsUpToDate();

			if (versionCheck == VersionCheckAnswer.ClientOutOfDate) {
				// Start Auto Update here
				LBL_Status.Text = "Update available !";
				MessageBox.Show("An update is available.");
				Logging.Warn("Client update available, closing.");
				Application.Exit();
				return;
			} else if (versionCheck == VersionCheckAnswer.ServerOutOfDate) {
				LBL_Status.Text = "Server out of date.";
				MessageBox.Show("The server is out of date. Please wait a moment and try again.");
				Logging.Warn("Server out of date, closing.");
				Application.Exit();
				return;
			}

			LBL_Status.Text = "Up to date.";
			Logging.Info("Up to date.");
		}

		private bool FindProcess() {
			Logging.Info("Looking for the game's process...");
			LBL_Status.Text = "Looking for the game's process...";

			ProcessFinder.Result result = ProcessFinder.GetProcess();
			if (result.type == ProcessFinder.ResultType.NoneAvailable) {
				Logging.Info("No FFXIV process available, checking again in 3s.");
				LBL_Process.Text = "Please open FFXIV.";
				LBL_Status.Text = "Please open FFXIV.";

				TIM_Process.Interval = 3000; // 3s
				TIM_Process.Enabled = true;

				return false;
			} else if (result.type == ProcessFinder.ResultType.ClosedSelector) {
				Logging.Info("User closed the process selector, waiting on user input.");
				BT_SelectProcess.Enabled = true;

				return false;
			}

			gameProcess = result.process.process;
			LBL_Process.Text = result.process.ToString();
			BT_SelectProcess.Enabled = false;
			processUpdateName = true;
			TIM_Process.Interval = 15000; // 15s
			TIM_Process.Enabled = true;
			Logging.Info("Process found.");
			return true;
		}

		private void TIM_Process_Tick(object sender, EventArgs e) {
			if (processUpdateName) {
				LBL_Process.Text = ProcessFinder.GetProcessInformation(gameProcess).ToString();
			} else if (FindProcess()) {
				LogInAndConnect();
			}
		}

		private void BT_SelectProcess_Click(object sender, EventArgs e) {
			FindProcess();
		}

		private void InitAudio() {
			AudioController.Device input = (AudioController.Device)CBB_AudioInputs.SelectedItem;
			AudioController.Device output = (AudioController.Device)CBB_AudioOutputs.SelectedItem;
			AudioController.Init(input, output);
			AudioInputProcessing.Init(SLD_VoiceActivation.Value);
		}

		private void LogInAndConnect() {
			if (GameData.IsLoggedIn(gameProcess)) {
				Logging.Info("User is logged in, connecting...");
				TIM_LoginWait.Enabled = false;
				LBL_Status.Text = "Connecting to server...";

				short worldID = GameData.GetCurrentWorldID(gameProcess);
				string name = GameData.GetName(gameProcess);
				int mapID = GameData.GetMapID(gameProcess);
				int instanceID = GameData.GetInstanceID(gameProcess);
				Vector3 position = GameData.GetPosition(gameProcess);
				userID = Network.ConnectToVoiceChat(worldID, name, mapID, instanceID, position);

				if (userID == 0) {
					MessageBox.Show("Couldn't connect to voice chat server. Please restart Eorzeans' Voice.");
					Logging.Error("An error occurred while connecting to server, closing.");
					Application.Exit();
					return;
				}
				Logging.Info("User is now connected : " + name + " (" + userID + ")");

				AudioController.StartAudio();
				Network.StartReceivingData();

				lastSent = DateTime.Now;
				LBL_Status.Text = "Connected !";
				Logging.Info("Audio started and now receiving data form server.");
				TIM_KeepAlive.Enabled = true;
				TIM_SendInfo.Enabled = true;
			} else {
				Logging.Info("User is not logged into a character, checking again in 3s.");
				LBL_Status.Text = "Please log into a character.";
				TIM_LoginWait.Interval = 3000; // 3s
				TIM_LoginWait.Enabled = true;
			}
		}

		private void LoginWaitTick(object sender, EventArgs e) {
			LogInAndConnect();
		}

		private void SendInfoTick(object sender, EventArgs e) {
			UpdateServer newInfo = new UpdateServer {
				id = userID,
				position = GameData.GetPosition(gameProcess),
				worldID = GameData.GetCurrentWorldID(gameProcess),
				mapID = GameData.GetMapID(gameProcess),
				instanceID = GameData.GetInstanceID(gameProcess)
			};

			if (infoCache != newInfo) {
				Logging.Debug("Info changed, sending to server.");
				infoCache = newInfo;
				lastSent = DateTime.Now;
				Network.SendInfoToServer(newInfo);
			}
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
					Logging.Debug("Adding user to around list : " + i.name + " (" + i.id + ")");
				} else {
					c.position = i.position;
				}
			}

			foreach (ClientAround c in around) {
				ClientInfo i = info.FirstOrDefault(x => x.id == c.id);

				if (i == null) {
					c.remove = true;
					Logging.Debug("Removing user from around list : " + i.name + " (" + i.id + ")");
				}
			}
		}

		private void UpdateControls(object sender, EventArgs e) {
			foreach (ClientAround c in around.ToArray()) {
				if (c.remove) {
					if (c.controls != null) {
						PAN_AroundContent.Controls.Remove(c.controls);
					}

					around.Remove(c);
					c.controls.Dispose();
				} else if (c.controls == null) {
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
			if (DateTime.Now - lastSent >= TimeSpan.FromSeconds(5)) {
				lastSent = DateTime.Now;
				Logging.Debug("Hasn't sent anything in 5s or more, sending Keep Alive.");
				Network.SendKeepAlive(userID);
			}
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e) {
			if (userID != 0) {
				Logging.Info("Disconnect before closing.");
				Network.Disconnect(userID);
			}
		}

		private void VoiceModeChanged(object sender, EventArgs e) {
			if (RBT_VoiceActivation.Checked) {
				Logging.Debug("Switch to Voice Activation.");
				AudioInputProcessing.mode = AudioInputProcessing.Mode.VoiceActivation;
			} else if (RBT_PushToTalk.Checked) {
				Logging.Debug("Switch to Push To Talk.");
				AudioInputProcessing.mode = AudioInputProcessing.Mode.PushToTalk;
			}
		}

		private void SLD_VoiceActivation_ValueChanged(object sender, EventArgs e) {
			AudioInputProcessing.voiceActivationThreshold = SLD_VoiceActivation.Value;
		}

		public void UpdateVoiceActivationSlider(float value) {
			SLD_VoiceActivation.ActiveValue = value;
		}

		private void BT_Mute_Click(object sender, EventArgs e) {
			if (AudioInputProcessing.muted) {
				AudioInputProcessing.muted = false;
				BT_Mute.BackgroundImage = Properties.Resources.Speaking;
				Logging.Debug("Now speaking.");
			} else {
				AudioInputProcessing.muted = true;
				BT_Mute.BackgroundImage = Properties.Resources.Muted;
				Logging.Debug("Now muted.");
			}
		}

		private void CBB_AudioInputs_SelectedIndexChanged(object sender, EventArgs e) {
			if (CBB_AudioInputs.SelectedItem != null) {
				AudioController.Device d = (AudioController.Device)CBB_AudioInputs.SelectedItem;
				Logging.Debug("Change input device to : " + d.name);
				AudioController.ChangeInputDevice(d);
			}
		}

		private void CBB_AudioOutputs_SelectedIndexChanged(object sender, EventArgs e) {
			if (CBB_AudioOutputs.SelectedItem != null) {
				AudioController.Device d = (AudioController.Device)CBB_AudioOutputs.SelectedItem;
				Logging.Debug("Change output device to : " + d.name);
				AudioController.ChangeOutputDevice(d);
			}
		}

		private void SLD_GlobalVolume_ValueChanged(object sender, EventArgs e) {
			LBL_GlobalVolume.Text = Math.Round(SLD_GlobalVolume.Value * 100f).ToString() + " %";
			AudioController.ChangeGlobalVolume(SLD_GlobalVolume.Value);
		}
	}
}
