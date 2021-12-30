namespace EorzeansVoiceLib.Enums {
	// If you want to add something to this enum
	// add it after every other element to avoid compatibility issues
	// and for the Version Check to keep working.

	public enum NetworkMessageType : int {
		// TO SERVER
		Ping = 0, // string "Ping"
		VersionCheck = 1, // VersionCheck object
		Connect = 2, // Connect object
		UpdateServer = 3, // UpdateServer object
		SendVoiceToServer = 4, // SendVoice object
		KeepAlive = 5, // int userID
		Disconnect = 6, // int userID

		// TO CLIENT
		Pong = 7, // string "Pong"
		VersionCheckResult = 8, // VersionCheckAnswer enum
		Connected = 9, // int userID
		UpdateClient = 10, // List<ClientInfo> object
		SendVoiceToClient = 11, // SendVoice object
		ForceDisconnect = 12 // string Error
	}
}
