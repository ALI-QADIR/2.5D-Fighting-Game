using System.Threading.Tasks;
using Smash.Player.Input;
using Smash.Services;
using Smash.Ui.Panels;
using TripleA.FSM;
using TripleA.ImprovedTimer.Timers;
using TripleA.Singletons;
using UnityEngine;

namespace Smash.System
{
	public class SpeedRunManager : GenericSingleton<SpeedRunManager>
	{
		[SerializeField] private SpeedRunOptionsPanel m_optionsPanel;
		[SerializeField] private SpeedRunTimerPanel m_timerPanel;
		[SerializeField] private InputReader m_playerInput;
		[SerializeField] private Transform m_playerStart;
		
		private StopwatchTimer m_stopwatchTimer;
		private FrequencyTimer m_countdownTimer;
		private StateMachine m_stateMachine;

		private bool m_shouldBeginCountdown, m_shouldBeginGame, m_shouldGameEnd;
		private int m_count;

		#region States

		private SpeedRunInit m_init;
		private SpeedRunCountdown m_countdown;
		private SpeedRunActive m_active;
		private SpeedRunEnd m_end;
		private SpeedRunGetReady m_getReady;

		#endregion States

		protected override void Awake()
		{
			base.Awake();
			SetUpStateMachine();

			m_stopwatchTimer = new StopwatchTimer(2f);
			m_stopwatchTimer.onTimerUpdate += OnTimerUpdate;
			m_countdownTimer = new FrequencyTimer(1);
			m_countdownTimer.onTick += CountDownTick;
		}

		private void Update()
		{
			m_stateMachine.OnUpdate();
		}

		public void OpenStartPanel()
		{
			m_shouldBeginCountdown = false;
			m_shouldBeginGame = false;
			m_shouldGameEnd = false;
			
			m_playerInput.DisablePlayerInput();
			m_optionsPanel.BeginGame();
			// Debug.LogWarning("Init");
		}

		public void BeginCountDown()
		{
			m_countdownTimer.Reset();
			m_count = 3;
			m_countdownTimer.Start();
			m_stopwatchTimer.Reset();
			
			m_shouldBeginCountdown = false;
			m_playerInput.transform.position = m_playerStart.position;
			m_timerPanel.SetText("3");
			
			// Debug.LogWarning("Countdown Start");
		}

		public void GameStarted()
		{
			m_countdownTimer.Reset();
			m_stopwatchTimer.Start();
			
			m_playerInput.EnablePlayerInput();
			
			m_shouldBeginGame = false;
		}

		public void GameEnded()
		{
			m_shouldGameEnd = false;
			
			m_playerInput.DisablePlayerInput();
			
			float time = m_stopwatchTimer.CurrentTime;
			m_timerPanel.Time = time;
			m_stopwatchTimer.Stop();
			m_stopwatchTimer.Reset();
			
			m_optionsPanel.EndGame(time.ToString("F3"));
			PlayerAuthentication.Instance.SetPlayerScore(time * 1000f);
			SetPlayerScore();
		}
		
		private async void SetPlayerScore()
		{
			if (!PlayerAuthentication.Instance.IsSignedIn())
			{
				Debug.Log("Could not update leaderboard");
				return;
			}
			await Task.Delay(500);
			await LeaderboardServicesHandler.Instance.TryGetOrSetPlayerDetails();
		}
		
		public void ShouldBeginCountdown() => m_shouldBeginCountdown = true;
		public void ShouldGameEnd() => m_shouldGameEnd = true;
		
		private void OnTimerUpdate(float time) => m_timerPanel.Time = time;

		private void CountDownTick()
		{
			m_count--;
			m_timerPanel.SetText(m_count.ToString());
			if (m_count < 0) m_shouldBeginGame = true;
		}

		private void SetUpStateMachine()
		{
			m_stateMachine = new StateMachine();
			
			m_init = new SpeedRunInit(Instance);
			m_getReady = new SpeedRunGetReady(Instance);
			m_countdown = new SpeedRunCountdown(Instance);
			m_active = new SpeedRunActive(Instance);
			m_end = new SpeedRunEnd(Instance);
			
			AddTransition(m_init, m_getReady, new FuncPredicate(() => true));
			AddTransition(m_getReady, m_countdown, new FuncPredicate(() => m_shouldBeginCountdown));
			AddTransition(m_countdown, m_active, new FuncPredicate(() => m_shouldBeginGame));
			AddTransition(m_active, m_end, new FuncPredicate(() => m_shouldGameEnd));
			AddTransition(m_end, m_countdown, new FuncPredicate(() => m_shouldBeginCountdown));
			
			m_stateMachine.SetState(m_init);
		}
		
		private void AddTransition(IState from, IState to, IPredicate condition) =>
			m_stateMachine.AddTransition(from, to, condition);
		
		private void AddAnyTransition(IState to, IPredicate condition) => m_stateMachine.AddAnyTransition(to, condition);
	}
}