using UnityEngine;
using UnityEngine.UI;

public class TerritoryLabel : MonoBehaviour
{
	public Text territoryText;

	void Update()
	{
		territoryText.text = GameManager.instance.Territory;
	}
}
