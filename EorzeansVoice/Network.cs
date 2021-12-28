using EorzeansVoice.Utils;
using EorzeansVoiceLib;
using EorzeansVoiceLib.Enums;
using EorzeansVoiceLib.NetworkMessageContent;
using EorzeansVoiceLib.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

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
				return (string)msg.content == "Pong";
			} catch (Exception e) {
				MessageBox.Show("An error occurred in Network.PingServer : " + e.Message);
				return false;
			}
		}

		public static VersionCheckAnswer IsUpToDate() {
			try {
				VersionCheck vCheck = new VersionCheck(NetworkConsts.clientVersion, NetworkConsts.serverVersion);
				byte[] versionCheckMessage = new NetworkMessage(NetworkMessageType.VersionCheck, vCheck).ToBytes();
				udpClient.Send(versionCheckMessage, versionCheckMessage.Length);

				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(NetworkConsts.serverAddr), NetworkConsts.port);
				byte[] received = udpClient.Receive(ref remoteEP);

				NetworkMessage msg = received.ToMessage();
				return (VersionCheckAnswer)msg.content;
			} catch (Exception e) {
				MessageBox.Show("An error occurred in Network.IsUpToDate : " + e.Message);
				return VersionCheckAnswer.ClientOutOfDate; // This makes the app close
			}
		}

		public static int ConnectToVoiceChat(short worldID, string name, int mapID, int instanceID, Vector3 position) {
			try {
				Connect connect = new Connect {
					worldID = worldID,
					name = name,
					mapID = mapID,
					instanceID = instanceID,
					position = position
				};

				byte[] connectMessage = new NetworkMessage(NetworkMessageType.Connect, connect).ToBytes();
				udpClient.Send(connectMessage, connectMessage.Length);

				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(NetworkConsts.serverAddr), NetworkConsts.port);
				byte[] received = udpClient.Receive(ref remoteEP);

				NetworkMessage msg = received.ToMessage();
				return (int)msg.content;

				// Start receive info loop
				//udpClient.BeginReceive(new AsyncCallback(ReceiveVoiceData), null); // Start receive voice loop
			} catch (Exception e) {
				MessageBox.Show("An error occurred in Network.ConnectToVoiceChat : " + e.Message);
				return 0;
			}
		}

		public static void SendInfoToServer(int id, Vector3 pos, short worldID, int mapID, int instanceID) {
			UpdateServer newInfo = new UpdateServer {
				id = id,
				position = pos,
				worldID = worldID,
				mapID = mapID,
				instanceID = instanceID
			};

			byte[] updateServerMessage = new NetworkMessage(NetworkMessageType.UpdateServer, newInfo).ToBytes();
			udpClient.SendAsync(updateServerMessage, updateServerMessage.Length);
		}

		public static void SendVoiceToServer(byte[] data) {
			byte[] sendVoiceMessage = new NetworkMessage(NetworkMessageType.SendVoiceToServer, data).ToBytes();
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
