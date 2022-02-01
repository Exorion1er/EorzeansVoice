using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class USC_ClientAround : UserControl {
		[Category("Data")]
		[Description("The user's character name")]
		public string Username {
			get { return LBL_CA_Name.Text; }
			set { LBL_CA_Name.Text = value; }
		}

		[Category("Data")]
		[Description("The user's volume")]
		public float Volume {
			get { return SLD_CA_Volume.Value; }
			set { SLD_CA_Volume.Value = value; }
		}

		[Category("Data")]
		[Description("The user's voice level")]
		public float VoiceLevel {
			get { return SLD_CA_Volume.ActiveValue; }
			set { SLD_CA_Volume.ActiveValue = value; }
		}

		[Category("Data")]
		[Description("Whether the user is muted or not")]
		public bool Muted {
			get { return muted; }
			set { muted = value; }
		}

		[Category("Action")]
		[Description("Invoked when this user's mute status changes.")]
		public event EventHandler MuteChanged;

		[Category("Action")]
		[Description("Invokes when this user's volume was changed.")]
		public event EventHandler VolumeChanged;

		public int id;

		private bool muted;

		public USC_ClientAround() {
			InitializeComponent();
		}

		private void BT_CA_Mute_Click(object sender, EventArgs e) {
			muted = !muted;
			if (muted) {
				BT_CA_Mute.BackgroundImage = Properties.Resources.Muted;
			} else {
				BT_CA_Mute.BackgroundImage = Properties.Resources.Speaking;
			}

			MuteChanged?.Invoke(this, e);
		}

		private void SLD_CA_Volume_ValueChanged(object sender, EventArgs e) {
			LBL_CA_Volume.Text = Math.Round(SLD_CA_Volume.Value * 100f).ToString() + " %";

			VolumeChanged?.Invoke(this, e);
		}
	}
}
