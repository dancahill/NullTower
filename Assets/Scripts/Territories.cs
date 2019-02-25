using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public struct MapPoint
{
	public float x;
	public float y;
}

[System.Serializable]
public class EnemyWave
{
	public Enemy.Type type;
	/// <summary>
	/// Rate of enemies spawned per second
	/// - used to calculate delay after enemy is spawned, not before
	/// </summary>
	public float rate;
	/// <summary>
	/// delay (in seconds) can be used to override rate
	/// </summary>
	public float delay;
	//public int count;
	//public float rate;
}

//[System.Serializable]
public class Territory
{
	public GameObject gameobject = null;
	public string name = "Unknown";
	public int startmoney = 200;
	public int startlives = 5;
	public MapPoint map;
	public int[] neighbours;
	/// <summary>
	/// nodes where turrets can be built
	/// </summary>
	public MapPoint[] turretnodes;
	/// <summary>
	/// waypoints enemies follow (in order)
	/// </summary>
	public MapPoint[] waypoints;
	public EnemyWave[][] waves;
}

public class Territories
{
	public static Territory Get(string territoryname)
	{
		// eventually change this to store all this data in a file, i.e. xml or json
		Territory t = null;
		switch (territoryname)
		{
			case "Alaska":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -305, y = 136 }, neighbours = new[] { 1, 3, 33 } }; // 0
				break;
			case "NorthWestTerritory":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -215, y = 130 }, neighbours = new[] { 0, 2, 3, 4 } }; // 1
				break;
			case "Greenland":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -82, y = 141 }, neighbours = new[] { 1, 4, 5, 13 } }; // 2
				break;
			case "Alberta":
				t = new Territory
				{
					name = territoryname,
					startlives = 2,
					startmoney = 200,
					map = { x = -234, y = 107 },
					neighbours = new[] { 0, 1, 4, 6 },
					turretnodes = new MapPoint[] {
						new MapPoint { x =  6, y =  7 },
						new MapPoint { x =  9, y =  8 },
						new MapPoint { x =  7, y = 11 }
					},
					waypoints = new MapPoint[] {
						new MapPoint { x =  1, y =  1 },
						new MapPoint { x =  6, y =  1 },
						new MapPoint { x =  6, y =  3 },
						new MapPoint { x =  3, y =  3 },
						new MapPoint { x =  3, y =  6 },
						new MapPoint { x = 10, y =  6 },
						new MapPoint { x = 10, y =  3 },
						new MapPoint { x = 12, y =  3 },
						new MapPoint { x = 12, y =  9 },
						new MapPoint { x =  5, y =  9 },
						new MapPoint { x =  5, y = 11 },
						new MapPoint { x =  3, y = 11 },
						new MapPoint { x =  3, y = 14 },
						new MapPoint { x = 14, y = 14 }
					},
					waves = new EnemyWave[][] {
						new EnemyWave[] {
							new EnemyWave { type=Enemy.Type.Basic, rate=2 },
							new EnemyWave { type=Enemy.Type.Basic, rate=2 },
							new EnemyWave { type=Enemy.Type.Basic, rate=1 }
						},
						new EnemyWave[] {
							new EnemyWave { type=Enemy.Type.Basic, rate=2 },
							new EnemyWave { type=Enemy.Type.Basic, rate=2 },
							new EnemyWave { type=Enemy.Type.Basic, rate=2 },
							new EnemyWave { type=Enemy.Type.Basic, rate=2 },
							new EnemyWave { type=Enemy.Type.Tough, rate=1 },
						},
						new EnemyWave[] {
							new EnemyWave { type=Enemy.Type.Tough, rate=1 },
							new EnemyWave { type=Enemy.Type.Tough, rate=1 },
							new EnemyWave { type=Enemy.Type.Tough, delay=6 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
							new EnemyWave { type=Enemy.Type.Fast,  delay=6 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
							new EnemyWave { type=Enemy.Type.Fast,  rate=5 },
						}
					}
				};
				return t;
			case "Ontario":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -185, y = 96 }, neighbours = new[] { 1, 2, 3, 5, 6, 7 } }; // 4
				break;
			case "Quebec":
				t = new Territory { name = territoryname, startlives = 1, startmoney = 100, map = { x = -133, y = 98 }, neighbours = new[] { 2, 4, 6 } }; // 5
				break;
			case "WesternUnitedStates":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -222, y = 60 }, neighbours = new[] { 3, 4, 7, 8 } }; // 6
				break;
			case "EasternUnitedStates":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -186, y = 49 }, neighbours = new[] { 4, 5, 6, 8 } }; // 7
				break;
			case "CentralAmerica":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -230, y = 20 }, neighbours = new[] { 6, 7, 9 } }; // 8
				break;
			// south america
			case "Venezuela":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -205, y = -42 }, neighbours = new[] { 8, 10, 11 } }; // 9
				break;
			case "Peru":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -183, y = -105 }, neighbours = new[] { 9, 11, 12 } }; // 10
				break;
			case "Brazil":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -143, y = -83 }, neighbours = new[] { 9, 10, 12, 20 } }; // 11
				break;
			case "Argentinia":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -185, y = -158 }, neighbours = new[] { 10, 11 } }; // 12
				break;
			// europe
			case "Iceland":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -57, y = 122 }, neighbours = new[] { -1 } }; // 13
				break;
			case "GreatBritain":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -48, y = 78 }, neighbours = new[] { -1 } }; // 14
				break;
			case "Scandinavia":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -9, y = 135 }, neighbours = new[] { -1 } }; // 15
				break;
			case "W.Europe":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -51, y = 30 }, neighbours = new[] { -1 } }; // 16
				break;
			case "N.Europe":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -9, y = 80 }, neighbours = new[] { -1 } }; // 17
				break;
			case "S.Europe":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 5, y = 34 }, neighbours = new[] { -1 } }; // 18
				break;
			case "Ukraine":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 45, y = 70 }, neighbours = new[] { -1 } }; // 19
				break;
			// africa
			case "NorthAfrica":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = -44, y = -36 }, neighbours = new[] { -1 } }; // 20
				break;
			case "Egypt":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 7, y = -16 }, neighbours = new[] { -1 } }; // 21
				break;
			case "EastAfrica":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 32, y = -68 }, neighbours = new[] { -1 } }; // 22
				break;
			case "Congo":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 0, y = -100 }, neighbours = new[] { -1 } }; // 23
				break;
			case "SouthAfrica":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 0, y = -159 }, neighbours = new[] { -1 } }; // 24
				break;
			case "Madagascar":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 55, y = -159 }, neighbours = new[] { -1 } }; // 25
				break;
			// asia
			case "MiddleEast":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 59, y = -2 }, neighbours = new[] { -1 } }; // 26
				break;
			case "Afghanistan":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 103, y = 48 }, neighbours = new[] { -1 } }; // 27
				break;
			case "Ural":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 121, y = 109 }, neighbours = new[] { -1 } }; // 28
				break;
			case "Siberia":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 170, y = 125 }, neighbours = new[] { -1 } }; // 29
				break;
			case "Yakursk":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 229, y = 142 }, neighbours = new[] { -1 } }; // 30
				break;
			case "Irkutsk":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 219, y = 98 }, neighbours = new[] { -1 } }; // 31
				break;
			case "Mongolia":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 236, y = 66 }, neighbours = new[] { -1 } }; // 32
				break;
			case "Kamchatka":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 310, y = 133 }, neighbours = new[] { -1 } }; // 33
				break;
			case "Japan":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 274, y = 40 }, neighbours = new[] { -1 } }; // 34
				break;
			case "China":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 184, y = 27 }, neighbours = new[] { -1 } }; // 35
				break;
			case "India":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 140, y = -19 }, neighbours = new[] { -1 } }; // 36
				break;
			case "Siam":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 191, y = -30 }, neighbours = new[] { -1 } }; // 37
				break;
			// australia
			case "Indonesia":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 208, y = -81 }, neighbours = new[] { -1 } }; // 38
				break;
			case "NewGuinea":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 282, y = -94 }, neighbours = new[] { -1 } }; // 39
				break;
			case "WesternAustralia":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 211, y = -145 }, neighbours = new[] { -1 } }; // 40
				break;
			case "EasternAustralia":
				t = new Territory { name = territoryname, startlives = 5, startmoney = 200, map = { x = 275, y = -160 }, neighbours = new[] { -1 } };  // 41
				break;
			default:
				break;
		}

		if (t != null)
		{
			if (t.turretnodes == null)
			{
				// default nodes for a blank map
				t.turretnodes = new MapPoint[] {
					new MapPoint { x =  6, y =  7 },
					new MapPoint { x =  9, y =  8 }
				};
			}
			if (t.waypoints == null)
			{
				// default waypoints for a blank map
				t.waypoints = new MapPoint[] {
					new MapPoint { x =  1, y =  1 },
					new MapPoint { x =  6, y =  1 },
					new MapPoint { x =  6, y =  3 },
					new MapPoint { x =  3, y =  3 },
					new MapPoint { x =  3, y =  6 },
					new MapPoint { x = 10, y =  6 },
					new MapPoint { x = 10, y =  3 },
					new MapPoint { x = 12, y =  3 },
					new MapPoint { x = 12, y =  9 },
					new MapPoint { x =  5, y =  9 },
					new MapPoint { x =  5, y = 11 },
					new MapPoint { x =  3, y = 11 },
					new MapPoint { x =  3, y = 14 },
					new MapPoint { x = 14, y = 14 }
				};
			}
			if (t.waves == null)
			{
				t.waves = new EnemyWave[][] {
					new EnemyWave[] {
						new EnemyWave { type=Enemy.Type.Basic, rate=1 }
					},
					new EnemyWave[] {
						new EnemyWave { type=Enemy.Type.Tough, rate=1 }
					}
				};
			}
		}
		return t;
	}
}
//}
