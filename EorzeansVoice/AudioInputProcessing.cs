using EorzeansVoice.Utils;
using POpusCodec;
using POpusCodec.Enums;
using System;
using System.Timers;

namespace EorzeansVoice {
	public static class AudioInputProcessing {
		public enum Mode {
			VoiceActivation,
			PushToTalk
		}

		public static Mode mode;
		public static float voiceActivationThreshold;
		public static bool muted = false;

		private static readonly Timer TIM_KeepOn = new Timer();
		private static OpusEncoder encoder;

		public static void Init(float threshold) {
			voiceActivationThreshold = threshold;

			encoder = new OpusEncoder(SamplingRate.Sampling48000, Channels.Stereo, OpusApplicationType.Voip, Delay.Delay20ms) {
				Bitrate = 64 * 1024
			};

			TIM_KeepOn.Interval = 300;
			TIM_KeepOn.Elapsed += TIM_KeepOn_Elapsed;
		}

		public static void ProcessAudioInput(byte[] data) {
			switch (mode) {
				case Mode.VoiceActivation:
					VoiceActivation(data);
					break;
				case Mode.PushToTalk:
					// TODO
					break;
			}
		}

		private static void VoiceActivation(byte[] data) {
			float volume = ((float)MeasureDB(data)).Normalize(-100, 0, 0, 1);
			Main.instance.UpdateVoiceActivationSlider(volume);

			if (muted) {
				return;
			}

			bool aboveThreshold = volume >= voiceActivationThreshold;
			if (TIM_KeepOn.Enabled || aboveThreshold) {
				EncodeSend(data);

				if (aboveThreshold) {
					TIM_KeepOn.Stop();
					TIM_KeepOn.Start();
				}
			}
		}

		private static void TIM_KeepOn_Elapsed(object sender, ElapsedEventArgs e) {
			TIM_KeepOn.Stop();
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

		private static void EncodeSend(byte[] data) {
			byte[] encoded = encoder.Encode(data.ToShorts());
			Network.SendVoiceToServer(encoded);
		}
	}
}
