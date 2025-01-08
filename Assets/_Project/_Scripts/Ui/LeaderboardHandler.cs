using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smash.System;
using Entry = Smash.Ui.Components.LeaderboardEntry;
using TMPro;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

namespace Smash.Ui
{
	public class LeaderboardHandler : MonoBehaviour
	{
		[SerializeField] private GameObject m_board;
		[SerializeField] private TMP_Text m_connectionStatusText;
		[SerializeField] private TMP_Text m_yourScoreText;
		[SerializeField] private Entry[] m_entries;

		private const string k_LeaderboardId = "smash-platformer-speedrun";
		
		public void OnClickRefresh() => RefreshBoard();

		public void OnOpen()
		{
			if (PlayerAuthentication.Instance.IsSignedIn())
				RefreshBoard();
			else
			{
				m_board.SetActive(false);
				m_connectionStatusText.text = "not signed in";
				m_connectionStatusText.gameObject.SetActive(true);
				SignIn();
			}
		}

		private async void SignIn()
		{
			try
			{
				await PlayerAuthentication.Instance.TryAnonSignin();
				RefreshBoard();
			}
			catch
			{
				m_connectionStatusText.gameObject.SetActive(true);
				m_connectionStatusText.text = "sign in failed!!";
			}
		}

		private async void RefreshBoard()
		{
			m_board.SetActive(false);
			m_connectionStatusText.text = "refreshing...";
			m_connectionStatusText.gameObject.SetActive(true);
			await GetOrSetPlayerScore();
			bool setLeaderboard = await SetLeaderBoard();
			if (setLeaderboard)
			{
				m_connectionStatusText.gameObject.SetActive(false);
				m_board.SetActive(true);
			}
			else
			{
				m_connectionStatusText.gameObject.SetActive(true);
				m_connectionStatusText.text = "refresh failed!!";
			}
		}

		private async Task GetOrSetPlayerScore()
		{
			string yourScore = "--";
			try
			{
				await LeaderboardsService.Instance.AddPlayerScoreAsync(
					k_LeaderboardId, PlayerAuthentication.Instance.PlayerScore);
				LeaderboardEntry entry = await LeaderboardsService.Instance.GetPlayerScoreAsync(k_LeaderboardId);
				PlayerAuthentication.Instance.SetPlayerScore(entry.Score);
				PlayerAuthentication.Instance.SetPlayerName(entry.PlayerName);
				m_yourScoreText.text = ScoreHelper.ApproximatelyEqual(entry.Score, ScoreHelper.DefaultScore)
					? yourScore
					: ScoreHelper.ConvertScore(entry.Score);
			}
			catch
			{
				m_yourScoreText.text = "--";
			}
		}

		private async Task<bool> SetLeaderBoard()
		{
			try
			{
				var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(
					k_LeaderboardId, new GetScoresOptions { Limit = 25 });
				var entries = scoreResponse.Results;
				foreach (var entry in m_entries)
				{
					entry.gameObject.SetActive(false);
				}

				for (int i = 0; i < entries.Count; i++)
				{
					if (ScoreHelper.ApproximatelyEqual(entries[i].Score, ScoreHelper.DefaultScore)) continue;
					m_entries[i].Set(
						position: i+1, 
						playerName: entries[i].PlayerName, 
						score: entries[i].Score);
					m_entries[i].CheckMyPlayer(entries[i].PlayerId);
					m_entries[i].gameObject.SetActive(true);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}