
namespace EorzeansVoice {
	partial class UC_ClientAroundRework {
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
			this.materialSlider1 = new MaterialSkin.Controls.MaterialSlider();
			this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
			this.SuspendLayout();
			// 
			// materialSlider1
			// 
			this.materialSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.materialSlider1.BackColor = System.Drawing.SystemColors.Control;
			this.materialSlider1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.materialSlider1.Depth = 0;
			this.materialSlider1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.materialSlider1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.materialSlider1.Location = new System.Drawing.Point(49, -54);
			this.materialSlider1.MouseState = MaterialSkin.MouseState.HOVER;
			this.materialSlider1.Name = "materialSlider1";
			this.materialSlider1.Size = new System.Drawing.Size(830, 40);
			this.materialSlider1.TabIndex = 0;
			this.materialSlider1.Text = "Exorion Skrat";
			this.materialSlider1.Value = 100;
			this.materialSlider1.ValueMax = 100;
			this.materialSlider1.ValueSuffix = "%";
			// 
			// materialFloatingActionButton1
			// 
			this.materialFloatingActionButton1.Depth = 0;
			this.materialFloatingActionButton1.Icon = null;
			this.materialFloatingActionButton1.Location = new System.Drawing.Point(3, 10);
			this.materialFloatingActionButton1.Mini = true;
			this.materialFloatingActionButton1.MouseState = MaterialSkin.MouseState.HOVER;
			this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
			this.materialFloatingActionButton1.Size = new System.Drawing.Size(40, 40);
			this.materialFloatingActionButton1.TabIndex = 1;
			this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
			this.materialFloatingActionButton1.UseVisualStyleBackColor = true;
			// 
			// UC_ClientAroundRework
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(882, 60);
			this.Controls.Add(this.materialFloatingActionButton1);
			this.Controls.Add(this.materialSlider1);
			this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.StatusAndActionBar_None;
			this.Name = "UC_ClientAroundRework";
			this.Opacity = 0D;
			this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.ResumeLayout(false);

		}

		#endregion

		private MaterialSkin.Controls.MaterialSlider materialSlider1;
		private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton1;
	}
}
