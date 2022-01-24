using EorzeansVoice.Utils;
using EorzeansVoiceLib;
using EorzeansVoiceLib.Enums;
using EorzeansVoiceLib.NetworkMessageContent;
using EorzeansVoiceLib.Utils;
using System;
using System.Collections.Generic;
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
			} catch (Exception e) {
				Logging.Error("An error occurred in Network.IsNetworkWorking : " + e.Message);
				Logging.Error(e.StackTrace);
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
			} catch (Exception e) {
				Logging.Error("An error occurred in Network.ConnectToServer : " + e.Message);
				Logging.Error(e.StackTrace);
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
				Logging.Error("An error occurred in Network.PingServer : " + e.Message);
				Logging.Error(e.StackTrace);
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
				Logging.Error("An error occurred in Network.IsUpToDate : " + e.Message);
				Logging.Error(e.StackTrace);
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
			} catch (Exception e) {
				Logging.Error("An error occurred in Network.ConnectToVoiceChat : " + e.Message);
				Logging.Error(e.StackTrace);
				return 0;
			}
		}

		public static void SendInfoToServer(UpdateServer info) {
			byte[] updateServerMessage = new NetworkMessage(NetworkMessageType.UpdateServer, info).ToBytes();
			udpClient.SendAsync(updateServerMessage, updateServerMessage.Length);
		}

		public static void SendKeepAlive(int userID) {
			byte[] data = new NetworkMessage(NetworkMessageType.KeepAlive, userID).ToBytes();
			udpClient.SendAsync(data, data.Length);
		}

		public static void StartReceivingData() {
			udpClient.BeginReceive(new AsyncCallback(ReceiveData), null);
		}

		public static void SendVoiceToServer(byte[] data) {
			SendVoice content = new SendVoice {
				id = LogicController.userID,
				data = data
			};

			byte[] sendVoiceMessage = new NetworkMessage(NetworkMessageType.SendVoiceToServer, content).ToBytes();
			udpClient.SendAsync(sendVoiceMessage, sendVoiceMessage.Length);
		}

		public static void Disconnect(int userID) {
			byte[] data = new NetworkMessage(NetworkMessageType.Disconnect, userID).ToBytes();
			udpClient.Send(data, data.Length);
		}

		private static void ReceiveData(IAsyncResult result) {
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, NetworkConsts.port);
			NetworkMessage msg = null;
			byte[] received = null;

			try {
				received = udpClient.EndReceive(result, ref remoteEP);
			} catch (Exception e) {
				MessageBox.Show("An error occurred while receiving data from the server. Please restart EorzeansVoice.");

				Logging.Error("An error occurred in Network.ReceiveData : " + e.Message);
				Logging.Error(e.StackTrace);

				Application.Exit();
				return;
			}

			udpClient.BeginReceive(new AsyncCallback(ReceiveData), null);

			msg = received.ToMessage();
			switch (msg.type) {
				case NetworkMessageType.UpdateClient:
					LogicController.UpdateAround(msg.content.ToObject<List<ClientInfo>>());
					break;
				case NetworkMessageType.SendVoiceToClient:
					AudioController.ProcessAudioData(msg.content.ToObject<SendVoice>());
					break;
				case NetworkMessageType.ForceDisconnect:
					ForceDisconnect((string)msg.content);
					break;
			}
		}

		public static void ForceDisconnect(string error) {
			if (LogicController.userID != 0) {
				Disconnect(LogicController.userID);
			}

			MessageBox.Show("An error occurred that made the server disconnect you. Please restart EorzeansVoice. Error : " + error);
			Logging.Warn("Received ForceDisconnect from server, closing.");
			Application.Exit();
		}
	}
}
