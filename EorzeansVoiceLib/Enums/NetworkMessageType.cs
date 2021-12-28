namespace EorzeansVoiceLib.Enums {
	// If you want to add something to this enum
	// add it after every other element to avoid compatibility issues
	// and for the Version Check to keep working.

	public enum NetworkMessageType : int {
		// TO SERVER
		Ping = 0, // string "Ping"
		VersionCheck = 1, // VersionCheck object
		Connect = 2, // Connect object
		UpdateServer = 3, // TBD object (currently just pos)
		SendVoiceToServer = 4, // TBD object
		Disconnect = 5, // TBD object

		// TO CLIENT
		Pong = 6, // string "Pong"
		VersionCheckResult = 7, // VersionCheckAnswer enum
		Connected = 8, // int ID
		UpdateClient = 9, // List<ClientInfo> object
		SendVoiceToClient = 10 // TBD object
	}
}
