using UnityEngine;

[System.Serializable]
public class MapTerritory
{
	public GameObject gameobject = null;
	public string name = "Unknown";
	public float mapx;
	public float mapy;
	public int[] neighbours;
}
