using System;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Smash.Ui.Components
{
	[RequireComponent(typeof(Button))]
	public class ButtonSelectionHandler : UiEventInvoker, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{ 
		[SerializeField] private Button m_button;
		[SerializeField] private AnimationStrategy<BaseEventData> m_animationStrategy;
		[SerializeField] private bool m_deactivateOnClick;

		public static event Action<GameObject> OnButtonDeselected;

		protected override void Awake()
		{
			base.Awake();
			m_button.onClick.AddListener(InvokeEvent);
			m_button.onClick.AddListener(ButtonClickSound);
			if (m_deactivateOnClick) m_button.onClick.AddListener(() => OnDeselect(null));
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			eventData.selectedObject = gameObject;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			eventData.selectedObject = null;
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
