using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EorzeansVoice.Utils {
	public partial class VolumeSlider : UserControl {
		public float Value {
			get { return value; }
			set { this.value = value; Invalidate(); }
		}

		public Color BarColor {
			get { return ((SolidBrush)barColor).Color; }
			set { barColor = new SolidBrush(value); Invalidate(); }
		}

		public Color HandleColor {
			get { return ((SolidBrush)handleColor).Color; }
			set { handleColor = new SolidBrush(value); Invalidate(); }
		}

		public float HandleWidth {
			get { return handleWidth; }
			set { handleWidth = value; Invalidate(); }
		}

		private float value = 1f;
		private Brush barColor = Brushes.Aquamarine;
		private Brush handleColor = Brushes.AliceBlue;
		private float handleWidth = 15f;

		public VolumeSlider() {
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe) {
			base.OnPaint(pe);

			DrawBar(pe, out float barWidth, out float barX);
			DrawHandle(pe, barWidth, barX);
		}

		private void DrawBar(PaintEventArgs pe, out float barWidth, out float barX) {
			float barHeightOffset = 15;
			float barHeight = Height - barHeightOffset * 2;
			float barWidthLeftOffset = 10;
			float barWidthRightOffset = 10;
			barWidth = Width - 1 - barWidthRightOffset - barWidthLeftOffset;
			barX = barWidthLeftOffset;
			float barY = Height / 2 - barHeight / 2;

			pe.Graphics.FillRectangle(barColor, barX, barY, barWidth, barHeight);
			pe.Graphics.DrawRectangle(Pens.LightGray, barX, barY, barWidth, barHeight);
		}

		private void DrawHandle(PaintEventArgs pe, float barWidth, float barX) {
			float handleHeightOffset = 5;
			float handleHeight = Height - handleHeightOffset * 2;
			float handleX = value.Normalize(0, 1, barX - handleWidth / 2, barWidth);
			float handleY = Height / 2 - handleHeight / 2;

			pe.Graphics.FillEllipse(handleColor, handleX, handleY, handleWidth, handleHeight);
			pe.Graphics.DrawEllipse(Pens.Gray, handleX, handleY, handleWidth, handleHeight);
		}
	}
}
