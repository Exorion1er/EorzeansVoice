namespace EorzeansVoice {
	public class Config {
		public const string FileName = "Config.json";

		public int ScaleX { get; set; } = 500;
		public int ScaleY { get; set; } = 650;
		public string Address { get; set; } = "127.0.0.1";
		public int Port { get; set; } = 22686;
		public bool Muted { get; set; } = false;
		public bool Deaf { get; set; } = false;
		public AudioInputProcessing.Mode VoiceMode { get; set; } = AudioInputProcessing.Mode.VoiceActivation;
		public float VoiceActivationThreshold { get; set; } = 0.7f;
	}
}
