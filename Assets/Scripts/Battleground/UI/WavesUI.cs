using UnityEngine;
using UnityEngine.UI;

public class WavesUI : MonoBehaviour
{
	public Text m_WavesText;

	void Update()
	{
		m_WavesText.text = string.Format("WAVE: {0}/{1}", BGWaveManager.waveNumber, BGWaveManager.totalWaves);
	}
}
