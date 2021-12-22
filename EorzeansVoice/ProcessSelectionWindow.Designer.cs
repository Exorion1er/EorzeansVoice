
namespace EorzeansVoice {
	partial class ProcessSelectionWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.CBB_PF_Processes = new System.Windows.Forms.ComboBox();
			this.BT_PF_Confirm = new System.Windows.Forms.Button();
			this.LBL_PF_One = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CBB_PF_Processes
			// 
			this.CBB_PF_Processes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBB_PF_Processes.FormattingEnabled = true;
			this.CBB_PF_Processes.Location = new System.Drawing.Point(12, 31);
			this.CBB_PF_Processes.Name = "CBB_PF_Processes";
			this.CBB_PF_Processes.Size = new System.Drawing.Size(256, 23);
			this.CBB_PF_Processes.TabIndex = 0;
			this.CBB_PF_Processes.SelectedIndexChanged += new System.EventHandler(this.CBB_PF_Processes_SelectedIndexChanged);
			// 
			// BT_PF_Confirm
			// 
			this.BT_PF_Confirm.Enabled = false;
			this.BT_PF_Confirm.Location = new System.Drawing.Point(274, 31);
			this.BT_PF_Confirm.Name = "BT_PF_Confirm";
			this.BT_PF_Confirm.Size = new System.Drawing.Size(86, 23);
			this.BT_PF_Confirm.TabIndex = 1;
			this.BT_PF_Confirm.Text = "Confirm";
			this.BT_PF_Confirm.UseVisualStyleBackColor = true;
			this.BT_PF_Confirm.Click += new System.EventHandler(this.BT_PF_Confirm_Click);
			// 
			// LBL_PF_One
			// 
			this.LBL_PF_One.AutoSize = true;
			this.LBL_PF_One.Location = new System.Drawing.Point(12, 9);
			this.LBL_PF_One.Name = "LBL_PF_One";
			this.LBL_PF_One.Size = new System.Drawing.Size(156, 15);
			this.LBL_PF_One.TabIndex = 2;
			this.LBL_PF_One.Text = "Please select FFXIV\'s process";
			// 
			// ProcessFinder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(372, 64);
			this.Controls.Add(this.LBL_PF_One);
			this.Controls.Add(this.BT_PF_Confirm);
			this.Controls.Add(this.CBB_PF_Processes);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ProcessFinder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select FFXIV\'s process";
			this.Load += new System.EventHandler(this.ProcessFinder_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox CBB_PF_Processes;
		private System.Windows.Forms.Button BT_PF_Confirm;
		private System.Windows.Forms.Label LBL_PF_One;
	}
}