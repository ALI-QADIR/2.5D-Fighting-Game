using System;
using Smash.StructsAndEnums;
using TMPro;
using TripleA.Utils.Observables.Primaries;
using UnityEngine;
using UnityEngine.UI;

namespace Smash.Player
{
	public class UiPawn : BasePawn
	{
		[SerializeField] private Image m_statusImage;
		[SerializeField] private TMP_Text m_statusText;
		[SerializeField] private TMP_Text m_indexText;
		private ObservableBool m_backHeld;

		private PlayerStatus m_status;
		private int m_index;
		
		public float BackHeldTime { get; set; }

		public override void Initialise()
		{
			// no-op
			// _inputHandler.SetUiPawn(this);
			m_backHeld = new ObservableBool(false);
		}

		public void BackHeld(bool held)
		{
			m_backHeld.Set(held);
		}

		public void SetIndex(int index)
		{
			m_index = index;
			index++;
			m_indexText.SetText("p" + index);
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
