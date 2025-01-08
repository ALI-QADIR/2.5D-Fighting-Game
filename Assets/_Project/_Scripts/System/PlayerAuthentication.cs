using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TripleA.Singletons;

namespace Smash.System
{
	public class PlayerAuthentication : PersistentSingleton<PlayerAuthentication>
	{
		private PlayerSignInData m_playerSignInData;
		public string PlayerId => m_playerSignInData?.Id;
		public string AccessToken => m_playerSignInData?.AccessToken;
		public string PlayerName => m_playerSignInData?.PlayerName;
		public double PlayerScore => m_playerSignInData?.PlayerScore ?? ScoreHelper.DefaultScore;
		public void SetPlayerScore(double score) => m_playerSignInData?.SetPlayerScore(score);
		public void SetPlayerName(string playerName) => m_playerSignInData?.SetPlayerName(playerName);
		
		private bool m_isServiceInitialised;

		public bool IsSignedIn()
		{
			if (!m_isServiceInitialised) return false;
			return AuthenticationService.Instance.IsSignedIn;
		}

		protected override void Awake()
		{
			base.Awake();
			m_isServiceInitialised = false;
		}

		public async Task<bool> InitializeUnityAuthentication()
		{
			if (m_isServiceInitialised) return true;
			Debug.Log("Initializing Unity Authentication");
			try
			{
				await UnityServices.InitializeAsync();

				Debug.Log("Setup Events");
				SetUpEvents();
				m_isServiceInitialised = true;
			}
			catch (Exception e)
			{
				Debug.LogError("Unity Authentication Failed " + e);
				m_isServiceInitialised = false;
			}

			return m_isServiceInitialised;
		}

		public async Task<bool> TryAnonSignin()
		{
			Debug.Log("Try Sign In");
			try
			{
				await AuthenticationService.Instance.SignInAnonymouslyAsync();
				m_playerSignInData = CreatePlayerData();
				Debug.Log($"Name {m_playerSignInData.PlayerName}\nID {m_playerSignInData.Id}");
				return true;
			}
			catch (ServicesInitializationException e)
			{
				Debug.Log("Initialising Service" + e);
				await InitializeUnityAuthentication();
				return false;
			}
			catch (Exception e)
			{
				Debug.LogError("Sign In Failed " + e);
				return false;
			}
		}

		private PlayerSignInData CreatePlayerData() => new (AuthenticationService.Instance.PlayerId,
			AuthenticationService.Instance.AccessToken, "anon", 84540);

		private void SetUpEvents()
		{
			AuthenticationService.Instance.SignedIn += InstanceOnSignedIn;
			AuthenticationService.Instance.SignedOut += InstanceOnSignedOut;
			AuthenticationService.Instance.SignInFailed += InstanceOnSignInFailed;
		}

		private void InstanceOnSignInFailed(RequestFailedException obj)
		{
			Debug.Log("sign in failed");
		}

		private void InstanceOnSignedOut()
		{
			Debug.Log("Signed Out");
		}

		private void InstanceOnSignedIn()
		{
			Debug.Log("Signed In");
		}
	}
}