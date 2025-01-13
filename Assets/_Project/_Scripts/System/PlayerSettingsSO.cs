using TripleA.Observables.Primaries;
using UnityEngine;
using UnityEngine.Events;

namespace Smash.System
{
	[CreateAssetMenu(fileName = "Player Settings", menuName = "Scriptable Objects/Player Settings")]
	public class PlayerSettingsSO : ScriptableObject
	{
		[SerializeField] private ObservableBool m_touchControls = new (true);
		[SerializeField] private ObservableBool m_sfx = new (true);
		[SerializeField] private ObservableInt m_sfxVolume = new (5);
		
		public bool GetTouchControls => m_touchControls.Value;
		public void SetTouchControls(bool value) => m_touchControls.Set(value);
		public void AddTouchControlsListener(UnityAction<bool> callback) => m_touchControls.AddListener(callback);
		public void RemoveTouchControlsListener(UnityAction<bool> callback) => m_touchControls.RemoveListener(callback);
		
		public bool GetSfx => m_sfx.Value;
		public void SetSfx(bool value) => m_sfx.Set(value);
		public void AddSfxListener(UnityAction<bool> callback) => m_sfx.AddListener(callback);
		public void RemoveSfxListener(UnityAction<bool> callback) => m_sfx.RemoveListener(callback);
		
		public void SetSfxVolume(int value) => m_sfxVolume.Set(value);
		public int GetSfxVolume => m_sfxVolume.Value;
		public void AddSfxVolListener(UnityAction<int> callback) => m_sfxVolume.AddListener(callback);
		public void RemoveSfxVolListener(UnityAction<int> callback) => m_sfxVolume.RemoveListener(callback);
	}
}