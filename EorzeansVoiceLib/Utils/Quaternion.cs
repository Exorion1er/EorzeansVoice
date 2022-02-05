using System;

namespace EorzeansVoiceLib.Utils {
	public struct Quaternion {
		public Quaternion(float x, float y, float z, float w) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public float x;
		public float y;
		public float z;
		public float w;

		public static Quaternion Euler(float x, float y, float z) {
			return FromEularRad(new Vector3(x, y, z) * (float)(Math.PI / 180.0));
		}

		private static Quaternion FromEularRad(Vector3 euler) {
			var yaw = euler.x;
			var pitch = euler.y;
			var roll = euler.z;
			float rollOver2 = roll * 0.5f;
			float sinRollOver2 = (float)Math.Sin((float)rollOver2);
			float cosRollOver2 = (float)Math.Cos((float)rollOver2);
			float pitchOver2 = pitch * 0.5f;
			float sinPitchOver2 = (float)Math.Sin((float)pitchOver2);
			float cosPitchOver2 = (float)Math.Cos((float)pitchOver2);
			float yawOver2 = yaw * 0.5f;
			float sinYawOver2 = (float)Math.Sin((float)yawOver2);
			float cosYawOver2 = (float)Math.Cos((float)yawOver2);
			Quaternion result;
			result.x = sinYawOver2 * cosPitchOver2 * cosRollOver2 + cosYawOver2 * sinPitchOver2 * sinRollOver2; // confirmed (scc+css)
			result.y = cosYawOver2 * sinPitchOver2 * cosRollOver2 - sinYawOver2 * cosPitchOver2 * sinRollOver2; // confirmed (csc-scs)
			result.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2; // confirmed (ccs-ssc)
			result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2; // confirmed (ccc+sss)
			return result;
		}

		public static Vector3 operator *(Quaternion rotation, Vector3 point) {
			float x = rotation.x * 2F;
			float y = rotation.y * 2F;
			float z = rotation.z * 2F;
			float xx = rotation.x * x;
			float yy = rotation.y * y;
			float zz = rotation.z * z;
			float xy = rotation.x * y;
			float xz = rotation.x * z;
			float yz = rotation.y * z;
			float wx = rotation.w * x;
			float wy = rotation.w * y;
			float wz = rotation.w * z;

			Vector3 res;
			res.x = (1F - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
			res.y = (xy + wz) * point.x + (1F - (xx + zz)) * point.y + (yz - wx) * point.z;
			res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1F - (xx + yy)) * point.z;
			return res;
		}
	}
}
