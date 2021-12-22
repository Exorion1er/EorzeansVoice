using System;
using Version = EorzeansVoiceLib.Utils.Version;

namespace EorzeansVoiceLib.NetworkMessageContent {
	[Serializable]
	public class VersionCheck {
		public Version clientVersion;
		public Version serverVersion;

		public VersionCheck(Version clientVersion, Version serverVersion) {
			this.clientVersion = clientVersion;
			this.serverVersion = serverVersion;
		}
	}
}
