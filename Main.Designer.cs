
namespace EorzeansVoice {
	partial class Main {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.LBL_Name = new System.Windows.Forms.Label();
			this.LBL_Pos = new System.Windows.Forms.Label();
			this.LBL_MapID = new System.Windows.Forms.Label();
			this.LBL_Instance = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// LBL_Name
			// 
			this.LBL_Name.AutoSize = true;
			this.LBL_Name.Location = new System.Drawing.Point(12, 9);
			this.LBL_Name.Name = "LBL_Name";
			this.LBL_Name.Size = new System.Drawing.Size(38, 15);
			this.LBL_Name.TabIndex = 0;
			this.LBL_Name.Text = "label1";
			// 
			// LBL_Pos
			// 
			this.LBL_Pos.AutoSize = true;
			this.LBL_Pos.Location = new System.Drawing.Point(13, 28);
			this.LBL_Pos.Name = "LBL_Pos";
			this.LBL_Pos.Size = new System.Drawing.Size(38, 15);
			this.LBL_Pos.TabIndex = 1;
			this.LBL_Pos.Text = "label1";
			// 
			// LBL_MapID
			// 
			this.LBL_MapID.AutoSize = true;
			this.LBL_MapID.Location = new System.Drawing.Point(13, 47);
			this.LBL_MapID.Name = "LBL_MapID";
			this.LBL_MapID.Size = new System.Drawing.Size(38, 15);
			this.LBL_MapID.TabIndex = 2;
			this.LBL_MapID.Text = "label1";
			// 
			// LBL_Instance
			// 
			this.LBL_Instance.AutoSize = true;
			this.LBL_Instance.Location = new System.Drawing.Point(13, 66);
			this.LBL_Instance.Name = "LBL_Instance";
			this.LBL_Instance.Size = new System.Drawing.Size(38, 15);
			this.LBL_Instance.TabIndex = 3;
			this.LBL_Instance.Text = "label1";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(367, 552);
			this.Controls.Add(this.LBL_Instance);
			this.Controls.Add(this.LBL_MapID);
			this.Controls.Add(this.LBL_Pos);
			this.Controls.Add(this.LBL_Name);
			this.Name = "Main";
			this.Text = "Eorzeans Voice";
			this.Load += new System.EventHandler(this.Main_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LBL_Name;
		private System.Windows.Forms.Label LBL_Pos;
		private System.Windows.Forms.Label LBL_MapID;
		private System.Windows.Forms.Label LBL_Instance;
	}
}

