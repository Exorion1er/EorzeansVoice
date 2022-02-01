using EorzeansVoiceLib;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EorzeansVoice {
	public class Config {
		[JsonIgnore]
		public const string FileName = "Config.json";

		public int Version { get; set; } = 1;
		public int SizeX { get; set; } = 500;
		public int SizeY { get; set; } = 650;
		public string Address { get; set; } = "127.0.0.1";
		public int Port { get; set; } = 22686;
		public float GlobalVolume { get; set; } = 1.0f;
		public bool Muted { get; set; } = false;
		public bool Deaf { get; set; } = false;
		public AudioInputProcessing.Mode VoiceMode { get; set; } = AudioInputProcessing.Mode.VoiceActivation;
		public float VoiceActivationThreshold { get; set; } = 0.7f;
		public HotkeyController.KeyAction PushToTalkKey { get; set; }

		public static Config Load() {
			if (!File.Exists(FileName)) {
				Logging.Warn("Config file doesn't exist.");
				return CreateDefaultConfig();
			}

			string content = File.ReadAllText(FileName);

			int v = FindVersion(content);
			if (v != 0) {
				Config def = new Config();
				if (v > def.Version) {
					Logging.Error("Config file's version is above default version.");
					MessageBox.Show("An error occurred with your Configuration file. Switching to default Configuraiton.");
					CorruptedConfig();
					return CreateDefaultConfig();
				} else if (v < def.Version) {
					string upgraded = ApplyUpgrades(v, def.Version, content);

					if (File.Exists("Config.V" + v + ".json")) {
						File.Delete("Config.V" + v + ".json");
					}
					File.Move(FileName, "Config.V" + v + ".json");

					Config config = ParseConfig(upgraded);
					config.Version = def.Version;
					config.Save();
					return config;
				}

				return ParseConfig(content);
			}
			return CreateDefaultConfig();
		}

		private static Config CreateDefaultConfig() {
			Logging.Info("Creating new config file.");

			Config config = new Config();
			config.Save();
			return config;
		}

		private static int FindVersion(string text) {
			Regex versionRGX = new Regex("\"Version\": ([0-9]+),");
			Match match = versionRGX.Match(text);
			if (match.Success) {
				return int.Parse(match.Groups[1].Value);
			}
			Logging.Error("Could not find Version in config file.");
			return 0;
		}

		private static string ApplyUpgrades(int inputVersion, int targetVersion, string input) {
			if (inputVersion == targetVersion) {
				return input;
			}

			try {
				switch (inputVersion) {
					case 1: // V1 to V2
						// Do stuff here
						break;
				}
			} catch (Exception e) {
				Logging.Error("Could not apply upgrade to config input : " + input);
				Logging.Error("Exception : " + e.Message);
				Logging.Error(e.StackTrace);
				MessageBox.Show("An error occurred while upgrading your Configuration file. Please restart Eorzeans' Voice.");

				CorruptedConfig();
				CreateDefaultConfig();
				Application.Exit();
				return input;
			}

			inputVersion++;
			return ApplyUpgrades(inputVersion, targetVersion, input);
		}

		private static Config ParseConfig(string input) {
			try {
				Config config = JsonConvert.DeserializeObject<Config>(input);
				return config;
			} catch (Exception e) {
				Logging.Error("Could not deserialize Config input : " + input);
				Logging.Error("Exception : " + e.Message);
				Logging.Error(e.StackTrace);
				MessageBox.Show("An error occurred while parsing your Configuration file, switching to default Configuration.");

				CorruptedConfig();
				return CreateDefaultConfig();
			}
		}

		private static void CorruptedConfig() {
			int rdm = new Random().Next(1, 999999);
			File.Move(FileName, "Config.CORRUPTED_" + rdm + ".json");
		}

		public void Save() {
			string json = JsonConvert.SerializeObject(this, Formatting.Indented);
			File.WriteAllText(FileName, json);
		}
	}
}
