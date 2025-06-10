using System;
using Smash.Player.CommandPattern.ActionCommands;
using Smash.StructsAndEnums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Smash.Player
{
	public class UiPawn : BasePawn
	{
		[SerializeField] private Image m_statusImage;
		[SerializeField] private TMP_Text m_statusText;
		[SerializeField] private TMP_Text m_indexText;

		private PlayerStatus m_status;

		public override void Initialise()
		{
		}
		
		public override void SetIndex(int index)
		{
			PlayerIndex = index;
			index++;
			m_indexText.SetText("p" + index);
		}

		public override void HandleCancelButton()
		{
		}

		public override void HandleCancelButton(IGameplayActionCommand command)
		{
		}

		public override void HandleSubmitButton(IGameplayActionCommand command)
		{
		}

		public override void HandleHorizontalScrollInput(IGameplayActionCommand command)
		{
		}

		public override void HandleVerticalScrollInput(IGameplayActionCommand command)
		{
		}

		public override void HandleShoulderTriggerInput(IGameplayActionCommand command)
		{
		}

		public override void HandleShoulderButtonInput(IGameplayActionCommand command)
		{
		}

		public override void HandleResumeInput(IGameplayActionCommand command)
		{
		}

		public void SetStatus(PlayerStatus status)
		{
			m_status = status;

			switch (status)
			{
				case PlayerStatus.Ready:
					m_statusImage.enabled = true;
					m_statusText.enabled = true;
					m_statusText.SetText("ready");
					break;
				case PlayerStatus.Waiting:
					m_statusImage.enabled = true;
					m_statusText.enabled = true;
					m_statusText.SetText("waiting...");
					break;
				case PlayerStatus.Selecting:
					m_statusText.enabled = false;
					m_statusImage.enabled = false;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}

		public void SetTransformValues(RectTransform newRectTransform)
		{
			RectTransform rectTransform = (RectTransform) transform;
			rectTransform.position = newRectTransform.position;
		}
	}
}
