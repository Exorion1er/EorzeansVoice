using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EorzeansVoice {
	public partial class MainRework : MaterialForm {
		public MainRework() {
			InitializeComponent();

			MaterialSkinManager manager = MaterialSkinManager.Instance;
			manager.AddFormToManage(this);
			manager.Theme = MaterialSkinManager.Themes.DARK;
		}

		private void materialFloatingActionButton3_Click(object sender, EventArgs e) {

		}

		private void MainRework_Load(object sender, EventArgs e) {
			for (int i = 0; i < 30; i++) {
				UC_ClientAroundRework rework = new UC_ClientAroundRework();
				rework.Dock = DockStyle.Top;
				materialCard3.Controls.Add(rework);
			}
		}
	}
}
