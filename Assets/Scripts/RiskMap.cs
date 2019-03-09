using UnityEngine;
using UnityEngine.UI;
using System;

public partial class RiskMap : MonoBehaviour
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
		if (Manager.PlayMusic)
		{
			AudioSource audio = gameObject.AddComponent<AudioSource>();
			AudioClip clip = (AudioClip)Resources.Load("Music/07 - human briefing");
			audio.PlayOneShot(clip);
		}
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
				//print("found turretprefab");
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
		Toast(TerritoryName);
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
			//case "Ontario": maptoload = "Level01"; break;
			default: maptoload = "BattleGround"; break;
		}
		//sceneFader.FadeTo(maptoload, TerritoryName);
		FaderTest fader = gameObject.AddComponent<FaderTest>();
		fader.FadeTo(maptoload, TerritoryName);
	}

	private void MapSetupRisk()
	{
		territories = new Territory[] {
			// north america
			Territories.Get("Alaska"), // 0
			Territories.Get("Northwest Territory"),
			Territories.Get("Greenland"),
			Territories.Get("Alberta"),
			Territories.Get("Ontario"),
			Territories.Get("Quebec"),
			Territories.Get("Western United States"),
			Territories.Get("Eastern United States"),
			Territories.Get("Central America"),
/*
			// south america
			Territories.Get("Venezuela"), // 9
			Territories.Get("Peru"),
			Territories.Get("Brazil"),
			Territories.Get("Argentinia"),
			// europe
			Territories.Get("Iceland"), // 13
			Territories.Get("Great Britain"),
			Territories.Get("Scandinavia"),
			Territories.Get("Western Europe"),
			Territories.Get("Northern Europe"),
			Territories.Get("Southern Europe"),
			Territories.Get("Ukraine"),
			// africa
			Territories.Get("North Africa"), // 20
			Territories.Get("Egypt"),
			Territories.Get("East Africa"),
			Territories.Get("Congo"),
			Territories.Get("South Africa"),
			Territories.Get("Madagascar"),
			// asia
			Territories.Get("Middle East"), // 26
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
			Territories.Get("New Guinea"),
			Territories.Get("Western Australia"),
			Territories.Get("Eastern Australia")
*/
		};
	}
}
