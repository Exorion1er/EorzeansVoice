using System;

namespace EorzeansVoiceLib.Utils {
	[Serializable]
	public class Version {
		public int major;
		public int minor;
		public int fix;

		public Version(int major, int minor, int fix) {
			this.major = major;
			this.minor = minor;
			this.fix = fix;
		}

		public override int GetHashCode() => HashCode.Combine(major, minor, fix);
		public override bool Equals(object obj) => obj is Version version && major == version.major && minor == version.minor && fix == version.fix;
		public static bool operator ==(Version v1, Version v2) => v1.Equals(v2);
		public static bool operator !=(Version v1, Version v2) => v1.Equals(v2) == false;
		public static bool operator >(Version v1, Version v2) => v1.major > v2.major || v1.minor > v2.minor || v1.fix > v2.fix;
		public static bool operator <(Version v1, Version v2) => v1.major < v2.major || v1.minor < v2.minor || v1.fix < v2.fix;
	}
}
