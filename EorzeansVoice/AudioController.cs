using EorzeansVoice.Utils;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using POpusCodec;
using POpusCodec.Enums;
using System.Windows.Forms;

namespace EorzeansVoice {
	public static class AudioController {
		public class Device {
			public int id;
			public string name;

			public Device(int id, string name) {
				this.id = id;
				this.name = name;
			}

			public override string ToString() {
				return name;
			}
		}

		private static WaveIn input;
		private static BufferedWaveProvider waveProvider;
		private static WaveOut output;
		private static OpusEncoder encoder;
		private static OpusDecoder decoder;

		public static void LoadAudioDevices(ComboBox inputs, ComboBox outputs) {
			MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
			for (int i = 0; i < WaveIn.DeviceCount; i++) {
				MMDevice device = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)[i];
				Device newDevice = new Device(i, device.DeviceFriendlyName);
				inputs.Items.Add(newDevice);

				if (device.ID == enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications).ID) {
					inputs.SelectedItem = newDevice;
				}
			}

			for (int i = 0; i < WaveOut.DeviceCount; i++) {
				MMDevice device = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[i];
				Device newDevice = new Device(i, device.DeviceFriendlyName);
				outputs.Items.Add(newDevice);

				if (device.ID == enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Communications).ID) {
					outputs.SelectedItem = newDevice;
				}
			}
		}

		public static void Init(Device inputDevice, Device outputDevice) {
			input = new WaveIn {
				DeviceNumber = inputDevice.id,
				BufferMilliseconds = 20
			};
			input.WaveFormat = new WaveFormat((int)SamplingRate.Sampling48000, (int)Channels.Mono);
			input.RecordingStopped += RecordingStopped;
			input.DataAvailable += DataAvailable;

			waveProvider = new BufferedWaveProvider(input.WaveFormat);

			output = new WaveOut {
				DeviceNumber = outputDevice.id,
				DesiredLatency = 100
			};
			output.Init(waveProvider);
			output.PlaybackStopped += PlaybackStopped;

			encoder = new OpusEncoder(SamplingRate.Sampling48000, Channels.Mono, OpusApplicationType.Voip, Delay.Delay20ms) {
				Bitrate = 64 * 1024
			};

			decoder = new OpusDecoder(SamplingRate.Sampling48000, Channels.Mono);
		}

		public static bool StartAudio() {
			if (input == null || output == null || waveProvider == null || encoder == null || decoder == null) {
				return false;
			}

			input.StartRecording();
			output.Play();
			return true;
		}

		public static void StopAudio() {
			if (output != null) {
				output.Stop();
			}

			if (input != null) {
				input.StopRecording();
			}
		}

		private static void DataAvailable(object sender, WaveInEventArgs e) {
			byte[] encoded = encoder.Encode(e.Buffer.ToShorts());
			Network.SendVoiceToServer(encoded);
		}

		private static void RecordingStopped(object sender, StoppedEventArgs e) {
			StopAudio();
		}

		private static void PlaybackStopped(object sender, StoppedEventArgs e) {
			StopAudio();
		}
	}
}
