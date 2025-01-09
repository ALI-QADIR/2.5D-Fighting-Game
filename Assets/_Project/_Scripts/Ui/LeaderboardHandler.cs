using System.Threading.Tasks;
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
				SignIn().GetAwaiter();
			}
		}

		private async Task SignIn()
		{
			bool isSignInSuccessful = await PlayerAuthentication.Instance.TryAnonSignin();
			if (isSignInSuccessful)
			{
				RefreshBoard();
			}
			else
			{
				m_connectionStatusText.gameObject.SetActive(true);
				m_connectionStatusText.text = "sign in failed!!";
			}
		}

		private async void RefreshBoard()
		{
			if (!PlayerAuthentication.Instance.IsSignedIn())
			{
				await SignIn();
			}
			m_board.SetActive(false);
			m_connectionStatusText.text = "refreshing...";
			m_connectionStatusText.gameObject.SetActive(true);
			bool isPlayerScoreSet = await TryGetOrSetPlayerScore();
			if (!isPlayerScoreSet)
			{
				SetLeaderboard(false);
				return; 
			} 
			bool setLeaderboard = await TrySetLeaderBoard();
			SetLeaderboard(setLeaderboard);
		}

		private void SetLeaderboard(bool setLeaderboard)
		{
			m_connectionStatusText.gameObject.SetActive(!setLeaderboard);
			m_connectionStatusText.text = "refresh failed!!";
			m_board.SetActive(setLeaderboard);
		}

		private async Task<bool> TryGetOrSetPlayerScore()
		{
			string yourScore = "--";
			try
			{
				LeaderboardEntry entry = await LeaderboardsService.Instance.AddPlayerScoreAsync(
					k_LeaderboardId, PlayerAuthentication.Instance.PlayerScore);
				PlayerAuthentication.Instance.SetPlayerScore(entry.Score);
				PlayerAuthentication.Instance.SetPlayerName(entry.PlayerName);
				m_yourScoreText.text = ScoreHelper.ApproximatelyEqual(entry.Score, ScoreHelper.DefaultScore)
					? yourScore
					: ScoreHelper.ConvertScore(entry.Score);
				return true;
			}
			catch
			{
				m_yourScoreText.text = yourScore;
				return false;
			}
		}

		private async Task<bool> TrySetLeaderBoard()
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