using System;
using PrimeTween;
using UnityEngine;

namespace Smash.World.Tutorial
{
	public class CameraController : MonoBehaviour
	{
		private Transform m_tr;
		[SerializeField] private float m_duration = 0.5f;
		[SerializeField] private Ease m_ease = Ease.InOutSine;

		private void Awake()
		{
			m_tr ??= transform;
		}

		private void OnEnable()
		{
			Area.OnAreaEnter += OnAreaEnter;
		}

		private void OnDestroy()
		{
			Area.OnAreaEnter -= OnAreaEnter;
		}

		private void OnDestroy()
		{
			Area.OnAreaEnter -= OnAreaEnter;
		}

		private void OnAreaEnter(Vector3 obj)
		{
			Tween.Position(m_tr, obj, m_duration, m_ease);
		}
	}
}