using TMPro;
using UnityEngine;

namespace Smash.Ui
{
	public class MainMenuHandler : MonoBehaviour
	{
		[SerializeField] private TMP_Text m_nameText;
		[SerializeField] private TMP_Text m_scoreText;
		[SerializeField] private TMP_Text m_versionText;

		private void Awake()
		{
			m_versionText.text = Application.version;
		}
	}
}
