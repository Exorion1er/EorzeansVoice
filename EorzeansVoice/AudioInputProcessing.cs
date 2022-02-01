using EorzeansVoice.Utils;
using POpusCodec;
using POpusCodec.Enums;
using System;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace EorzeansVoice {
	public static class AudioInputProcessing {
		public enum Mode {
			VoiceActivation,
			PushToTalk
		}

		public static Mode mode;
		public static float voiceActivationThreshold;
		public static HotkeyController.KeyAction pttKey;
		public static bool muted = false;

		private static readonly Timer TIM_KeepOn = new Timer();
		private static OpusEncoder encoder;
		private static bool pttDown = false;

		public static void Init(float threshold) {
			voiceActivationThreshold = threshold;

			encoder = new OpusEncoder(SamplingRate.Sampling48000, Channels.Stereo, OpusApplicationType.Voip, Delay.Delay20ms) {
				Bitrate = 64 * 1024
			};

			TIM_KeepOn.Interval = 300;
			TIM_KeepOn.Elapsed += TIM_KeepOn_Elapsed;
		}

		public static void ChangePTTKey(Keys key, Keys modifiers) {
			HotkeyController.KeyAction ka = new HotkeyController.KeyAction() {
				key = key,
				upDown = HotkeyController.KeyUpDown.KeyDown | HotkeyController.KeyUpDown.KeyUp,
				control = modifiers.HasFlag(Keys.Control),
				shift = modifiers.HasFlag(Keys.Shift),
				alt = modifiers.HasFlag(Keys.Alt),
				callbackDown = PTTDown,
				callbackUp = PTTUp
			};
			HotkeyController.hookedKeys.Remove(pttKey);
			HotkeyController.hookedKeys.Add(ka);
			pttKey = ka;
		}

		private static void PTTDown() {
			pttDown = true;
			TIM_KeepOn.Stop();
		}

		private static void PTTUp() {
			pttDown = false;
			TIM_KeepOn.Start();
		}

		public static void ProcessAudioInput(byte[] data) {
			float volume = ((float)MeasureDB(data)).Normalize(-100, 0, 0, 1);
			Main.instance.UpdateVoiceActivationSLDActiveValue(volume);

			if (muted) {
				return;
			}

			bool shouldSend = false;

			switch (mode) {
				case Mode.VoiceActivation:
					shouldSend = VoiceActivation(volume);
					break;
				case Mode.PushToTalk:
					shouldSend = PushToTalk();
					break;
			}

			if (shouldSend) {
				EncodeSend(data);
			}
		}

		private static bool VoiceActivation(float volume) {
			bool aboveThreshold = volume >= voiceActivationThreshold;
			if (TIM_KeepOn.Enabled || aboveThreshold) {
				if (aboveThreshold) {
					TIM_KeepOn.Stop();
					TIM_KeepOn.Start();
				}

				return true;
			}
			return false;
		}

		private static bool PushToTalk() {
			if (TIM_KeepOn.Enabled || pttDown) {
				if (pttDown) {
					TIM_KeepOn.Stop();
					TIM_KeepOn.Start();
				}

				return true;
			}
			return false;
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
