using System.Threading.Tasks;
using Smash.Services;
using Smash.System;
using Smash.Ui.Components;
using TMPro;
using UnityEngine;

namespace Smash.Ui
{
	public class LeaderboardHandler : MonoBehaviour
	{
		[SerializeField] private GameObject m_board;
		[SerializeField] private TMP_Text m_connectionStatusText;
		[SerializeField] private TMP_Text m_yourScoreText;
		[SerializeField] private LeaderboardEntry[] m_entries;
		
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
			bool isPlayerScoreSet = await LeaderboardServicesHandler.Instance.TryGetOrSetPlayerDetails();
			if (!isPlayerScoreSet)
			{
				SetLeaderboard(false);
				return; 
			} 
			SetPlayerScore();
			bool setLeaderboard = await LeaderboardServicesHandler.Instance.TryGetLeaderBoard(m_entries.Length);
			SetLeaderboard(setLeaderboard);
		}

		private void SetLeaderboard(bool setLeaderboard)
		{
			if (setLeaderboard) PopulateLeaderBoard();
			m_connectionStatusText.gameObject.SetActive(!setLeaderboard);
			m_connectionStatusText.text = "refresh failed!!";
			m_board.SetActive(setLeaderboard);
		}

		private void SetPlayerScore()
		{
			double playerScore = PlayerAuthentication.Instance.PlayerScore;
			m_yourScoreText.text =
				ScoreHelper.ApproximatelyEqual(playerScore, ScoreHelper.DefaultScore)
				? "--" : ScoreHelper.ConvertScore(playerScore);
		}

		private void PopulateLeaderBoard()
		{
			foreach (var entry in m_entries)
			{
				entry.gameObject.SetActive(false);
			}
			
			var entries = LeaderboardServicesHandler.Instance.Entries;
			
			for (int i = 0; i < entries.Count; i++)
			{
				double score = entries[i].Score;
				if (ScoreHelper.ApproximatelyEqual(score, ScoreHelper.DefaultScore))
				{
					score = 0;
				}
				m_entries[i].Set(
					position: i+1, 
					playerName: entries[i].PlayerName,
					score: score);
				m_entries[i].CheckMyPlayer(entries[i].PlayerId);
				m_entries[i].gameObject.SetActive(true);
			}
		}
	}
}
