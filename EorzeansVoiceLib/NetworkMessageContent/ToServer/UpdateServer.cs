using EorzeansVoiceLib.Utils;
using System;

namespace EorzeansVoiceLib.NetworkMessageContent {
	public struct UpdateServer {
		public int id;
		public Vector3 position;
		public short worldID;
		public int mapID;
		public int instanceID;

		public override bool Equals(object obj) {
			return obj is UpdateServer server &&
				   id == server.id &&
				   position == server.position &&
				   worldID == server.worldID &&
				   mapID == server.mapID &&
				   instanceID == server.instanceID;
		}

		public override int GetHashCode() {
			return HashCode.Combine(id, position, worldID, mapID, instanceID);
		}

		public static bool operator ==(UpdateServer a, UpdateServer b) {
			return a.Equals(b);
		}
		public static bool operator !=(UpdateServer a, UpdateServer b) => !(a == b);
	}
}
