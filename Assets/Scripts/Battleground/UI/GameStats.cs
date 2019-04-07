using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
	public GameObject attackPanel;
	public Text livesText;
	public Text moneyText;
	public Text territoryText;
	public Text wavesText;

	void Update()
	{
		attackPanel.SetActive(BattleManager.instance.attackMode);
		livesText.text = "LIVES: " + BattleManager.instance.stats.Lives.ToString();
		moneyText.text = "CASH: $" + BattleManager.instance.stats.Money.ToString();
		territoryText.text = GameManager.instance.TerritoryName;
		wavesText.text = string.Format("WAVE: {0}/{1}", BGWaveManager.waveNumber, BGWaveManager.totalWaves);
	}
}
