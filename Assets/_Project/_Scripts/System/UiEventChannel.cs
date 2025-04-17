using System;
using TripleA.EventSystem.EventChannel;
using UnityEngine;

namespace Smash.System
{
	[CreateAssetMenu(fileName = "Ui Event Channel", menuName = "Scriptable Objects/Event Channels/Ui Event Channel")]
	public class UiEventChannel : EventChannel<UiEventArgs>
	{
        
	}

	[Serializable]
	public class UiEventArgs
	{
		public string id;
		[HideInInspector] public MonoBehaviour sender;
	}
}
