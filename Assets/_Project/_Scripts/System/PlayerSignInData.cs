namespace Smash.System
{
	public class PlayerSignInData
	{
		private readonly string m_id, m_accessToken;
		public string PlayerName { get; private set; }
		public double PlayerScore { get; private set; }
		public string Id => m_id;
		public string AccessToken => m_accessToken;
		public PlayerSignInData(string id, string accessToken, string playerName, double score)
		{
			m_id = id;
			m_accessToken = accessToken;
			PlayerName = playerName;
			PlayerScore = score;
		}
		
		public void SetPlayerName(string name) => PlayerName = name;

		public void SetPlayerScore(double score)
		{
			if (score > PlayerScore) return;
			PlayerScore = score;
		}
	}
}