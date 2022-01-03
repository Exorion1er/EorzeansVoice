using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class USC_ClientAround : UserControl {
		[Category("Data")]
		[Description("The user's character name")]
		public string Userame {
			get { return LBL_CA_Name.Text; }
			set { LBL_CA_Name.Text = value; }
		}

		[Category("Data")]
		[Description("The user's volume")]
		public int Volume {
			get { return TBR_CA_Volume.Value; }
			set { TBR_CA_Volume.Value = value; }
		}

		[Category("Data")]
		[Description("Whether the user is muted or not")]
		public bool Muted {
			get { return muted; }
			set { muted = value; }
		}

		private bool muted;

		public USC_ClientAround() {
			InitializeComponent();
		}

		private void TBR_CA_Volume_Scroll(object sender, EventArgs e) {
			LBL_CA_Volume.Text = TBR_CA_Volume.Value + "%";
		}

		private void BT_CA_Mute_Click(object sender, EventArgs e) {
			muted = !muted;
		}
	}
}
