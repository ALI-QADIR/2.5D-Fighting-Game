using System.Collections.Generic;
using System.Threading.Tasks;
using TripleA.Singletons;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;

namespace Smash.Services
{
	public class LeaderboardServicesHandler : PersistentSingleton<LeaderboardServicesHandler>
	{
		public List<LeaderboardEntry> Entries { get; private set; }
		
		private LeaderboardScoresPage m_scorePage;
		private const string k_LeaderboardId = "smash-platformer-speedrun";

		protected override void Awake()
		{
			base.Awake();
			Entries = new List<LeaderboardEntry>();
		}

		public async Task<bool> TryGetOrSetPlayerDetails()
		{
			try
			{
				LeaderboardEntry entry = await LeaderboardsService.Instance.AddPlayerScoreAsync(
					k_LeaderboardId, PlayerAuthentication.Instance.PlayerScore);
				PlayerAuthentication.Instance.SetPlayerScore(entry.Score);
				PlayerAuthentication.Instance.SetPlayerName(entry.PlayerName);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> TryGetLeaderBoard(int limit)
		{
			try
			{
				m_scorePage = await LeaderboardsService.Instance.GetScoresAsync(
					k_LeaderboardId, new GetScoresOptions { Limit = limit });
				Entries.Clear();
				Entries.AddRange(m_scorePage.Results);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
