using EorzeansVoiceLib;
using System;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class Main : Form {
		public static Main instance;

		public bool lookingForKeybind;

		public Main() {
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			instance = this;

			LogicController.Load(CBB_AudioInputs, CBB_AudioOutputs, SLD_VoiceActivation.Value);
		}

		private void Main_Shown(object sender, EventArgs e) {
			LogicController.MainShown();
		}

		public void UpdateStatus(string message) {
			LBL_Status.Text = message;
		}

		public void UpdateProcess(string message) {
			LBL_Process.Text = message;
		}

		public void ToggleProcessButton(bool value) {
			BT_SelectProcess.Enabled = value;
		}

		private void BT_SelectProcess_Click(object sender, EventArgs e) {
			LogicController.FindProcess();
		}

		private void UpdateControls(object sender, EventArgs e) {
			foreach (LogicController.ClientAround c in LogicController.around.ToArray()) {
				if (c.remove) {
					if (c.controls != null) {
						PAN_AroundContent.Controls.Remove(c.controls);
					}

					LogicController.around.Remove(c);
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

		private void Main_FormClosing(object sender, FormClosingEventArgs e) {
			LogicController.Closing();
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

		private void BT_PTTKeybind_Click(object sender, EventArgs e) {
			lookingForKeybind = true;
			BT_PTTKeybind.Text = "Recording...";
			HotkeyController.StopListening();
		}

		private void BT_PTTKeybind_KeyDown(object sender, KeyEventArgs e) {
			// TODO : Don't stop for modifiers

			if (lookingForKeybind) {
				lookingForKeybind = false;
				BT_PTTKeybind.Text = e.Modifiers + " + " + e.KeyData;

				AudioInputProcessing.ChangePTTKey(e.KeyData, e.Modifiers);
				HotkeyController.StartListening();
			}
		}
	}
}
