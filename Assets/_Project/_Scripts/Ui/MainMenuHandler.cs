using System.Threading.Tasks;
using Smash.Services;
using Smash.System;
using TMPro;
using UnityEngine;

namespace Smash.Ui
{
	public class MainMenuHandler : MonoBehaviour
	{
		[SerializeField] private TMP_Text m_nameText;
		[SerializeField] private TMP_Text m_scoreText;
		[SerializeField] private TMP_Text m_versionText;

		private void Awake()
		{
			m_versionText.text = Application.version;
		}

		public async void OnOpen()
		{
			if (PlayerAuthentication.Instance.IsSignedIn())
				SetPlayerName();
			else
			{
				m_nameText.text = "not signed in";
				m_scoreText.text = string.Empty;
				await SignIn();
			}
		}

		public async void OnRetry()
		{
			await SignIn();
		}

		private async Task SignIn()
		{
			bool isSignInSuccessful = await PlayerAuthentication.Instance.TryAnonSignin();
			if (isSignInSuccessful)
			{
				SetPlayerName();
			}
			else
			{
				SignInFailed();
			}
		}

		private void SignInFailed()
		{
			m_nameText.text = "sign in failed!!";
			m_scoreText.text = "press 'R' / right trigger or click to retry";
		}

		private async void SetPlayerName()
		{
			if (!PlayerAuthentication.Instance.IsSignedIn())
			{
				m_nameText.text = "signing in anonymously...";
				m_scoreText.text = string.Empty;
				await SignIn();
			}
			await Task.Delay(500);
			bool isPlayerSet = await LeaderboardServicesHandler.Instance.TryGetOrSetPlayerDetails();
			if (!isPlayerSet)
			{
				SignInFailed();
				return;
			}
			m_nameText.text = PlayerAuthentication.Instance.PlayerName;
			m_scoreText.text =
				ScoreHelper.ApproximatelyEqual(ScoreHelper.DefaultScore, PlayerAuthentication.Instance.PlayerScore)
					? "score not set"
					: ScoreHelper.ConvertScore(PlayerAuthentication.Instance.PlayerScore);
		}
	}
}
