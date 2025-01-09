using System;
using Smash.System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NotImplementedException = System.NotImplementedException;

namespace Smash.Ui.Components
{
	public class LeaderboardEntry : MonoBehaviour
	{
		[SerializeField] private TMP_Text m_position;
		[SerializeField] private TMP_Text m_name;
		[SerializeField] private TMP_Text m_score;
		[SerializeField] private Image m_bgImage;

		public void Set(int position, string playerName, double score)
		{
			SetName(playerName);
			SetPosition(position);
			SetScore(score);
		}

		public void SetName(string playerName) => m_name.text = playerName;

		public void SetScore(double score)
		{
			m_score.text = ScoreHelper.ConvertScore(score);
		}
		
		public void SetPosition(int position) => m_position.text = position.ToString();

		public void CheckMyPlayer(string id)
		{
			if (String.Equals(id, PlayerAuthentication.Instance.PlayerId))
			{
				Color color = m_bgImage.color;
				color.a = 0.3f;
				m_bgImage.color = color;
			}
		}
	}
}
