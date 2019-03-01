using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
	public Text moneyText;

	void Update()
	{
		moneyText.text = "CASH: $" + PlayerStats.Money.ToString();
	}
}
