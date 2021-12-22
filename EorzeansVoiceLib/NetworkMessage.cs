using EorzeansVoiceLib.Enums;
using System;

namespace EorzeansVoiceLib {
	[Serializable]
	public class NetworkMessage {
		public NetworkMessageType type;
		public dynamic content;

		public NetworkMessage(NetworkMessageType type, object content) {
			this.type = type;
			this.content = content;
		}
	}
}
