namespace EorzeansVoice.Utils {
	public static class ShortsBytesExt {
		public static short[] ToShorts(this byte[] input) {
			short[] processedValues = new short[input.Length / 2];
			for (int c = 0; c < processedValues.Length; c++) {
				processedValues[c] = (short)(input[c * 2] << 0);
				processedValues[c] += (short)(input[(c * 2) + 1] << 8);
			}
			return processedValues;
		}

		public static byte[] ToBytes(this short[] input) {
			byte[] processedValues = new byte[input.Length * 2];
			for (int c = 0; c < input.Length; c++) {
				processedValues[c * 2] = (byte)(input[c] & 0xFF);
				processedValues[c * 2 + 1] = (byte)((input[c] >> 8) & 0xFF);
			}
			return processedValues;
		}
	}
}
