using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class Main : Form {
		private Timer mainTimer = new Timer();
		
		public Main() {
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			ProcessFinder form = new ProcessFinder();
			DialogResult result = form.ShowDialog();

			if (result == DialogResult.OK) {
				// Do stuff

				mainTimer.Interval = 500;
				mainTimer.Tick += Tick;
				mainTimer.Enabled = true;
			} else {
				Application.Exit();
			}
		}

		private void Tick(object sender, EventArgs e) {
			LBL_Name.Text = "Name : " + GameData.GetName();
			LBL_Pos.Text = "Position : " + GameData.GetPosition().ToString();
			LBL_MapID.Text = "Map ID : " + GameData.GetMapID().ToString();
			LBL_Instance.Text = "Instance : " + GameData.GetInstance().ToString();
		}
	}
}
