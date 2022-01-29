using EorzeansVoice.Utils;
using System.Diagnostics;

namespace EorzeansVoice {
	public static class GameData {
		private class Offsets {
			public class Combatant {
				//public const ulong Name = 0x30; // string(64)
				public const ulong posX = 0xA0; // float
				public const ulong posZ = 0xA4; // float
				public const ulong posY = 0xA8; // float
				// public const ulong Heading = 0xB0; // float
				// public const ulong Job = 0x1E0; // byte
				// public const ulong Level = 0x1E1; // byte
				public const ulong currentWorldID = 0x19B4; // short
				// public const ulong homeWorldID = 0x19B6; // short
			}

			public const ulong combatantPtr = 0x1EA9B38; // ulong
			public const ulong nameWhenOnline = 0x1EAD239; // string(21)
			public const ulong mapID = 0x1E7F834; // int
			public const ulong instanceID = 0x1E7B0CC; // int
		}

		public static bool IsLoggedIn(Process p) {
			if (GetName(p) != string.Empty) {
				return true;
			}
			return false;
		}

		public static string GetName(Process p) {
			return MemoryReader.GetString(p, Offsets.nameWhenOnline, 21).Trim();
		}

		public static int GetMapID(Process p) {
			return MemoryReader.GetValue<int>(p, Offsets.mapID);
		}

		public static int GetInstanceID(Process p) {
			return MemoryReader.GetValue<int>(p, Offsets.instanceID);
		}

		public static Vector3 GetPosition(Process p) {
			Vector3 pos = Vector3.Zero;

			ulong combatantAddr = MemoryReader.GetValue<ulong>(p, Offsets.combatantPtr);

			pos.x = MemoryReader.GetValue<float>(p, Offsets.Combatant.posX, combatantAddr);
			pos.y = MemoryReader.GetValue<float>(p, Offsets.Combatant.posY, combatantAddr);
			pos.z = MemoryReader.GetValue<float>(p, Offsets.Combatant.posZ, combatantAddr);

			return pos;
		}

		public static short GetCurrentWorldID(Process p) {
			ulong combatantAddr = MemoryReader.GetValue<ulong>(p, Offsets.combatantPtr);
			return MemoryReader.GetValue<short>(p, Offsets.Combatant.currentWorldID, combatantAddr);
		}
	}
}
