using EorzeansVoice.Utils;
using System.Diagnostics;

namespace EorzeansVoice {
	public static class GameData {
		private const ulong posX = 0x1E7FAF0;
		private const ulong posY = 0x1E7FAF4;
		private const ulong posZ = 0x1E7FAF8;
		private const ulong nameWhenOnline = 0x1EA9239;
		private const ulong mapID = 0x1E7B834;
		private const ulong instance = 0x1E770CC;

		public static bool IsLoggedIn(Process p) {
			if (GetName(p) != string.Empty) {
				return true;
			}
			return false;
		}

		public static string GetName(Process p) {
			return MemoryReader.GetString(p, nameWhenOnline, 21).Trim();
		}

		public static int GetMapID(Process p) {
			return (int)MemoryReader.GetValue<int>(p, mapID);
		}

		public static int GetInstance(Process p) {
			return (int)MemoryReader.GetValue<int>(p, instance);
		}

		public static Vector3 GetPosition(Process p) {
			Vector3 pos = Vector3.Zero;
			pos.x = (float)MemoryReader.GetValue<float>(p, posX);
			pos.y = (float)MemoryReader.GetValue<float>(p, posY);
			pos.z = (float)MemoryReader.GetValue<float>(p, posZ);

			return pos;
		}
	}
}
