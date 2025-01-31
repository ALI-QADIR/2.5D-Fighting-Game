using System;
using Smash.System;
using UnityEngine;
using UnityEngine.Video;

namespace Smash.World.SpeedRun
{
	[RequireComponent(typeof(BoxCollider))]
	public class VidPlayerTrigger : MonoBehaviour
	{
		[SerializeField] private VideoPlayer m_player;

		private void Awake()
		{
			m_player.playOnAwake = false;
			m_player.Stop();
			m_player.gameObject.SetActive(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			m_player.gameObject.SetActive(true);
			m_player.Play();
		}

		private void OnTriggerExit(Collider other)
		{
			m_player.Stop();
			m_player.gameObject.SetActive(false);
		}
	}
}