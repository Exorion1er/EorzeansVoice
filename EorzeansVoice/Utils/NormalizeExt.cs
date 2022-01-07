using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EorzeansVoice.Utils {
	public static class NormalizeExt {
		public static float Normalize(this float val, float valMin, float valMax, float min, float max) {
			return ((val - valMin) / (valMax - valMin) * (max - min)) + min;
		}
	}
}
