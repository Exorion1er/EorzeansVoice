using EorzeansVoiceLib.Utils;

namespace EorzeansVoiceLib.NetworkMessageContent {
	public class Connect {
		public short worldID;
		public string name;
		public int mapID;
		public int instanceID;
		public Vector3 position;
	}
}
