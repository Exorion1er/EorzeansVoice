using nucs.JsonSettings;

namespace EorzeansVoice {
	public class Config : JsonSettings {
		public override string FileName { get; set; } = "Config.json";

		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public float ScaleX { get; set; } = 500f;
		public float ScaleY { get; set; } = 650f;
		public string Address { get; set; } = "127.0.0.1";
		public int Port { get; set; } = 22686;
		public bool Muted { get; set; } = false;
		public bool Deaf { get; set; } = false;
		public AudioInputProcessing.Mode VoiceMode { get; set; } = AudioInputProcessing.Mode.VoiceActivation;
		public float VoiceActivationThreshold { get; set; } = 0.7f;

		public Config() { }
		public Config(string fileName) : base(fileName) { }
	}
}
