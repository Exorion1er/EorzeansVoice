
namespace EorzeansVoice {
	partial class USC_ClientAround {
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
			this.BT_CA_Mute = new System.Windows.Forms.Button();
			this.PAN_CA_Content = new System.Windows.Forms.Panel();
			this.LBL_CA_Name = new System.Windows.Forms.Label();
			this.LBL_CA_Volume = new System.Windows.Forms.Label();
			this.SLD_CA_Volume = new EorzeansVoice.Utils.Slider();
			this.PAN_CA_Content.SuspendLayout();
			this.SuspendLayout();
			// 
			// BT_CA_Mute
			// 
			this.BT_CA_Mute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BT_CA_Mute.BackgroundImage = global::EorzeansVoice.Properties.Resources.Speaking;
			this.BT_CA_Mute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.BT_CA_Mute.Location = new System.Drawing.Point(675, 3);
			this.BT_CA_Mute.Name = "BT_CA_Mute";
			this.BT_CA_Mute.Size = new System.Drawing.Size(26, 26);
			this.BT_CA_Mute.TabIndex = 2;
			this.BT_CA_Mute.UseVisualStyleBackColor = true;
			this.BT_CA_Mute.Click += new System.EventHandler(this.BT_CA_Mute_Click);
			// 
			// PAN_CA_Content
			// 
			this.PAN_CA_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PAN_CA_Content.BackColor = System.Drawing.SystemColors.Control;
			this.PAN_CA_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PAN_CA_Content.Controls.Add(this.SLD_CA_Volume);
			this.PAN_CA_Content.Controls.Add(this.LBL_CA_Name);
			this.PAN_CA_Content.Controls.Add(this.LBL_CA_Volume);
			this.PAN_CA_Content.Controls.Add(this.BT_CA_Mute);
			this.PAN_CA_Content.Location = new System.Drawing.Point(10, 5);
			this.PAN_CA_Content.Name = "PAN_CA_Content";
			this.PAN_CA_Content.Size = new System.Drawing.Size(706, 33);
			this.PAN_CA_Content.TabIndex = 5;
			// 
			// LBL_CA_Name
			// 
			this.LBL_CA_Name.Location = new System.Drawing.Point(3, 5);
			this.LBL_CA_Name.Name = "LBL_CA_Name";
			this.LBL_CA_Name.Size = new System.Drawing.Size(104, 23);
			this.LBL_CA_Name.TabIndex = 5;
			this.LBL_CA_Name.Text = "Exorion S.";
			this.LBL_CA_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LBL_CA_Volume
			// 
			this.LBL_CA_Volume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LBL_CA_Volume.Location = new System.Drawing.Point(631, 3);
			this.LBL_CA_Volume.Name = "LBL_CA_Volume";
			this.LBL_CA_Volume.Size = new System.Drawing.Size(38, 26);
			this.LBL_CA_Volume.TabIndex = 4;
			this.LBL_CA_Volume.Text = "100%";
			this.LBL_CA_Volume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SLD_CA_Volume
			// 
			this.SLD_CA_Volume.ActivebarColor = System.Drawing.SystemColors.ActiveCaption;
			this.SLD_CA_Volume.ActiveValue = 0F;
			this.SLD_CA_Volume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SLD_CA_Volume.HandleColor = System.Drawing.SystemColors.Highlight;
			this.SLD_CA_Volume.InactiveBarColor = System.Drawing.SystemColors.ControlDark;
			this.SLD_CA_Volume.Location = new System.Drawing.Point(113, 5);
			this.SLD_CA_Volume.Max = 1F;
			this.SLD_CA_Volume.Min = 0F;
			this.SLD_CA_Volume.Name = "SLD_CA_Volume";
			this.SLD_CA_Volume.Size = new System.Drawing.Size(512, 23);
			this.SLD_CA_Volume.TabIndex = 6;
			this.SLD_CA_Volume.Text = "slider1";
			this.SLD_CA_Volume.UseActiveValue = true;
			this.SLD_CA_Volume.Value = 1F;
			this.SLD_CA_Volume.ValueChanged += new System.EventHandler(this.SLD_CA_Volume_ValueChanged);
			// 
			// USC_ClientAround
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.PAN_CA_Content);
			this.Name = "USC_ClientAround";
			this.Size = new System.Drawing.Size(726, 43);
			this.PAN_CA_Content.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BT_CA_Mute;
		private System.Windows.Forms.Panel PAN_CA_Content;
		private System.Windows.Forms.Label LBL_CA_Name;
		private System.Windows.Forms.Label LBL_CA_Volume;
		private Utils.Slider SLD_CA_Volume;
	}
}
