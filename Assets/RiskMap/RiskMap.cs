using UnityEngine;
using UnityEngine.UI;
using System;

public class RiskMap : MonoBehaviour
{
	public GameObject canvas;
	public GameObject turrets;
	public GameObject turretprefab;
	public Territory[] territories;
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
		try
		{
			if (turretprefab == null)
			{
				turretprefab = (GameObject)Resources.Load("StandardTurret", typeof(GameObject));
			}
			if (turretprefab == null)
			{
				print("turretprefab is still null");
				return;
			}
			else
			{
				print("found turretprefab");
			}
			MapSetupRisk();
			turrets = new GameObject("Turrets On Map");
			foreach (Territory territory in territories)
			{
				territory.gameobject = Instantiate(turretprefab, new Vector3(territory.map.x, territory.map.y), canvas.transform.rotation, turrets.transform);
				territory.gameobject.transform.localScale = new Vector3(5, 5, 5);
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
		//Debug.Log("RiskMap OnMouseEnter: " + TerritoryName);
		TerritoryLabel.text = TerritoryName;
	}

	private void OnMouseExit()
	{
		//Debug.Log("RiskMap OnMouseExit: " + TerritoryName);
		TerritoryLabel.text = "";
	}

	private void OnMouseUp()
	{
		//Debug.Log("RiskMap OnMouseUp: " + TerritoryName);
		TerritoryLabel.text = TerritoryName + " selected";
		string maptoload;
		switch (TerritoryName)
		{
			case "Ontario": maptoload = "Level01"; break;
			case "Quebec": maptoload = "Level02"; break;
			default: maptoload = "LevelAutogen"; break;
		}
		sceneFader.FadeTo(maptoload, TerritoryName);
	}

	private void MapSetupRisk()
	{
		territories = new Territory[] {
			// north america
			Territories.Get("Alaska"), // 0
			Territories.Get("NorthWestTerritory"),
			Territories.Get("Greenland"),
			Territories.Get("Alberta"),
			Territories.Get("Ontario"),
			Territories.Get("Quebec"),
			Territories.Get("WesternUnitedStates"),
			Territories.Get("EasternUnitedStates"),
			Territories.Get("CentralAmerica"),
			// south america
			Territories.Get("Venezuela"), // 9
			Territories.Get("Peru"),
			Territories.Get("Brazil"),
			Territories.Get("Argentinia"),
			// europe
			Territories.Get("Iceland"), // 13
			Territories.Get("GreatBritain"),
			Territories.Get("Scandinavia"),
			Territories.Get("W.Europe"),
			Territories.Get("N.Europe"),
			Territories.Get("S.Europe"),
			Territories.Get("Ukraine"),
			// africa
			Territories.Get("NorthAfrica"), // 20
			Territories.Get("Egypt"),
			Territories.Get("EastAfrica"),
			Territories.Get("Congo"),
			Territories.Get("SouthAfrica"),
			Territories.Get("Madagascar"),
			// asia
			Territories.Get("MiddleEast"), // 26
			Territories.Get("Afghanistan"),
			Territories.Get("Ural"),
			Territories.Get("Siberia"),
			Territories.Get("Yakursk"),
			Territories.Get("Irkutsk"),
			Territories.Get("Mongolia"),
			Territories.Get("Kamchatka"),
			Territories.Get("Japan"),
			Territories.Get("China"),
			Territories.Get("India"),
			Territories.Get("Siam"),
			// australia
			Territories.Get("Indonesia"), // 38
			Territories.Get("NewGuinea"),
			Territories.Get("WesternAustralia"),
			Territories.Get("EasternAustralia")
		};
	}
}
