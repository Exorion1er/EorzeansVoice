using System;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class Main : Form {
		public Main() {
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			ProcessFinder form = new ProcessFinder();
			DialogResult result = form.ShowDialog();

			if (result == DialogResult.OK) {
				// Do stuff
			} else {
				Application.Exit();
			}
		}
	}
}
