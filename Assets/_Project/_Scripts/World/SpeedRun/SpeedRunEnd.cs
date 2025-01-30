using System;
using Smash.System;
using UnityEngine;

namespace Smash.World.SpeedRun
{
	[RequireComponent(typeof(BoxCollider))]
	public class SpeedRunEnd : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			SpeedRunManager.Instance.ShouldGameEnd();
		}
	}
}