using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Smash.Ui
{
	[RequireComponent(typeof(Button))]
	public class ButtonSelectionHandler : UiEventInvoker, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{ 
		[Header("Components")]
		[SerializeField] private Image m_fadingImage;
		[SerializeField] private Button m_button;
		
		[Header("Animation")]
		[SerializeField] private float m_duration = 0.1f;
		[SerializeField] private float m_scaleAmount = 1.1f;

		private static readonly Color m_S_InactiveColor = new (1, 1, 1, 0.0f);
		private static readonly Color m_S_ActiveColor = Color.white;

		private static Vector3 m_s_scale;
		
		private Sequence m_selectSequence, m_deselectSequence;
		private Transform m_tr;

		public static event Action<GameObject> OnButtonDeselected;

		protected override void Awake()
		{
			base.Awake();
			m_s_scale = Vector3.one * m_scaleAmount;
			m_tr = transform;
			m_fadingImage.color = m_S_InactiveColor;
			m_button.onClick.AddListener(InvokeEvent);
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
			if (m_deselectSequence.isAlive) m_deselectSequence.Stop();
			// Todo: Sound
			m_selectSequence = Sequence.Create()
				.Group(Tween.Color(m_fadingImage, m_S_InactiveColor, m_S_ActiveColor, m_duration))
				.Group(Tween.Scale(target: m_tr, startValue: Vector3.one, endValue: m_s_scale, duration: m_duration));
			
		}

		public void OnDeselect(BaseEventData eventData)
		{
			if (m_selectSequence.isAlive) m_deselectSequence.Stop();
			
			OnButtonDeselected?.Invoke(gameObject);
			
			m_deselectSequence = Sequence.Create()
				.Group(Tween.Color(m_fadingImage, m_S_ActiveColor, m_S_InactiveColor, m_duration))
				.Group(Tween.Scale(target: m_tr, startValue: m_s_scale, endValue: Vector3.one, duration: m_duration));
		}
	}
}