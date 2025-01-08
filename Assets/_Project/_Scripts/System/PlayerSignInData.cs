namespace Smash.System
{
	public class PlayerSignInData
	{
		private readonly string m_id, m_accessToken;
		public string PlayerName { get; private set; }
		public string Id => m_id;
		public string AccessToken => m_accessToken;
		public PlayerSignInData(string id, string accessToken, string playerName)
		{
			m_id = id;
			m_accessToken = accessToken;
			PlayerName = playerName;
		}
		
		public void SetPlayerName(string name) => PlayerName = name;
	}
}