using Newtonsoft.Json;
using System.Text;

namespace EorzeansVoiceLib.Utils {
	public static class ToFromBytes {
		public static byte[] ToBytes(this NetworkMessage message) {
			string jsonString = JsonConvert.SerializeObject(message);
			return Encoding.UTF8.GetBytes(jsonString);
		}

		public static NetworkMessage ToMessage(this byte[] bytes) {
			string jsonString = Encoding.UTF8.GetString(bytes);
			return JsonConvert.DeserializeObject<NetworkMessage>(jsonString);
		}
	}
}
