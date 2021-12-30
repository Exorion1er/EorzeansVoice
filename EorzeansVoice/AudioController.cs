using EorzeansVoice.Utils;
using EorzeansVoiceLib.NetworkMessageContent;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using POpusCodec;
using POpusCodec.Enums;
using System;
using System.Collections.Generic;
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

		private static readonly MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
		private static WaveIn input;
		private static MixingWaveProvider32 mixer;
		private static WaveOut output;
		private static OpusEncoder encoder;
		private static OpusDecoder decoder;

		public static void LoadAudioDevices(ComboBox inputs, ComboBox outputs) {
			List<MMDevice> allInputs = GetMMDevices(DataFlow.Capture);
			MMDevice defaultInput = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
			for (int i = 0; i < WaveIn.DeviceCount; i++) {
				WaveInCapabilities capabilities = WaveIn.GetCapabilities(i);
				MMDevice device = allInputs.Find(x => x.FriendlyName.StartsWith(capabilities.ProductName));
				Device newDevice = new Device(i, device.FriendlyName);
				inputs.Items.Add(newDevice);

				if (device.ID == defaultInput.ID) {
					inputs.SelectedItem = newDevice;
				}
			}

			List<MMDevice> allOutputs = GetMMDevices(DataFlow.Render);
			MMDevice defaultOutput = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Communications);
			for (int i = 0; i < WaveOut.DeviceCount; i++) {
				WaveOutCapabilities capabilities = WaveOut.GetCapabilities(i);
				MMDevice device = allOutputs.Find(x => x.FriendlyName.StartsWith(capabilities.ProductName));
				Device newDevice = new Device(i, device.FriendlyName);
				outputs.Items.Add(newDevice);

				if (device.ID == defaultOutput.ID) {
					outputs.SelectedItem = newDevice;
				}
			}
		}

		public static List<MMDevice> GetMMDevices(DataFlow type) {
			List<MMDevice> final = new List<MMDevice>();

			foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(type, DeviceState.Active)) {
				final.Add(device);
			}

			return final;
		}

		public static void Init(Device inputDevice, Device outputDevice) {
			input = new WaveIn {
				DeviceNumber = inputDevice.id,
				BufferMilliseconds = 20
			};
			input.WaveFormat = new WaveFormat((int)SamplingRate.Sampling48000, (int)Channels.Stereo);
			input.RecordingStopped += RecordingStopped;
			input.DataAvailable += DataAvailable;

			output = new WaveOut {
				DeviceNumber = outputDevice.id,
				DesiredLatency = 100
			};
			output.PlaybackStopped += PlaybackStopped;

			// Hacky way of setting the WaveOut sampling rate to 48000 before adding any channel to the mixer
			mixer = new MixingWaveProvider32();
			BufferedWaveProvider p = new BufferedWaveProvider(input.WaveFormat);
			Wave16ToFloatProvider p32 = new Wave16ToFloatProvider(p);
			mixer.AddInputStream(p32);
			output.Init(mixer);
			mixer.RemoveInputStream(p32);

			encoder = new OpusEncoder(SamplingRate.Sampling48000, Channels.Stereo, OpusApplicationType.Voip, Delay.Delay20ms) {
				Bitrate = 64 * 1024
			};

			decoder = new OpusDecoder(SamplingRate.Sampling48000, Channels.Stereo);
		}

		public static bool StartAudio() {
			if (input == null || output == null || mixer == null || encoder == null || decoder == null) {
				return false;
			}

			input.StartRecording();
			output.Play();
			return true;
		}

		public static void ProcessAudioData(SendVoice content) {
			byte[] encoded = content.data;
			byte[] final = decoder.DecodePacket(encoded).ToBytes();

			foreach (Main.ClientAround c in Main.instance.around) {
				if (content.id == c.id) {
					c.waveProvider.AddSamples(final, 0, final.Length);
					break;
				}
			}
		}

		public static Tuple<BufferedWaveProvider, WaveChannel32> AddNewProvider() {
			BufferedWaveProvider waveProvider = new BufferedWaveProvider(input.WaveFormat);
			Wave16ToFloatProvider waveProvider32 = new Wave16ToFloatProvider(waveProvider);
			WaveStream stream = new WaveProviderToWaveStream(waveProvider32);
			WaveChannel32 channel = new WaveChannel32(stream);

			mixer.AddInputStream(channel);

			output.Stop();
			output.Play();

			return new Tuple<BufferedWaveProvider, WaveChannel32>(waveProvider, channel);
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
			MessageBox.Show("RecordingStopped");
			StopAudio();

			// TODO : Restart recording instead of stopping all
		}

		private static void PlaybackStopped(object sender, StoppedEventArgs e) {
			MessageBox.Show("PlaybackStopped");
			StopAudio();

			// TODO : Restart playback instead of stopping all
		}
	}
}
