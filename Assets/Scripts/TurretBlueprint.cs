using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
	public GameObject prefab;
	public GameObject upgradedPrefab;
	public int cost;
	public int upgradeCost;
	public float startHealth;

	public int GetSellAmount()
	{
		return cost / 2;
	}
}
