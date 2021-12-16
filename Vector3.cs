using System;
using System.Globalization;

namespace EorzeansVoice {
	public class Vector3 {
		public Vector3(float x, float y, float z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public float x;
		public float y;
		public float z;

		public static Vector3 Zero { get; } = new Vector3(0f, 0f, 0f);

		public static float Distance(Vector3 a, Vector3 b) {
			float diff_x = a.x - b.x;
			float diff_y = a.y - b.y;
			float diff_z = a.z - b.z;
			return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
		}

		public override string ToString() {
			return ToString(null, null);
		}

		public string ToString(string format) {
			return ToString(format, null);
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			if (string.IsNullOrEmpty(format))
				format = "F2";
			if (formatProvider == null)
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			return string.Format("({0}, {1}, {2})", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider));
		}
	}
}
