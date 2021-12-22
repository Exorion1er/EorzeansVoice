namespace EorzeansVoiceLib.Enums {
	// If you want to add something to this enum
	// add it after every other element to avoid compatibility issues
	// and for the Version Check to keep working.
	public enum NetworkMessageType : int {
		Ping = 0,
		VersionCheck = 1,
		Connect = 2,
		UpdateServer = 3, // Client sending information to server (position)
		UpdateClient = 4, // Server sending information back to client (other clients around them, positions, names)
		Teleport = 5,
		SendVoice = 6,
		Disconnect = 7
	}
}
