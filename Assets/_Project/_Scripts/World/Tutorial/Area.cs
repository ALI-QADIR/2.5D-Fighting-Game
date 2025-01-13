using System;
using UnityEngine;

namespace Smash.World.Tutorial
{
	public class Area : MonoBehaviour
	{
		[SerializeField] private Transform m_camPosition;
		public static event Action<Vector3> OnAreaEnter;

		private void OnTriggerEnter(Collider other)
		{
			OnAreaEnter?.Invoke(m_camPosition.position);
		}
	}
}
