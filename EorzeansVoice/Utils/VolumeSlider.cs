using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EorzeansVoice.Utils {
	public partial class Slider : Control {
		protected override Size DefaultSize {
			get => new Size(100, 20);
		}

		private float min = 0.0f;
		public float Min {
			get => min;
			set {
				min = value;
				RecalculateParameters();
			}
		}

		private float max = 1.0f;
		public float Max {
			get => max;
			set {
				max = value;
				RecalculateParameters();
			}
		}

		private float value = 0.3f;
		public float Value {
			get => value;
			set {
				this.value = value;
				ValueChanged?.Invoke(this, EventArgs.Empty);
				RecalculateParameters();
			}
		}

		private bool useActiveValue = false;
		public bool UseActiveValue {
			get => useActiveValue;
			set {
				useActiveValue = value;
				RecalculateParameters();
			}
		}

		private float activeValue = 0.3f;
		public float ActiveValue {
			get => activeValue;
			set {
				activeValue = value;
				RecalculateParameters();
			}
		}

		private Color activeBarColor = SystemColors.ActiveCaption;
		public Color ActivebarColor {
			get => activeBarColor;
			set {
				activeBarColor = value;
				Invalidate();
			}
		}

		private Color inactiveBarColor = SystemColors.ControlDark;
		public Color InactiveBarColor {
			get => inactiveBarColor;
			set {
				inactiveBarColor = value;
				Invalidate();
			}
		}

		private Color handleColor = SystemColors.Highlight;
		public Color HandleColor {
			get => handleColor;
			set {
				handleColor = value;
				Invalidate();
			}
		}

		public event EventHandler ValueChanged;

		private float radius;
		private PointF thumbPos;
		private PointF activePos;
		private SizeF barSize;
		private PointF barPos;
		private bool moving = false;
		private SizeF delta;

		public Slider() {
			// This reduces flicker
			DoubleBuffered = true;
			RecalculateParameters();
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);

			SolidBrush activeBarBrush = new SolidBrush(activeBarColor);
			SolidBrush inactiveBarBrush = new SolidBrush(inactiveBarColor);
			SolidBrush handleBrush = new SolidBrush(handleColor);

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.FillRectangle(inactiveBarBrush, barPos.X, barPos.Y, barSize.Width, barSize.Height);
			e.Graphics.FillRectangle(activeBarBrush, barPos.X, barPos.Y, activePos.X - barPos.X, barSize.Height);

			e.Graphics.FillCircle(handleBrush, thumbPos.X, thumbPos.Y, radius);
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			RecalculateParameters();
		}

		private void RecalculateParameters() {
			float valueToUse = useActiveValue ? activeValue : value;

			radius = 0.5f * (ClientSize.Height - 1);
			barSize = new SizeF(ClientSize.Width - 2f * radius, 0.5f * ClientSize.Height);
			barPos = new PointF(radius, (ClientSize.Height - barSize.Height) / 2);
			thumbPos = new PointF(barSize.Width / (max - min) * value + barPos.X, barPos.Y + 0.5f * barSize.Height);
			activePos = new PointF(barSize.Width / (max - min) * valueToUse + barPos.X, barPos.Y + 0.5F * barSize.Height);
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);

			// Difference between tumb and mouse position.
			delta = new SizeF(e.Location.X - thumbPos.X, e.Location.Y - thumbPos.Y);
			if (delta.Width * delta.Width + delta.Height * delta.Height <= radius * radius) {
				// Clicking inside thumb.
				moving = true;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if (moving) {
				float thumbX = e.Location.X - delta.Width;
				if (thumbX < barPos.X) {
					thumbX = barPos.X;
				} else if (thumbX > barPos.X + barSize.Width) {
					thumbX = barPos.X + barSize.Width;
				}
				Value = (thumbX - barPos.X) * (Max - Min) / barSize.Width;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			moving = false;
		}
	}
}
