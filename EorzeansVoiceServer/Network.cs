using EorzeansVoiceLib;
using EorzeansVoiceLib.Utils;
using System;
using System.Net;
using System.Net.Sockets;

namespace EorzeansVoiceServer {
	public static class Network {
		private static UdpClient udpClient;
		private static int port;

		public static void Start(int p) {
			port = p;
			udpClient = new UdpClient(port);

			if (Environment.OSVersion.Platform == PlatformID.Win32NT) { // Only necessary on Windows
				udpClient.Client.IOControl((IOControlCode)(-1744830452), new byte[] { 0, 0, 0, 0 }, null);
			}

			udpClient.BeginReceive(new AsyncCallback(ReceiveData), null);
		}

		private static void ReceiveData(IAsyncResult result) {
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);
			NetworkMessage msg = null;

			try {
				byte[] received = udpClient.EndReceive(result, ref remoteEP);
				msg = received.ToMessage();
			} catch (Exception e) {
				Logging.Error("Error receiving message : " + e.Message);
				Logging.Error(e.StackTrace);
				return;
			} finally {
				udpClient.BeginReceive(new AsyncCallback(ReceiveData), null);
			}

			Program.Process(remoteEP, msg);
		}

		public static void SendMessage(NetworkMessage message, IPEndPoint remoteEP) {
			byte[] data = message.ToBytes();
			// Check if remoteEP is valid first
			// Add try catch
			udpClient.SendAsync(data, data.Length, remoteEP);
		}

		public static void SendMessage(NetworkMessage message, Client client) {
			IPEndPoint remoteEP = new IPEndPoint(client.ipAddress, client.port);
			SendMessage(message, remoteEP);
		}
	}
}
