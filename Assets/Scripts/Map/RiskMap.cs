using UnityEngine;
using UnityEngine.UI;
using System;

public partial class RiskMap : MonoBehaviour
{
	GameObject turrets;
	public GameObject canvas;
	public GameObject turretPrefab;
	public Territory[] territories;
	public Text TerritoryLabel;

	void Start()
	{
		GameManager.instance.soundManager.PlayMusic();
		try
		{
			if (turretPrefab == null)
			{
				turretPrefab = (GameObject)Resources.Load("StandardTurret", typeof(GameObject));
			}
			if (turretPrefab == null)
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
				territory.gameobject = Instantiate(turretPrefab, new Vector3(territory.map.x, territory.map.y), canvas.transform.rotation, turrets.transform);
				territory.gameobject.transform.localScale = new Vector3(5, 5, 5);
				territory.gameobject.transform.rotation = Quaternion.Euler(-90, 0, 0);
				// seek and destroy all mesh colliders so virgil's mouse code doesn't shit itself
				MeshCollider mc = territory.gameobject.GetComponentInChildren<MeshCollider>();
				if (mc != null) mc.enabled = false;
			}
		}
		catch (Exception ex)
		{
			print("Exception: " + ex.ToString());
		}
	}

	public void BackToMainMenu()
	{
		GameManager.instance.sceneController.FadeAndLoadScene("MainMenu");
	}

	public void GoToBattleground(string territory)
	{
		GameManager.instance.sceneController.FadeAndLoadScene("BattleGround", territory);
		//sceneFader.FadeTo("BattleGround", territory);
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
