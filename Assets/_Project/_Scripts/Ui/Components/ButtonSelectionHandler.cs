using System;
using Smash.System;
using Smash.Ui.System;
using TripleA.Utils.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Smash.Ui.Components
{
	[RequireComponent(typeof(Button))]
	public class ButtonSelectionHandler : UiEventInvoker, ISelectHandler, IDeselectHandler
	{ 
		[SerializeField] private Button m_button;
		[SerializeField] private AnimationStrategy<BaseEventData> m_animationStrategy;
		[SerializeField] private bool m_deactivateOnClick;

		public static event Action<GameObject> OnButtonDeselected;

		protected void Awake()
		{
			m_animationStrategy ??= gameObject.GetOrAddComponent<AnimationStrategy<BaseEventData>>();
			
			m_button.onClick.AddListener(ButtonClickSound);
			if (m_deactivateOnClick) m_button.onClick.AddListener(() => OnDeselect(null));
		}

		public void OnSelect(BaseEventData eventData)
		{
			if (AudioManager.HasInstance)
				AudioManager.Instance.PlayButtonTransition();

			m_animationStrategy.Activate();
		}

		public void OnDeselect(BaseEventData eventData)
		{
			m_animationStrategy.Deactivate();
			OnButtonDeselected?.Invoke(gameObject);
		}

		private void ButtonClickSound()
		{
			if (AudioManager.HasInstance)
				AudioManager.Instance.PlayButtonClick();
		}
	}
}
