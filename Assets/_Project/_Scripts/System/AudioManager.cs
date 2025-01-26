using System;
using TripleA.Singletons;
using UnityEngine;
using UnityEngine.Audio;

namespace Smash.System
{
	public class AudioManager : PersistentSingleton<AudioManager>
	{
		[SerializeField] private PlayerSettingsSO m_playerSettings;
		[SerializeField] private AudioMixerGroup m_sfxMixerGroup;
		
		[SerializeField] private AudioSource m_btnClickAudioSource;
		[SerializeField] private AudioClip m_btnClickAudioClip;
		[SerializeField] private AudioSource m_btnTransitionAudioSource;
		[SerializeField] private AudioClip m_btnTransitionAudioClip;

		private const string k_Sfx_Vol_Key = "SfxVolume";
		private float m_lastSfxVolume;

		protected override void Awake()
		{
			base.Awake();
			m_playerSettings.AddSfxListener(ToggleSfx);
			m_sfxMixerGroup.audioMixer.GetFloat(k_Sfx_Vol_Key, out m_lastSfxVolume);
		}

		private void OnDisable()
		{
			m_playerSettings.RemoveSfxListener(ToggleSfx);
		}

		public void PlayButtonClick() => m_btnClickAudioSource.PlayOneShot(m_btnClickAudioClip);
		
		public void PlayButtonTransition() => m_btnTransitionAudioSource.PlayOneShot(m_btnTransitionAudioClip);
		
		private void ToggleSfx(bool toggle)
		{
			if (toggle)
			{
				m_sfxMixerGroup.audioMixer.SetFloat(k_Sfx_Vol_Key, m_lastSfxVolume);
			}
			else
			{
				m_sfxMixerGroup.audioMixer.GetFloat(k_Sfx_Vol_Key, out m_lastSfxVolume);
				m_sfxMixerGroup.audioMixer.SetFloat(k_Sfx_Vol_Key, -80f);
			}
		}
	}
}
