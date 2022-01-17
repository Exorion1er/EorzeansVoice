using EorzeansVoiceLib;
using EorzeansVoiceLib.Enums;
using EorzeansVoiceLib.NetworkMessageContent;
using EorzeansVoiceLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Timers;

namespace EorzeansVoiceServer {
	public class Program {
		private static readonly List<Client> clients = new List<Client>();
		private static readonly Timer TIM_CheckOffline = new Timer();

		public static void Main() {
			Logging.console = Logging.LogType.Debug; // Replace with arg
			Logging.file = Logging.LogType.Info; // Replace with arg
			Logging.fileName = "Log"; // Replace with arg
			Logging.Info("##### Eorzean's Voice " + NetworkConsts.serverVersion + " #####\n");

			TIM_CheckOffline.Interval = 1000;
			TIM_CheckOffline.Elapsed += TIM_CheckOffline_Elapsed;
			TIM_CheckOffline.Enabled = true;

			Network.Start();
			Logging.Info("Listening...");

			while (true) {
				// Handle line commands
				Console.ReadLine();
			}
		}

		public static void Process(IPEndPoint remoteEP, NetworkMessage msg) {
			switch (msg.type) {
				case NetworkMessageType.Ping:
					Ping(remoteEP);
					break;
				case NetworkMessageType.VersionCheck:
					VersionCheck(remoteEP, msg);
					break;
				case NetworkMessageType.Connect:
					Connect(remoteEP, msg);
					break;
				case NetworkMessageType.UpdateServer:
					UpdateServer(remoteEP, msg);
					break;
				case NetworkMessageType.SendVoiceToServer:
					ReceiveVoice(remoteEP, msg);
					break;
				case NetworkMessageType.KeepAlive:
					KeepAlive(remoteEP, msg);
					break;
				case NetworkMessageType.Disconnect:
					UserDisconnecting(msg);
					break;
			}
		}

		private static void Ping(IPEndPoint remoteEP) {
			Logging.Debug("Received ping from " + remoteEP.ToString() + ", answering pong.");
			Network.SendMessage(new NetworkMessage(NetworkMessageType.Pong, "Pong"), remoteEP);
		}

		private static void VersionCheck(IPEndPoint remoteEP, NetworkMessage received) {
			VersionCheck vCheck = received.content.ToObject<VersionCheck>();
			NetworkMessage reply = new NetworkMessage(NetworkMessageType.VersionCheckResult, VersionCheckAnswer.UpToDate);
			string verboseResult = "Up to date.";
			if (vCheck.clientVersion < NetworkConsts.clientVersion) {
				reply.content = VersionCheckAnswer.ClientOutOfDate;
				verboseResult = "Remote client is outdated !";
			} else if (vCheck.serverVersion < NetworkConsts.serverVersion) {
				reply.content = VersionCheckAnswer.ServerOutOfDate;
				verboseResult = "This server is outdated !";

				Logging.Warn("##### Please update this server ! New version : " + vCheck.serverVersion.ToString() + " #####");
			}

			Logging.Debug(remoteEP.ToString() + " is checking version. " + verboseResult);
			Network.SendMessage(reply, remoteEP);
		}

		private static void Connect(IPEndPoint remoteEP, NetworkMessage received) {
			Connect connect = received.content.ToObject<Connect>();

			Client newClient = new Client {
				id = GetNewID(),
				ipAddress = remoteEP.Address,
				port = remoteEP.Port,
				worldID = connect.worldID,
				name = connect.name,
				mapID = connect.mapID,
				instanceID = connect.instanceID,
				position = connect.position,
				lastReceived = DateTime.Now
			};

			clients.Add(newClient);

			NetworkMessage reply = new NetworkMessage(NetworkMessageType.Connected, newClient.id);
			Network.SendMessage(reply, remoteEP);
			SendUpdateInfo(newClient.GetAround(clients));

			Logging.Info(newClient.ToStringDetailed() + " is now connected.");
		}

		private static void UpdateServer(IPEndPoint remoteEP, NetworkMessage received) {
			UpdateServer newInfo = received.content.ToObject<UpdateServer>();
			Client client = clients.FirstOrDefault(x => x.id == newInfo.id);

			if (client == null) {
				Logging.Warn("Received update from non-existant client.");
				ForceDisconnect(remoteEP, "UpdateServer");
				return;
			}

			List<Client> aroundBefore = client.GetAround(clients); // Store around if client teleported
			bool teleported = UpdateInfo(client, newInfo); // Update info and check if client teleported

			SendUpdateInfo(client); // Send around info to client
			SendUpdateInfo(client.GetAround(clients)); // Send around info to around clients

			if (teleported) {
				SendUpdateInfo(aroundBefore); // If teleported, send around info to around clients before teleporting
			}

			// Optimization : Instead of sending everyone information about everyone, only send information about what changed

			Logging.Debug("Received update from " + client.ToString());
		}

		private static bool UpdateInfo(Client c, UpdateServer newInfo) {
			bool teleported = false;

			if (c.worldID != newInfo.worldID) {
				teleported = true;
				c.worldID = newInfo.worldID;
			}

			if (c.mapID != newInfo.mapID) {
				teleported = true;
				c.mapID = newInfo.mapID;
			}

			if (c.instanceID != newInfo.instanceID) {
				teleported = true;
				c.instanceID = newInfo.instanceID;
			}

			c.position = newInfo.position;
			c.lastReceived = DateTime.Now;

			return teleported;
		}

		private static void SendUpdateInfo(Client c) {
			List<ClientInfo> infoOfAround = ClientInfo.FromClients(c.GetAround(clients));
			Logging.Debug("Sending update to " + c.ToString());
			Network.SendMessage(new NetworkMessage(NetworkMessageType.UpdateClient, infoOfAround), c);
		}

		private static void SendUpdateInfo(List<Client> c) {
			foreach (Client client in c) {
				SendUpdateInfo(client);
			}
		}

		private static void ReceiveVoice(IPEndPoint remoteEP, NetworkMessage received) {
			SendVoice msg = received.content.ToObject<SendVoice>();
			Client client = clients.FirstOrDefault(x => x.id == msg.id);

			if (client == null) {
				Logging.Warn("Received voice from non-existant client.");
				ForceDisconnect(remoteEP, "ReceiveVoice");
				return;
			}

			client.lastReceived = DateTime.Now;

			SendVoice voice = new SendVoice() {
				id = msg.id,
				data = msg.data
			};

			NetworkMessage toOtherUsers = new NetworkMessage(NetworkMessageType.SendVoiceToClient, voice);
			foreach (Client c in clients) {
				if (c.id == msg.id) {
					continue;
				}

				Network.SendMessage(toOtherUsers, c);
			}
		}

		private static void KeepAlive(IPEndPoint remoteEP, NetworkMessage received) {
			int userID = (int)received.content;
			Client client = clients.FirstOrDefault(x => x.id == userID);

			if (client == null) {
				Logging.Warn("Received Keep Alive from non-existant client.");
				ForceDisconnect(remoteEP, "KeepAlive");
				return;
			}

			Logging.Debug("Received Keep Alive from " + client.ToString());
			client.lastReceived = DateTime.Now;
		}

		private static void UserDisconnecting(NetworkMessage received) {
			int userID = (int)received.content;
			Client client = clients.FirstOrDefault(x => x.id == userID);

			if (client == null) {
				Logging.Warn("Received Disconnect from non-existant client.");
				return;
			}

			Logging.Info(client.ToString() + " disconnecting.");
			clients.Remove(client);

			Logging.Debug("Sending update to clients around " + client.GetAround(clients));
			SendUpdateInfo(client.GetAround(clients));
		}

		private static void ForceDisconnect(IPEndPoint remoteEP, string error) {
			NetworkMessage msg = new NetworkMessage(NetworkMessageType.ForceDisconnect, error);
			Network.SendMessage(msg, remoteEP);
		}

		private static void ForceDisconnect(Client c, string error) {
			IPEndPoint remoteEP = new IPEndPoint(c.ipAddress, c.port);
			ForceDisconnect(remoteEP, error);

			Logging.Debug("Sending update to clients " + c.GetAround(clients));
			clients.Remove(c);
			SendUpdateInfo(c.GetAround(clients));
		}

		private static void TIM_CheckOffline_Elapsed(object sender, ElapsedEventArgs e) {
			DateTime now = DateTime.Now;

			foreach (Client c in clients.ToList()) {
				if (c.lastReceived < now - TimeSpan.FromSeconds(30)) {
					Logging.Info(c.ToString() + " stopped updating, disconnecting.");
					ForceDisconnect(c, "CheckOffline");
					clients.Remove(c);
				}
			}
		}

		private static int GetNewID() {
			Random rnd = new Random();
			int final = 0;
			do {
				final = rnd.Next(1, 1000000);
			} while (clients.Exists(x => x.id == final));

			return final;
		}
	}
}
