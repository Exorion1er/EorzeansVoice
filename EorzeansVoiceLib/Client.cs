using EorzeansVoice.Utils;
using System;
using System.Collections.Generic;

namespace EorzeansVoiceLib {
	public class Client {
		public int id;
		public string ipAddress;
		public int port;
		//public string server;
		public string name;
		public int mapID;
		public int instanceID;
		public Vector3 position;

		public override string ToString() {
			return name + " (" + id + ")";
		}

		public string ToStringDetailed() {
			return ToString() + " (map:" + mapID + "|instance:" + instanceID + ") (" + ipAddress + ":" + port + ")"; // Add server
		}
	}
}
