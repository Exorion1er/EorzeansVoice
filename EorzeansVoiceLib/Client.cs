﻿using EorzeansVoice.Utils;

namespace EorzeansVoiceLib {
	public class Client {
		public int id;
		public string ipAddress;
		public int port;
		public short worldID;
		public string name;
		public int mapID;
		public int instanceID;
		public Vector3 position;

		public override string ToString() {
			return name + " (" + id + ")";
		}

		public string ToStringDetailed() {
			return ToString() + " (World:" + worldID + "|map:" + mapID + "|instance:" + instanceID + ") (" + ipAddress + ":" + port + ")";
		}
	}
}