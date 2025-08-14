using System.Collections.Generic;
using Smash.Player;
using Smash.StructsAndEnums;
using Smash.Ui.Panels;
using UnityEngine;

namespace Smash.System
{
	public class SelectionManager : GameManager<SelectionManager>
	{
		[SerializeField] private UiPawn m_playerPawnPrefab;
		[SerializeField] private CharacterSelectPanelHandler m_characterSelectPanel;
		
		private Dictionary<int, UiPawn> m_uiPawns;

		protected override void Awake()
		{
			base.Awake();
			m_uiPawns = new Dictionary<int, UiPawn>();
			PlayerControllerManager.Instance.OnPlayerLeft += PlayerLeft;
			PlayerControllerManager.Instance.OnPlayerRegained += PlayerRegained;

			for (int i = 0; i < 2; i++)
			{
				var uiPawn = InstantiateUiPawn(i);
				uiPawn.SetStatus(PlayerStatus.Waiting);
				uiPawn.SetIndex(i);
			}
			
			RearrangeUiPawns();
		}

		#region Private Methods
		
		protected override void PlayerJoined(int index)
		{
			Debug.Log("Player Joined");
			var uiPawn = index >= 2 ? InstantiateUiPawn(index) : m_uiPawns[index];
			uiPawn.SetIndex(index);
			uiPawn.SetStatus(PlayerStatus.Selecting);
			// Debug.Log(index);
			// var ctr = PlayerControllerManager.Instance.InitialisePawn(index);
			PlayerControllerManager.Instance.DisableAllPlayerInput();
			PlayerControllerManager.Instance.DisableAllUiInput();
			// ctr.EnableUiInputAndDisablePlayerInput();
		}
		
		protected override void SceneLoaded()
		{
			// Debug.Log("Scene Loaded");
			PlayerDevicesManager.Instance.EnablePlayerJoining(6);
		}

		private void PlayerRegained(int index)
		{
			Debug.Log("Player Regained");
			PlayerJoined(index);
		}

		private void PlayerLeft(int index)
		{
			Debug.Log($"{PlayerControllerManager.Instance.CurrentPlayerCount} === {m_uiPawns.Count}");
			if (m_uiPawns.Count <= 2)
			{
				m_uiPawns[index].SetStatus(PlayerStatus.Waiting);
			}
			else
			{
				m_uiPawns.Remove(index, out UiPawn value);
				Destroy(value.gameObject);
				RearrangeUiPawns();
			}
		}

		private UiPawn InstantiateUiPawn(int index)
		{
			var uiPawn = Instantiate(m_playerPawnPrefab, m_characterSelectPanel.GetViewportTransform());
			m_uiPawns.Add(index, uiPawn);
			return uiPawn;
		}

		private void RearrangeUiPawns()
		{
			Debug.Log("rearranging ui pawns");
			var transformList = m_characterSelectPanel.GetTransforms(m_uiPawns.Count);
			int i = 0;
			foreach (var rectTransform in transformList)
			{
				var gotPawn = false;
				do
				{
					gotPawn = m_uiPawns.TryGetValue(i, out UiPawn uiPawn);
					if (gotPawn)
					{
						uiPawn.SetTransformValues(rectTransform);
					}
					i++;
				} while (!gotPawn);
			}
		}

		#endregion Private Methods
	}
}
