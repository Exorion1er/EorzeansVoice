using EorzeansVoice.Utils;
using EorzeansVoiceLib;
using EorzeansVoiceLib.Enums;
using EorzeansVoiceLib.NetworkMessageContent;
using EorzeansVoiceLib.Utils;
using System;
using System.Net;
using System.Net.Sockets;

namespace EorzeansVoice {
	public static class Network {
		private static UdpClient udpClient;

		public static bool IsNetworkWorking() {
			try {
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(NetworkConsts.networkTest.ToString());
				request.Timeout = 3000;
				request.Method = "HEAD";

				using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				return response.StatusCode == HttpStatusCode.OK;
			} catch {
				return false;
			}
		}

		public static bool IsServerWorking() {
			if (ConnectToServer()) {
				return PingServer();
			}
			return false;
		}

		public static bool ConnectToServer() {
			try {
				IPEndPoint ep = new IPEndPoint(IPAddress.Parse(NetworkConsts.serverAddr), NetworkConsts.port);
				udpClient = new UdpClient();
				udpClient.Connect(ep);
				
				return true;
			} catch {
				return false;
			}
		}

		private static bool PingServer() {
			try {
				byte[] pingMessage = new NetworkMessage(NetworkMessageType.Ping, "Ping").ToBytes();
				udpClient.Send(pingMessage, pingMessage.Length);

				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(NetworkConsts.serverAddr), NetworkConsts.port);
				byte[] received = udpClient.Receive(ref remoteEP);

				NetworkMessage msg = received.ToMessage();
				if (msg.content as string == "Pong") {
					return true;
				}
				return false;
			} catch {
				return false;
			}
		}

		public static VersionCheckAnswer IsUpToDate() {
			VersionCheck vCheck = new VersionCheck(NetworkConsts.clientVersion, NetworkConsts.serverVersion);
			byte[] versionCheckMessage = new NetworkMessage(NetworkMessageType.VersionCheck, vCheck).ToBytes();
			udpClient.Send(versionCheckMessage, versionCheckMessage.Length);

			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(NetworkConsts.serverAddr), NetworkConsts.port);
			byte[] received = udpClient.Receive(ref remoteEP);

			NetworkMessage msg = received.ToMessage();
			return (VersionCheckAnswer)msg.content;
		}

		public static void ConnectToVoiceChat(short worldID, string name, int mapID, int instanceID, Vector3 position) {
			Connect connect = new Connect {
				worldID = worldID,
				name = name,
				mapID = mapID,
				instanceID = instanceID,
				position = position
			};

			byte[] connectMessage = new NetworkMessage(NetworkMessageType.Connect, connect).ToBytes();
			udpClient.Send(connectMessage, connectMessage.Length);

			// Start receive info loop
			//udpClient.BeginReceive(new AsyncCallback(ReceiveVoiceData), null); // Start receive voice loop
		}

		public static void SendInfoToServer(Vector3 pos) {
			byte[] updateServerMessage = new NetworkMessage(NetworkMessageType.UpdateServer, pos).ToBytes();
			udpClient.Send(updateServerMessage, updateServerMessage.Length);
		}

		public static void SendVoiceToServer(byte[] data) {
			byte[] sendVoiceMessage = new NetworkMessage(NetworkMessageType.SendVoice, data).ToBytes();
			udpClient.SendAsync(sendVoiceMessage, sendVoiceMessage.Length);
		}

		private static void ReceiveVoiceData(IAsyncResult result) {
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, NetworkConsts.port);
			byte[] received = udpClient.EndReceive(result, ref remoteEP);

			// Handle data received

			udpClient.BeginReceive(new AsyncCallback(ReceiveVoiceData), null);
		}
	}
}
