using EorzeansVoice.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
		public DateTime lastReceived;

		public List<Client> GetAround(List<Client> clients) {
			List<Client> around = clients.Where(x => x.worldID == worldID && x.mapID == mapID && x.instanceID == instanceID).ToList();
			around.Remove(this);
			return around;
		}

		public override string ToString() {
			return name + " (" + id + ")";
		}

		public string ToStringDetailed() {
			return ToString() + " (World:" + worldID + "|map:" + mapID + "|instance:" + instanceID + ") (" + ipAddress + ":" + port + ")";
		}
	}
}
