using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using EorzeansVoiceLib;
using EorzeansVoiceLib.Utils;
using EorzeansVoiceLib.Enums;
using EorzeansVoiceLib.NetworkMessageContent;
using EorzeansVoice.Utils;
using System.Linq;

namespace EorzeansVoiceServer {
	public class Program {
		private static readonly List<Client> clients = new List<Client>();
		private static readonly bool verboseLogging = true;

		public static void Main() { // Check args for verbose logging
			ReceiveDataLoop();
		}

		private static void ReceiveDataLoop() {
			UdpClient udpClient = new UdpClient(NetworkConsts.port);
			while (true) {
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, NetworkConsts.port);
				byte[] bytes = udpClient.Receive(ref remoteEP);
				NetworkMessage received = bytes.ToMessage();

				NetworkMessage reply = Process(remoteEP, received.type, bytes);
				if (reply != null) {
					byte[] replyBytes = reply.ToBytes();
					udpClient.Send(replyBytes, replyBytes.Length, remoteEP);
				}
			}
		}

		private static string Prefix() {
			return "[" + DateTime.Now.ToString("T") + "] ";
		}

		private static NetworkMessage Process(IPEndPoint remoteEP, NetworkMessageType type, byte[] bytes) {
			NetworkMessage reply = null;

			switch (type) {
				case NetworkMessageType.Ping:
					reply = Ping(remoteEP);
					break;
				case NetworkMessageType.VersionCheck:
					reply = VersionCheck(remoteEP, bytes.ToMessage());
					break;
				case NetworkMessageType.Connect:
					reply = Connect(remoteEP, bytes.ToMessage());
					break;
				case NetworkMessageType.UpdateServer:
					reply = UpdateServer(bytes.ToMessage());
					break;
				case NetworkMessageType.SendVoiceToServer:
					ReceiveVoice(remoteEP, bytes.ToMessage());
					break;
				case NetworkMessageType.Disconnect:
					Console.WriteLine(Prefix() + "Received unhandled DISCONNECT message.");
					break;
			}

			return reply;
		}

		private static NetworkMessage Ping(IPEndPoint remoteEP) {
			if (verboseLogging) {
				Console.WriteLine(Prefix() + "Received ping from " + remoteEP.ToString() + ", answering pong.");
			}

			return new NetworkMessage(NetworkMessageType.Pong, "Pong");
		}

		private static NetworkMessage VersionCheck(IPEndPoint remoteEP, NetworkMessage received) {
			VersionCheck vCheck = received.content.ToObject<VersionCheck>();
			NetworkMessage reply = new NetworkMessage(NetworkMessageType.VersionCheckResult, VersionCheckAnswer.UpToDate);
			string verboseResult = "Up to date.";
			if (vCheck.clientVersion < NetworkConsts.clientVersion) {
				reply.content = VersionCheckAnswer.ClientOutOfDate;
				verboseResult = "Remote client is outdated !";
			} else if (vCheck.serverVersion < NetworkConsts.serverVersion) {
				reply.content = VersionCheckAnswer.ServerOutOfDate;
				verboseResult = "This server is outdated !";
			}

			if (verboseLogging) {
				Console.WriteLine(Prefix() + remoteEP.ToString() + " is checking version. " + verboseResult);
			}

			return reply;
		}

		private static NetworkMessage Connect(IPEndPoint remoteEP, NetworkMessage received) {
			Connect connect = received.content.ToObject<Connect>();

			Client newClient = new Client {
				id = GetNewID(),
				ipAddress = remoteEP.Address.ToString(),
				port = remoteEP.Port,
				worldID = connect.worldID,
				name = connect.name,
				mapID = connect.mapID,
				instanceID = connect.instanceID,
				position = connect.position
			};

			if (verboseLogging) {
				Console.WriteLine(Prefix() + newClient.ToStringDetailed() + " is now connected.");
			}

			clients.Add(newClient);

			return new NetworkMessage(NetworkMessageType.Connected, newClient.id);
		}

		private static NetworkMessage UpdateServer(NetworkMessage received) {
			UpdateServer newInfo = received.content.ToObject<UpdateServer>();
			Client client = clients.Find(x => x.id == newInfo.id);

			client.worldID = newInfo.worldID;
			client.mapID = newInfo.mapID;
			client.instanceID = newInfo.instanceID;
			client.position = newInfo.position;

			List<Client> around = clients.Where(x => x.worldID == client.worldID && x.mapID == client.mapID && x.instanceID == client.instanceID).ToList();
			around.Remove(client);
			List<ClientInfo> infoOfAround = ClientInfo.FromClients(around);

			if (verboseLogging) {
				Console.WriteLine(Prefix() + "Received update from " + client.ToString() + ", replying with info of " + infoOfAround.Count + " users around them.");
			}

			if (infoOfAround.Count > 0) {
				return new NetworkMessage(NetworkMessageType.UpdateClient, infoOfAround);
			}
			return null;
		}

		private static void ReceiveVoice(IPEndPoint remoteEP, NetworkMessage received) {
			// Opus Decode voice bytes
			// Add length to overall buffer
			// Add to client buffer
			// -> If overall buffer full
			// -> Mix all voices together
			// -> Encode with Opus again
			// -> Send back to all clients
		}

		private static int GetNewID() {
			Random rnd = new Random();
			int final = 0;
			do {
				final = rnd.Next(100000, 1000000);
			} while (clients.Exists(x => x.id == final));

			return final;
		}
	}
}
