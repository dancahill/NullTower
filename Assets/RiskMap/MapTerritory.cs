using UnityEngine;

[System.Serializable]
public class MapTerritory
{
	public GameObject gameobject;
	public string territoryname = "Unknown";
	public float mapx;
	public float mapy;
	public int[] neighbours;
}
