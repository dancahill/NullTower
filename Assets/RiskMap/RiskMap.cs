using UnityEngine;
using UnityEngine.UI;
using System;

//[RequireComponent(typeof(PolygonCollider2D))]
public class RiskMap : MonoBehaviour
{
	public GameObject canvas;
	public GameObject turrets;
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
		try
		{
			// how does Start() run five times and fail the first four?
			//
			// i think this is because the class was attached to the territory gameobjects as well as the map
			// test later
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
			foreach (MapTerritory territory in territories)
			{
				territory.gameobject = Instantiate(turretprefab, new Vector3(territory.mapx, territory.mapy), canvas.transform.rotation, turrets.transform);
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
		string maptoload;// = TerritoryName == "Ontario" ? "Level01" : "Level02";
		switch (TerritoryName)
		{
			case "Ontario": maptoload = "Level01"; break;
			case "Quebec": maptoload = "Level02"; break;
			//case "Alberta": maptoload = "LevelAutogen"; break;
			default: maptoload = "LevelAutogen"; break;
		}
		sceneFader.FadeTo(maptoload);
	}

	private void MapSetupRisk()
	{
		territories = new MapTerritory[] {
			// north america
			new MapTerritory { name="Alaska",              mapx = -305, mapy =  136, neighbours = new[] { 1, 3, 33 }         }, // 0
			new MapTerritory { name="NorthWestTerritory",  mapx = -215, mapy =  130, neighbours = new[] { 0, 2, 3, 4 }       }, // 1
			new MapTerritory { name="Greenland",           mapx =  -82, mapy =  141, neighbours = new[] { 1, 4, 5, 13 }      }, // 2
			new MapTerritory { name="Alberta",             mapx = -234, mapy =  107, neighbours = new[] { 0, 1, 4, 6 }       }, // 3
			new MapTerritory { name="Ontario",             mapx = -185, mapy =   96, neighbours = new[] { 1, 2, 3, 5, 6, 7 } }, // 4
			new MapTerritory { name="Quebec",              mapx = -133, mapy =   98, neighbours = new[] { 2, 4, 6 }          }, // 5
			new MapTerritory { name="WesternUnitedStates", mapx = -222, mapy =   60, neighbours = new[] { 3, 4, 7, 8 }       }, // 6
			new MapTerritory { name="EasternUnitedStates", mapx = -186, mapy =   49, neighbours = new[] { 4, 5, 6, 8 }       }, // 7
			new MapTerritory { name="CentralAmerica",      mapx = -230, mapy =   20, neighbours = new[] { 6, 7, 9 }          }, // 8
			// south america
			new MapTerritory { name="Venezuela",           mapx = -205, mapy =  -42, neighbours = new[] { 8, 10, 11 }        }, // 9
			new MapTerritory { name="Peru",                mapx = -183, mapy = -105, neighbours = new[] { 9, 11, 12 }        }, // 10
			new MapTerritory { name="Brazil",              mapx = -143, mapy =  -83, neighbours = new[] { 9, 10, 12, 20 }    }, // 11
			new MapTerritory { name="Argentinia",          mapx = -185, mapy = -158, neighbours = new[] { 10, 11 }           }, // 12
			// europe
			new MapTerritory { name="Iceland",             mapx =  -57, mapy =  122, neighbours = new[] { -1 } }, // 13
			new MapTerritory { name="GreatBritain",        mapx =  -48, mapy =   78, neighbours = new[] { -1 } }, // 14
			new MapTerritory { name="Scandinavia",         mapx =   -9, mapy =  135, neighbours = new[] { -1 } }, // 15
			new MapTerritory { name="W.Europe",            mapx =  -51, mapy =   30, neighbours = new[] { -1 } }, // 16
			new MapTerritory { name="N.Europe",            mapx =   -9, mapy =   80, neighbours = new[] { -1 } }, // 17
			new MapTerritory { name="S.Europe",            mapx =    5, mapy =   34, neighbours = new[] { -1 } }, // 18
			new MapTerritory { name="Ukraine",             mapx =   45, mapy =   70, neighbours = new[] { -1 } }, // 19
			// africa
			new MapTerritory { name="NorthAfrica",         mapx =  -44, mapy =  -36, neighbours = new[] { -1 } }, // 20
			new MapTerritory { name="Egypt",               mapx =    7, mapy =  -16, neighbours = new[] { -1 } }, // 21
			new MapTerritory { name="EastAfrica",          mapx =   32, mapy =  -68, neighbours = new[] { -1 } }, // 22
			new MapTerritory { name="Congo",               mapx =    0, mapy = -100, neighbours = new[] { -1 } }, // 23
			new MapTerritory { name="SouthAfrica",         mapx =    0, mapy = -159, neighbours = new[] { -1 } }, // 24
			new MapTerritory { name="Madagascar",          mapx =   55, mapy = -159, neighbours = new[] { -1 } }, // 25
			// asia
			new MapTerritory { name="MiddleEast",          mapx =   59, mapy =   -2, neighbours = new[] { -1 } }, // 26
			new MapTerritory { name="Afghanistan",         mapx =  103, mapy =   48, neighbours = new[] { -1 } }, // 27
			new MapTerritory { name="Ural",                mapx =  121, mapy =  109, neighbours = new[] { -1 } }, // 28
			new MapTerritory { name="Siberia",             mapx =  170, mapy =  125, neighbours = new[] { -1 } }, // 29
			new MapTerritory { name="Yakursk",             mapx =  229, mapy =  142, neighbours = new[] { -1 } }, // 30
			new MapTerritory { name="Irkutsk",             mapx =  219, mapy =   98, neighbours = new[] { -1 } }, // 31
			new MapTerritory { name="Mongolia",            mapx =  236, mapy =   66, neighbours = new[] { -1 } }, // 32
			new MapTerritory { name="Kamchatka",           mapx =  310, mapy =  133, neighbours = new[] { -1 } }, // 33
			new MapTerritory { name="Japan",               mapx =  274, mapy =   40, neighbours = new[] { -1 } }, // 34
			new MapTerritory { name="China",               mapx =  184, mapy =   27, neighbours = new[] { -1 } }, // 35
			new MapTerritory { name="India",               mapx =  140, mapy =  -19, neighbours = new[] { -1 } }, // 36
			new MapTerritory { name="Siam",                mapx =  191, mapy =  -30, neighbours = new[] { -1 } }, // 37
			// australia
			new MapTerritory { name="Indonesia",           mapx =  208, mapy =  -81, neighbours = new[] { -1 } }, // 38
			new MapTerritory { name="NewGuinea",           mapx =  282, mapy =  -94, neighbours = new[] { -1 } }, // 39
			new MapTerritory { name="WesternAustralia",    mapx =  211, mapy = -145, neighbours = new[] { -1 } }, // 40
			new MapTerritory { name="EasternAustralia",    mapx =  275, mapy = -160, neighbours = new[] { -1 } }  // 41
		};
	}
}
