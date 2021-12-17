using System.Diagnostics;

namespace EorzeansVoice {
	public static class GameData {
		public static Process gameProcess;

		private const ulong posXoffset = 0x1E7AB70;
		private const ulong posYoffset = 0x1E7AB74;
		private const ulong posZoffset = 0x1E7AB78;
		private const ulong nameWhenOnline = 0x1EA42B9;

		public static bool IsLoggedIn() {
			if (GetName() != string.Empty) {
				return true;
			}
			return false;
		}

		public static string GetName() {
			return MemoryReader.GetString(gameProcess, nameWhenOnline, 21).Trim();
		}

		public static Vector3 GetPosition() {
			if (gameProcess == null) {
				return Vector3.Zero;
			}

			Vector3 pos = Vector3.Zero;
			pos.x = (float)MemoryReader.GetValue<float>(gameProcess, posXoffset);
			pos.y = (float)MemoryReader.GetValue<float>(gameProcess, posYoffset);
			pos.z = (float)MemoryReader.GetValue<float>(gameProcess, posZoffset);

			return pos;
		}
	}
}
