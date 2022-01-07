using EorzeansVoice.Utils;
using POpusCodec;
using POpusCodec.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EorzeansVoice {
	public static class AudioInputProcessing {
		public enum Mode {
			VoiceActivation,
			PushToTalk
		}

		public static Mode mode;
		public static float voiceActivationThreshold;
		public static OpusEncoder encoder;

		public static void Init(float threshold) {
			voiceActivationThreshold = threshold;

			encoder = new OpusEncoder(SamplingRate.Sampling48000, Channels.Stereo, OpusApplicationType.Voip, Delay.Delay20ms) {
				Bitrate = 64 * 1024
			};
		}

		public static void ProcessAudioInput(byte[] data) {
			switch (mode) {
				case Mode.VoiceActivation:
					VoiceActivation(data);
					break;
				case Mode.PushToTalk:
					break;
			}
		}

		private static void VoiceActivation(byte[] data) {
			double decibels = NormalizeDB(MeasureDB(data));

			if (decibels >= voiceActivationThreshold) {
				EncodeSend(data);
			}
		}

		private static double MeasureDB(byte[] data) {
			double sum = 0;
			for (var i = 0; i < data.Length; i += 2) {
				double sample = BitConverter.ToInt16(data, i) / 32768.0;
				sum += sample * sample;
			}
			double rms = Math.Sqrt(sum / (data.Length / 2));
			return 20 * Math.Log10(rms);
		}

		private static float NormalizeDB(double value) {
			return (float)(((value - -100) / (0 - -100) * (1 - 0)) + 0);
		}

		private static void EncodeSend(byte[] data) {
			byte[] encoded = encoder.Encode(data.ToShorts());
			Network.SendVoiceToServer(encoded);
		}
	}
}
