using System;
using System.Globalization;

namespace EorzeansVoiceLib.Utils {
	[Serializable]
	public struct Vector3 {
		public Vector3(float x, float y, float z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public float x;
		public float y;
		public float z;

		public static Vector3 Zero { get; } = new Vector3(0f, 0f, 0f);
		public static Vector3 Right { get; } = new Vector3(1F, 0F, 0F);

		public static float Distance(Vector3 a, Vector3 b) {
			float diff_x = a.x - b.x;
			float diff_y = a.y - b.y;
			float diff_z = a.z - b.z;
			return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
		}

		public static float Dot(Vector3 lhs, Vector3 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z; }

		public override string ToString() {
			return ToString(null, null);
		}

		public string ToString(string format) {
			return ToString(format, null);
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			if (string.IsNullOrEmpty(format)) {
				format = "F2";
			}
			if (formatProvider == null) {
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return string.Format("({0}, {1}, {2})", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider));
		}

		public override bool Equals(object obj) {
			return obj is Vector3 vector &&
				   x == vector.x &&
				   y == vector.y &&
				   z == vector.z;
		}

		public override int GetHashCode() {
			return HashCode.Combine(x, y, z);
		}

		public static bool operator ==(Vector3 a, Vector3 b) => a.Equals(b);
		public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);

		public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); }
		public static Vector3 operator -(Vector3 a, Vector3 b) { return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z); }
		public static Vector3 operator *(Vector3 a, float d) { return new Vector3(a.x * d, a.y * d, a.z * d); }
	}
}
