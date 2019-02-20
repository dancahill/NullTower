using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
public class RiskMap : MonoBehaviour
{
	public GameObject canvas;
	public GameObject turretprefab;
	public MapTerritory[] territories;
	public SpriteRenderer sprite;
	public string TerritoryName = "Unknown";
	public Text TerritoryLabel;
	public SceneFader sceneFader;

	void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		territories = new MapTerritory[] {
			new MapTerritory { territoryname="Alberta",          gameobject = null, mapx = -234, mapy =  107, neighbours = new[] {  1, -1 } },
			new MapTerritory { territoryname="Ontario",          gameobject = null, mapx = -185, mapy =   96, neighbours = new[] {  1, -1 } },
			new MapTerritory { territoryname="Quebec",           gameobject = null, mapx = -133, mapy =   98, neighbours = new[] {  0, -1 } },
			new MapTerritory { territoryname="WesternAustralia", gameobject = null, mapx =  211, mapy = -145, neighbours = new[] { -1, -1 } }
		};
		try
		{
			if (turretprefab == null)
			{
				turretprefab = (GameObject)Resources.Load("StandardTurret", typeof(GameObject));
			}
			foreach (MapTerritory territory in territories)
			{
				territory.gameobject = Instantiate(turretprefab, new Vector3(territory.mapx, territory.mapy), canvas.transform.rotation);
				territory.gameobject.transform.localScale = new Vector3(5, 5, 5);
				// needs a reference to whatever is actually rotated, and then this can be rotated/positioned based on that
				// just cheat for now
				territory.gameobject.transform.rotation = Quaternion.Euler(-90, 0, 0);
			}
		}
		catch (Exception ex)
		{
			print("Exception: " + ex.ToString());
		}
	}

	private void OnMouseEnter()
	{
		Debug.Log("RiskMap OnMouseEnter: " + TerritoryName);
		TerritoryLabel.text = TerritoryName;
	}

	private void OnMouseExit()
	{
		Debug.Log("RiskMap OnMouseExit: " + TerritoryName);
		TerritoryLabel.text = "";
	}

	private void OnMouseUp()
	{
		Debug.Log("RiskMap OnMouseUp: " + TerritoryName);
		TerritoryLabel.text = TerritoryName + " clicked";
		string maptoload = TerritoryName == "Ontario" ? "Level01" : "Level02";
		sceneFader.FadeTo(maptoload);
	}
}
