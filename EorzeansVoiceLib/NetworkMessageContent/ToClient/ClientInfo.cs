using EorzeansVoice.Utils;
using System;
using System.Collections.Generic;

namespace EorzeansVoiceLib.NetworkMessageContent {
	[Serializable]
	public class ClientInfo {
		public int id;
		public string name;
		public Vector3 position;

		public static ClientInfo FromClient(Client client) {
			return new ClientInfo {
				id = client.id,
				name = client.name,
				position = client.position
			};
		}

		public static List<ClientInfo> FromClients(List<Client> clients) {
			List<ClientInfo> infos = new List<ClientInfo>();
			foreach (Client c in clients) {
				infos.Add(FromClient(c));
			}
			return infos;
		}
	}
}
