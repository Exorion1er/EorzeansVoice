﻿using EorzeansVoiceLib;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class Main : Form {
		private static readonly Keys[] modifierKeys = { Keys.ControlKey, Keys.ShiftKey, Keys.Menu };

		public static Main instance;

		public bool lookingForKeybind;

		public Main() {
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			instance = this;

			LogicController.Load(CBB_AudioInputs, CBB_AudioOutputs);
		}

		private void Main_Shown(object sender, EventArgs e) {
			LogicController.MainShown();
		}

		public void UpdateSize(int x, int y) {
			if (!InvokeRequired) {
				Size = new Size(x, y);
			} else {
				Invoke(new Action<int, int>(UpdateSize), x, y);
			}
		}

		public void UpdateStatus(string message) {
			if (!InvokeRequired) {
				LBL_Status.Text = message;
			} else {
				Invoke(new Action<string>(UpdateStatus), message);
			}
		}

		public void UpdateProcess(string message) {
			if (!InvokeRequired) {
				LBL_Process.Text = message;
			} else {
				Invoke(new Action<string>(UpdateProcess), message);
			}
		}

		public void ToggleProcessButton(bool value) {
			if (!InvokeRequired) {
				BT_SelectProcess.Enabled = value;
			} else {
				Invoke(new Action<bool>(ToggleProcessButton), value);
			}
		}

		public void UpdateVoiceActivationSLDMainValue(float value) {
			if (!InvokeRequired) {
				SLD_VoiceActivation.Value = value;
			} else {
				Invoke(new Action<float>(UpdateVoiceActivationSLDMainValue), value);
			}
		}

		public void UpdateGlobalVolume(float value) {
			if (!InvokeRequired) {
				SLD_GlobalVolume.Value = value;
			} else {
				Invoke(new Action<float>(UpdateGlobalVolume), value);
			}
		}

		public float GetGlobalVolume() {
			return SLD_GlobalVolume.Value;
		}

		public void UpdateVoiceMode(AudioInputProcessing.Mode mode) {
			if (!InvokeRequired) {
				if (mode == AudioInputProcessing.Mode.VoiceActivation) {
					RBT_VoiceActivation.Checked = true;
				} else if (mode == AudioInputProcessing.Mode.PushToTalk) {
					RBT_PushToTalk.Checked = true;
				}
			} else {
				Invoke(new Action<AudioInputProcessing.Mode>(UpdateVoiceMode), mode);
			}
		}

		public void UpdateVoiceActivationSLDActiveValue(float value) {
			SLD_VoiceActivation.ActiveValue = value;
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
						id = c.id,
						Dock = DockStyle.Top,
						Username = c.name,
					};
					newClientControls.MuteChanged += UserMuteChanged;
					newClientControls.VolumeChanged += UserVolumeChanged;
					PAN_AroundContent.Controls.Add(newClientControls);

					c.controls = newClientControls;
				}
			}
		}

		private void UserMuteChanged(object sender, EventArgs e) {
			USC_ClientAround controls = (USC_ClientAround)sender;
			LogicController.ClientAround ca = LogicController.around.Find(x => x.id == controls.id);

			if (controls.Muted) {
				ca.muted = true;
				ca.channel.Volume = 0;
			} else {
				ca.muted = false;
				ca.channel.Volume = ca.volume;
			}
		}

		private void UserVolumeChanged(object sender, EventArgs e) {
			USC_ClientAround controls = (USC_ClientAround)sender;
			LogicController.ClientAround ca = LogicController.around.Find(x => x.id == controls.id);

			ca.volume = controls.Volume;
			if (!ca.muted) {
				ca.channel.Volume = ca.volume;
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

		private void BT_Mute_Click(object sender, EventArgs e) {
			ToggleMute(!AudioInputProcessing.muted);
		}

		public void ToggleMute(bool value) {
			if (!value) {
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

		private void Main_KeyDown(object sender, KeyEventArgs e) {
			if (lookingForKeybind) {
				if (modifierKeys.Contains(e.KeyCode)) {
					return;
				}

				lookingForKeybind = false;

				string display;
				if (e.Modifiers != 0) {
					display = e.Modifiers + " + " + e.KeyCode;
				} else {
					display = e.KeyCode.ToString();
				}
				BT_PTTKeybind.Text = display;

				AudioInputProcessing.ChangePTTKey(e.KeyCode, e.Modifiers);
				HotkeyController.StartListening();
			}
		}

		private void BT_PTTKeybind_Leave(object sender, EventArgs e) {
			if (lookingForKeybind) {
				lookingForKeybind = false;
				BT_PTTKeybind.Text = "Unbound";

				if (AudioInputProcessing.pttKey != null) {
					PTTDisplayUseAlreadyBound();
				} else {
					BT_PTTKeybind.Text = "Unbound";
				}
			}
		}

		public void PTTDisplayUseAlreadyBound() {
			HotkeyController.KeyAction pttKey = AudioInputProcessing.pttKey;
			Keys modifiers = pttKey.control ? Keys.Control : Keys.None;
			modifiers |= pttKey.shift ? Keys.Shift : Keys.None;
			modifiers |= pttKey.alt ? Keys.Alt : Keys.None;

			string display;
			if (modifiers != 0) {
				display = modifiers + " + " + pttKey.key;
			} else {
				display = pttKey.key.ToString();
			}
			BT_PTTKeybind.Text = display;
		}
	}
}
