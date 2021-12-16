using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EorzeansVoice {
	public static class GameData {
		private const string gameProcessName = "ffxiv_dx11";
		private const ulong posXoffset = 0x1E7AB70;
		private const ulong posYoffset = 0x1E7AB74;
		private const ulong posZoffset = 0x1E7AB78;

		private static Process gameProcess;

		public static bool FindProcess() {
			Process p = Process.GetProcessesByName(gameProcessName).FirstOrDefault();
			if (p == null) {
				MessageBox.Show("Couldn't find FFXIV's process.");
				return false;
			}
			gameProcess = p;
			return true;
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
