using System.Diagnostics;

namespace EorzeansVoice {
	public static class GameData {
		public static Process gameProcess;

		private const ulong posX = 0x1E7AB70;
		private const ulong posY = 0x1E7AB74;
		private const ulong posZ = 0x1E7AB78;
		private const ulong nameWhenOnline = 0x1EA42B9;
		private const ulong mapID = 0x1E768B4;
		private const ulong instance = 0x1E7214C;

		public static bool IsLoggedIn() {
			if (GetName() != string.Empty) {
				return true;
			}
			return false;
		}

		public static string GetName() {
			return MemoryReader.GetString(gameProcess, nameWhenOnline, 21).Trim();
		}

		public static int GetMapID() {
			if (gameProcess == null) {
				return 0;
			}

			return (int)MemoryReader.GetValue<int>(gameProcess, mapID);
		}

		public static int GetInstance() {
			if (gameProcess == null) {
				return 0;
			}

			return (int)MemoryReader.GetValue<int>(gameProcess, instance);
		}

		public static Vector3 GetPosition() {
			if (gameProcess == null) {
				return Vector3.Zero;
			}

			Vector3 pos = Vector3.Zero;
			pos.x = (float)MemoryReader.GetValue<float>(gameProcess, posX);
			pos.y = (float)MemoryReader.GetValue<float>(gameProcess, posY);
			pos.z = (float)MemoryReader.GetValue<float>(gameProcess, posZ);

			return pos;
		}
	}
}
