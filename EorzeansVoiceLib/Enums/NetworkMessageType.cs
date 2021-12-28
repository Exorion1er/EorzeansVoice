namespace EorzeansVoiceLib.Enums {
	// If you want to add something to this enum
	// add it after every other element to avoid compatibility issues
	// and for the Version Check to keep working.
	public enum NetworkMessageType : int {
		Ping = 0, // string
		VersionCheck = 1, // VersionCheck class
		Connect = 2, // Connect class
		Connected = 3, // int
		UpdateServer = 4, // ClientInfo class // Client sending information to server (position)
		UpdateClient = 5, // TBD // Server sending information back to client (other clients around them, positions, names)
		Teleport = 6, // TBD
		SendVoice = 7, // TBD
		Disconnect = 8 // TBD
	}
}
