
namespace EorzeansVoice {
	partial class UC_ClientAround {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.BT_CAF_Mute = new System.Windows.Forms.Button();
			this.HSB_CAF_Volume = new System.Windows.Forms.HScrollBar();
			this.LBL_CAF_NameID = new System.Windows.Forms.Label();
			this.PAN_CAF_Content = new System.Windows.Forms.Panel();
			this.PAN_CAF_Content.SuspendLayout();
			this.SuspendLayout();
			// 
			// BT_CAF_Mute
			// 
			this.BT_CAF_Mute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BT_CAF_Mute.Location = new System.Drawing.Point(438, 50);
			this.BT_CAF_Mute.Name = "BT_CAF_Mute";
			this.BT_CAF_Mute.Size = new System.Drawing.Size(50, 30);
			this.BT_CAF_Mute.TabIndex = 2;
			this.BT_CAF_Mute.Text = "Mute";
			this.BT_CAF_Mute.UseVisualStyleBackColor = true;
			// 
			// HSB_CAF_Volume
			// 
			this.HSB_CAF_Volume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HSB_CAF_Volume.Location = new System.Drawing.Point(10, 50);
			this.HSB_CAF_Volume.Minimum = 1;
			this.HSB_CAF_Volume.Name = "HSB_CAF_Volume";
			this.HSB_CAF_Volume.Size = new System.Drawing.Size(425, 30);
			this.HSB_CAF_Volume.TabIndex = 4;
			this.HSB_CAF_Volume.Value = 100;
			// 
			// LBL_CAF_NameID
			// 
			this.LBL_CAF_NameID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LBL_CAF_NameID.AutoEllipsis = true;
			this.LBL_CAF_NameID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LBL_CAF_NameID.Location = new System.Drawing.Point(10, 10);
			this.LBL_CAF_NameID.Name = "LBL_CAF_NameID";
			this.LBL_CAF_NameID.Size = new System.Drawing.Size(478, 30);
			this.LBL_CAF_NameID.TabIndex = 3;
			this.LBL_CAF_NameID.Text = "999999 - WWWWWWWWWW WWWWWWWWWW";
			this.LBL_CAF_NameID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PAN_CAF_Content
			// 
			this.PAN_CAF_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PAN_CAF_Content.BackColor = System.Drawing.SystemColors.Control;
			this.PAN_CAF_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PAN_CAF_Content.Controls.Add(this.LBL_CAF_NameID);
			this.PAN_CAF_Content.Controls.Add(this.BT_CAF_Mute);
			this.PAN_CAF_Content.Controls.Add(this.HSB_CAF_Volume);
			this.PAN_CAF_Content.Location = new System.Drawing.Point(7, 10);
			this.PAN_CAF_Content.Name = "PAN_CAF_Content";
			this.PAN_CAF_Content.Size = new System.Drawing.Size(500, 90);
			this.PAN_CAF_Content.TabIndex = 5;
			// 
			// UC_ClientAround
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.PAN_CAF_Content);
			this.Name = "UC_ClientAround";
			this.Size = new System.Drawing.Size(515, 110);
			this.PAN_CAF_Content.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BT_CAF_Mute;
		private System.Windows.Forms.HScrollBar HSB_CAF_Volume;
		private System.Windows.Forms.Label LBL_CAF_NameID;
		private System.Windows.Forms.Panel PAN_CAF_Content;
	}
}
