
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
			this.BT_PTTKeybind = new System.Windows.Forms.Button();
			this.VSL_VoiceActivation = new NAudio.Gui.VolumeSlider();
			this.RBT_PushToTalk = new System.Windows.Forms.RadioButton();
			this.RBT_VoiceActivation = new System.Windows.Forms.RadioButton();
			this.LBL_GlobalVolume = new System.Windows.Forms.Label();
			this.LBL_GlobalVolumeName = new System.Windows.Forms.Label();
			this.BT_Mute = new System.Windows.Forms.Button();
			this.TBR_GlobalVolume = new System.Windows.Forms.TrackBar();
			this.BT_Deafen = new System.Windows.Forms.Button();
			this.LBL_AudioOutputs = new System.Windows.Forms.Label();
			this.LBL_AudioInputs = new System.Windows.Forms.Label();
			this.CBB_AudioOutputs = new System.Windows.Forms.ComboBox();
			this.CBB_AudioInputs = new System.Windows.Forms.ComboBox();
			this.TIM_LoginWait = new System.Windows.Forms.Timer(this.components);
			this.TIM_SendInfo = new System.Windows.Forms.Timer(this.components);
			this.TIM_KeepAlive = new System.Windows.Forms.Timer(this.components);
			this.GPB_Around = new System.Windows.Forms.GroupBox();
			this.PAN_AroundContent = new System.Windows.Forms.Panel();
			this.volumeSlider1 = new EorzeansVoice.Utils.VolumeSlider();
			this.TIM_UpdateControls = new System.Windows.Forms.Timer(this.components);
			this.GPB_Process.SuspendLayout();
			this.GPB_Audio.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TBR_GlobalVolume)).BeginInit();
			this.GPB_Around.SuspendLayout();
			this.PAN_AroundContent.SuspendLayout();
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
			this.GPB_Audio.Controls.Add(this.BT_PTTKeybind);
			this.GPB_Audio.Controls.Add(this.VSL_VoiceActivation);
			this.GPB_Audio.Controls.Add(this.RBT_PushToTalk);
			this.GPB_Audio.Controls.Add(this.RBT_VoiceActivation);
			this.GPB_Audio.Controls.Add(this.LBL_GlobalVolume);
			this.GPB_Audio.Controls.Add(this.LBL_GlobalVolumeName);
			this.GPB_Audio.Controls.Add(this.BT_Mute);
			this.GPB_Audio.Controls.Add(this.TBR_GlobalVolume);
			this.GPB_Audio.Controls.Add(this.BT_Deafen);
			this.GPB_Audio.Controls.Add(this.LBL_AudioOutputs);
			this.GPB_Audio.Controls.Add(this.LBL_AudioInputs);
			this.GPB_Audio.Controls.Add(this.CBB_AudioOutputs);
			this.GPB_Audio.Controls.Add(this.CBB_AudioInputs);
			this.GPB_Audio.Location = new System.Drawing.Point(12, 127);
			this.GPB_Audio.Name = "GPB_Audio";
			this.GPB_Audio.Size = new System.Drawing.Size(467, 195);
			this.GPB_Audio.TabIndex = 2;
			this.GPB_Audio.TabStop = false;
			this.GPB_Audio.Text = "Audio";
			// 
			// BT_PTTKeybind
			// 
			this.BT_PTTKeybind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BT_PTTKeybind.Location = new System.Drawing.Point(125, 160);
			this.BT_PTTKeybind.Name = "BT_PTTKeybind";
			this.BT_PTTKeybind.Size = new System.Drawing.Size(336, 23);
			this.BT_PTTKeybind.TabIndex = 12;
			this.BT_PTTKeybind.Text = "Unbound";
			this.BT_PTTKeybind.UseVisualStyleBackColor = true;
			// 
			// VSL_VoiceActivation
			// 
			this.VSL_VoiceActivation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.VSL_VoiceActivation.Location = new System.Drawing.Point(125, 131);
			this.VSL_VoiceActivation.Name = "VSL_VoiceActivation";
			this.VSL_VoiceActivation.Size = new System.Drawing.Size(336, 23);
			this.VSL_VoiceActivation.TabIndex = 11;
			this.VSL_VoiceActivation.VolumeChanged += new System.EventHandler(this.VSL_VoiceActivation_VolumeChanged);
			// 
			// RBT_PushToTalk
			// 
			this.RBT_PushToTalk.AutoSize = true;
			this.RBT_PushToTalk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.RBT_PushToTalk.Location = new System.Drawing.Point(27, 162);
			this.RBT_PushToTalk.Name = "RBT_PushToTalk";
			this.RBT_PushToTalk.Size = new System.Drawing.Size(89, 19);
			this.RBT_PushToTalk.TabIndex = 10;
			this.RBT_PushToTalk.Text = "Push To Talk";
			this.RBT_PushToTalk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.RBT_PushToTalk.UseVisualStyleBackColor = true;
			this.RBT_PushToTalk.CheckedChanged += new System.EventHandler(this.VoiceModeChanged);
			// 
			// RBT_VoiceActivation
			// 
			this.RBT_VoiceActivation.AutoSize = true;
			this.RBT_VoiceActivation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.RBT_VoiceActivation.Checked = true;
			this.RBT_VoiceActivation.Location = new System.Drawing.Point(6, 133);
			this.RBT_VoiceActivation.Name = "RBT_VoiceActivation";
			this.RBT_VoiceActivation.Size = new System.Drawing.Size(110, 19);
			this.RBT_VoiceActivation.TabIndex = 9;
			this.RBT_VoiceActivation.TabStop = true;
			this.RBT_VoiceActivation.Text = "Voice Activation";
			this.RBT_VoiceActivation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.RBT_VoiceActivation.UseVisualStyleBackColor = true;
			this.RBT_VoiceActivation.CheckedChanged += new System.EventHandler(this.VoiceModeChanged);
			// 
			// LBL_GlobalVolume
			// 
			this.LBL_GlobalVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LBL_GlobalVolume.Location = new System.Drawing.Point(321, 80);
			this.LBL_GlobalVolume.Name = "LBL_GlobalVolume";
			this.LBL_GlobalVolume.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.LBL_GlobalVolume.Size = new System.Drawing.Size(38, 45);
			this.LBL_GlobalVolume.TabIndex = 8;
			this.LBL_GlobalVolume.Text = "100%";
			this.LBL_GlobalVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LBL_GlobalVolumeName
			// 
			this.LBL_GlobalVolumeName.AutoSize = true;
			this.LBL_GlobalVolumeName.Location = new System.Drawing.Point(6, 95);
			this.LBL_GlobalVolumeName.Name = "LBL_GlobalVolumeName";
			this.LBL_GlobalVolumeName.Size = new System.Drawing.Size(90, 15);
			this.LBL_GlobalVolumeName.TabIndex = 7;
			this.LBL_GlobalVolumeName.Text = "Global Volume :";
			// 
			// BT_Mute
			// 
			this.BT_Mute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BT_Mute.Location = new System.Drawing.Point(365, 80);
			this.BT_Mute.Name = "BT_Mute";
			this.BT_Mute.Size = new System.Drawing.Size(45, 45);
			this.BT_Mute.TabIndex = 5;
			this.BT_Mute.Text = "Mute";
			this.BT_Mute.UseVisualStyleBackColor = true;
			// 
			// TBR_GlobalVolume
			// 
			this.TBR_GlobalVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TBR_GlobalVolume.Location = new System.Drawing.Point(107, 80);
			this.TBR_GlobalVolume.Maximum = 100;
			this.TBR_GlobalVolume.Name = "TBR_GlobalVolume";
			this.TBR_GlobalVolume.Size = new System.Drawing.Size(208, 45);
			this.TBR_GlobalVolume.TabIndex = 6;
			this.TBR_GlobalVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.TBR_GlobalVolume.Value = 100;
			// 
			// BT_Deafen
			// 
			this.BT_Deafen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BT_Deafen.Location = new System.Drawing.Point(416, 80);
			this.BT_Deafen.Name = "BT_Deafen";
			this.BT_Deafen.Size = new System.Drawing.Size(45, 45);
			this.BT_Deafen.TabIndex = 4;
			this.BT_Deafen.Text = "Deafen";
			this.BT_Deafen.UseVisualStyleBackColor = true;
			// 
			// LBL_AudioOutputs
			// 
			this.LBL_AudioOutputs.AutoSize = true;
			this.LBL_AudioOutputs.Location = new System.Drawing.Point(6, 54);
			this.LBL_AudioOutputs.Name = "LBL_AudioOutputs";
			this.LBL_AudioOutputs.Size = new System.Drawing.Size(89, 15);
			this.LBL_AudioOutputs.TabIndex = 3;
			this.LBL_AudioOutputs.Text = "Output Device :";
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
			// TIM_SendInfo
			// 
			this.TIM_SendInfo.Interval = 200;
			this.TIM_SendInfo.Tick += new System.EventHandler(this.SendInfoTick);
			// 
			// TIM_KeepAlive
			// 
			this.TIM_KeepAlive.Interval = 1000;
			this.TIM_KeepAlive.Tick += new System.EventHandler(this.KeepAliveTick);
			// 
			// GPB_Around
			// 
			this.GPB_Around.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GPB_Around.Controls.Add(this.PAN_AroundContent);
			this.GPB_Around.Location = new System.Drawing.Point(12, 328);
			this.GPB_Around.Name = "GPB_Around";
			this.GPB_Around.Size = new System.Drawing.Size(467, 260);
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
			this.PAN_AroundContent.Controls.Add(this.volumeSlider1);
			this.PAN_AroundContent.Location = new System.Drawing.Point(0, 22);
			this.PAN_AroundContent.Name = "PAN_AroundContent";
			this.PAN_AroundContent.Size = new System.Drawing.Size(467, 238);
			this.PAN_AroundContent.TabIndex = 0;
			// 
			// volumeSlider1
			// 
			this.volumeSlider1.BarColor = System.Drawing.SystemColors.ControlLight;
			this.volumeSlider1.HandleColor = System.Drawing.SystemColors.Highlight;
			this.volumeSlider1.HandleWidth = 15F;
			this.volumeSlider1.Location = new System.Drawing.Point(64, 55);
			this.volumeSlider1.MinimumSize = new System.Drawing.Size(50, 40);
			this.volumeSlider1.Name = "volumeSlider1";
			this.volumeSlider1.Size = new System.Drawing.Size(306, 40);
			this.volumeSlider1.TabIndex = 0;
			this.volumeSlider1.Value = 1F;
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
			((System.ComponentModel.ISupportInitialize)(this.TBR_GlobalVolume)).EndInit();
			this.GPB_Around.ResumeLayout(false);
			this.PAN_AroundContent.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label LBL_Status;
		private System.Windows.Forms.GroupBox GPB_Process;
		private System.Windows.Forms.Label LBL_Process;
		private System.Windows.Forms.Button BT_SelectProcess;
		private System.Windows.Forms.Timer TIM_Process;
		private System.Windows.Forms.GroupBox GPB_Audio;
		private System.Windows.Forms.Label LBL_AudioOutputs;
		private System.Windows.Forms.Label LBL_AudioInputs;
		private System.Windows.Forms.ComboBox CBB_AudioOutputs;
		private System.Windows.Forms.ComboBox CBB_AudioInputs;
		private System.Windows.Forms.Timer TIM_LoginWait;
		private System.Windows.Forms.Timer TIM_SendInfo;
		private System.Windows.Forms.Timer TIM_KeepAlive;
		private System.Windows.Forms.GroupBox GPB_Around;
		private System.Windows.Forms.Timer TIM_UpdateControls;
		private System.Windows.Forms.Panel PAN_AroundContent;
		private System.Windows.Forms.TrackBar TBR_GlobalVolume;
		private System.Windows.Forms.Button BT_Mute;
		private System.Windows.Forms.Button BT_Deafen;
		private System.Windows.Forms.Label LBL_GlobalVolume;
		private System.Windows.Forms.Label LBL_GlobalVolumeName;
		private System.Windows.Forms.Button BT_PTTKeybind;
		private NAudio.Gui.VolumeSlider VSL_VoiceActivation;
		private System.Windows.Forms.RadioButton RBT_PushToTalk;
		private System.Windows.Forms.RadioButton RBT_VoiceActivation;
		private Utils.VolumeSlider volumeSlider1;
	}
}

