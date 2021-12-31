
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
			this.components = new System.ComponentModel.Container();
			this.LBL_Status = new System.Windows.Forms.Label();
			this.GPB_Process = new System.Windows.Forms.GroupBox();
			this.BT_SelectProcess = new System.Windows.Forms.Button();
			this.LBL_Process = new System.Windows.Forms.Label();
			this.TIM_Process = new System.Windows.Forms.Timer(this.components);
			this.GPB_Audio = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.LBL_AudioInputs = new System.Windows.Forms.Label();
			this.CBB_AudioOutputs = new System.Windows.Forms.ComboBox();
			this.CBB_AudioInputs = new System.Windows.Forms.ComboBox();
			this.TIM_LoginWait = new System.Windows.Forms.Timer(this.components);
			this.TIM_SendPosition = new System.Windows.Forms.Timer(this.components);
			this.TIM_KeepAlive = new System.Windows.Forms.Timer(this.components);
			this.GPB_Around = new System.Windows.Forms.GroupBox();
			this.PAN_AroundContent = new System.Windows.Forms.Panel();
			this.TIM_UpdateControls = new System.Windows.Forms.Timer(this.components);
			this.GPB_Process.SuspendLayout();
			this.GPB_Audio.SuspendLayout();
			this.GPB_Around.SuspendLayout();
			this.SuspendLayout();
			// 
			// LBL_Status
			// 
			this.LBL_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LBL_Status.AutoEllipsis = true;
			this.LBL_Status.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.LBL_Status.Location = new System.Drawing.Point(12, 9);
			this.LBL_Status.Name = "LBL_Status";
			this.LBL_Status.Size = new System.Drawing.Size(467, 50);
			this.LBL_Status.TabIndex = 5;
			this.LBL_Status.Text = "Checking version...";
			this.LBL_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// GPB_Process
			// 
			this.GPB_Process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GPB_Process.Controls.Add(this.BT_SelectProcess);
			this.GPB_Process.Controls.Add(this.LBL_Process);
			this.GPB_Process.Location = new System.Drawing.Point(12, 62);
			this.GPB_Process.Name = "GPB_Process";
			this.GPB_Process.Size = new System.Drawing.Size(467, 59);
			this.GPB_Process.TabIndex = 6;
			this.GPB_Process.TabStop = false;
			this.GPB_Process.Text = "FFXIV Process";
			// 
			// BT_SelectProcess
			// 
			this.BT_SelectProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BT_SelectProcess.Enabled = false;
			this.BT_SelectProcess.Location = new System.Drawing.Point(349, 22);
			this.BT_SelectProcess.Name = "BT_SelectProcess";
			this.BT_SelectProcess.Size = new System.Drawing.Size(112, 31);
			this.BT_SelectProcess.TabIndex = 1;
			this.BT_SelectProcess.Text = "Select process";
			this.BT_SelectProcess.UseVisualStyleBackColor = true;
			this.BT_SelectProcess.Click += new System.EventHandler(this.BT_SelectProcess_Click);
			// 
			// LBL_Process
			// 
			this.LBL_Process.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LBL_Process.Location = new System.Drawing.Point(6, 22);
			this.LBL_Process.Name = "LBL_Process";
			this.LBL_Process.Size = new System.Drawing.Size(337, 31);
			this.LBL_Process.TabIndex = 0;
			this.LBL_Process.Text = "Loading...";
			this.LBL_Process.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TIM_Process
			// 
			this.TIM_Process.Tick += new System.EventHandler(this.ProcessTimerTick);
			// 
			// GPB_Audio
			// 
			this.GPB_Audio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GPB_Audio.Controls.Add(this.label2);
			this.GPB_Audio.Controls.Add(this.LBL_AudioInputs);
			this.GPB_Audio.Controls.Add(this.CBB_AudioOutputs);
			this.GPB_Audio.Controls.Add(this.CBB_AudioInputs);
			this.GPB_Audio.Location = new System.Drawing.Point(12, 127);
			this.GPB_Audio.Name = "GPB_Audio";
			this.GPB_Audio.Size = new System.Drawing.Size(467, 156);
			this.GPB_Audio.TabIndex = 2;
			this.GPB_Audio.TabStop = false;
			this.GPB_Audio.Text = "Audio";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Output Device :";
			// 
			// LBL_AudioInputs
			// 
			this.LBL_AudioInputs.AutoSize = true;
			this.LBL_AudioInputs.Location = new System.Drawing.Point(6, 25);
			this.LBL_AudioInputs.Name = "LBL_AudioInputs";
			this.LBL_AudioInputs.Size = new System.Drawing.Size(79, 15);
			this.LBL_AudioInputs.TabIndex = 2;
			this.LBL_AudioInputs.Text = "Input Device :";
			// 
			// CBB_AudioOutputs
			// 
			this.CBB_AudioOutputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CBB_AudioOutputs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBB_AudioOutputs.FormattingEnabled = true;
			this.CBB_AudioOutputs.Location = new System.Drawing.Point(107, 51);
			this.CBB_AudioOutputs.Name = "CBB_AudioOutputs";
			this.CBB_AudioOutputs.Size = new System.Drawing.Size(354, 23);
			this.CBB_AudioOutputs.TabIndex = 1;
			// 
			// CBB_AudioInputs
			// 
			this.CBB_AudioInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CBB_AudioInputs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBB_AudioInputs.FormattingEnabled = true;
			this.CBB_AudioInputs.Location = new System.Drawing.Point(107, 22);
			this.CBB_AudioInputs.Name = "CBB_AudioInputs";
			this.CBB_AudioInputs.Size = new System.Drawing.Size(354, 23);
			this.CBB_AudioInputs.TabIndex = 0;
			// 
			// TIM_SendPosition
			// 
			this.TIM_SendPosition.Interval = 500;
			this.TIM_SendPosition.Tick += new System.EventHandler(this.UpdatePositionTick);
			// 
			// TIM_KeepAlive
			// 
			this.TIM_KeepAlive.Interval = 5000;
			this.TIM_KeepAlive.Tick += new System.EventHandler(this.KeepAliveTick);
			// 
			// GPB_Around
			// 
			this.GPB_Around.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GPB_Around.Controls.Add(this.PAN_AroundContent);
			this.GPB_Around.Location = new System.Drawing.Point(12, 290);
			this.GPB_Around.Name = "GPB_Around";
			this.GPB_Around.Size = new System.Drawing.Size(467, 298);
			this.GPB_Around.TabIndex = 7;
			this.GPB_Around.TabStop = false;
			this.GPB_Around.Text = "Around";
			// 
			// PAN_AroundContent
			// 
			this.PAN_AroundContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PAN_AroundContent.AutoScroll = true;
			this.PAN_AroundContent.BackColor = System.Drawing.Color.Transparent;
			this.PAN_AroundContent.Location = new System.Drawing.Point(0, 7);
			this.PAN_AroundContent.Name = "PAN_AroundContent";
			this.PAN_AroundContent.Size = new System.Drawing.Size(467, 291);
			this.PAN_AroundContent.TabIndex = 0;
			// 
			// TIM_UpdateControls
			// 
			this.TIM_UpdateControls.Enabled = true;
			this.TIM_UpdateControls.Tick += new System.EventHandler(this.UpdateControls);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(491, 600);
			this.Controls.Add(this.GPB_Around);
			this.Controls.Add(this.GPB_Audio);
			this.Controls.Add(this.GPB_Process);
			this.Controls.Add(this.LBL_Status);
			this.MaximizeBox = false;
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Eorzeans Voice";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.Shown += new System.EventHandler(this.Main_Shown);
			this.GPB_Process.ResumeLayout(false);
			this.GPB_Audio.ResumeLayout(false);
			this.GPB_Audio.PerformLayout();
			this.GPB_Around.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label LBL_Status;
		private System.Windows.Forms.GroupBox GPB_Process;
		private System.Windows.Forms.Label LBL_Process;
		private System.Windows.Forms.Button BT_SelectProcess;
		private System.Windows.Forms.Timer TIM_Process;
		private System.Windows.Forms.GroupBox GPB_Audio;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label LBL_AudioInputs;
		private System.Windows.Forms.ComboBox CBB_AudioOutputs;
		private System.Windows.Forms.ComboBox CBB_AudioInputs;
		private System.Windows.Forms.Timer TIM_LoginWait;
		private System.Windows.Forms.Timer TIM_SendPosition;
		private System.Windows.Forms.Timer TIM_KeepAlive;
		private System.Windows.Forms.GroupBox GPB_Around;
		private System.Windows.Forms.Timer TIM_UpdateControls;
		private System.Windows.Forms.Panel PAN_AroundContent;
	}
}

