﻿using System;
using System.Xml;
using UnityEngine;

[Serializable]
public struct MapPoint
{
	public float x;
	public float y;
}

[Serializable]
public class EnemyWave
{
	public Attacker.Type type;
	/// <summary>
	/// number of enemies to spawn (1 if not provided)
	/// </summary>
	public int count = 1;
	/// <summary>
	/// Rate of enemies spawned per second
	/// - used to calculate delay after enemy is spawned, not before
	/// </summary>
	public float rate = 1;
	/// <summary>
	/// delay (in seconds) between this spawn and the next
	/// - can be used to override rate
	/// </summary>
	public float delay = 0;
}

[Serializable]
public class Territory
{
	public enum Ownership
	{
		Neutral,
		Player,
		AI
	}
	public string name = "Unknown";
	public Ownership ownership = Ownership.Neutral;
	public int highScore = 0;
	// other stuff for making the game work
	[NonSerialized] public int startMoney = 200;
	[NonSerialized] public int startLives = 5;
	[NonSerialized] public GameObject gameobject = null;
	/// <summary>
	/// x,y coordinates of territory's icon on world map
	/// </summary>
	[NonSerialized] public MapPoint map;
	/// <summary>
	/// array of adjacent territories attackable from this one
	/// </summary>
	[NonSerialized] public int[] neighbours;
	/// <summary>
	/// nodes where turrets can be built
	/// </summary>
	[NonSerialized] public MapPoint[] turretnodes;
	/// <summary>
	/// waypoints enemies follow (in order)
	/// </summary>
	[NonSerialized] public MapPoint[] waypoints;
	/// <summary>
	/// array of information about attacking waves and units
	/// </summary>
	[NonSerialized] public EnemyWave[][] waves;
}

public class Territories
{
	static XmlDocument mapxml = null;

	private static bool LoadXML()
	{
		if (mapxml != null) return false;
		TextAsset asset = (TextAsset)Resources.Load("Data/RiskMapData", typeof(TextAsset));
		if (asset == null)
		{
			Debug.Log("RiskMapData asset is null");
			return false;
		}
		mapxml = new XmlDocument();
		mapxml.LoadXml(asset.text);
		return true;
	}

	public static Territory Get(string territoryname)
	{
		LoadXML();
		XmlNode xmlterritory = mapxml.DocumentElement.SelectSingleNode("/Territories/Territory[@name='" + territoryname + "']");
		if (xmlterritory == null)
		{
			Debug.Log(string.Format("Territory {0} is missing in XML - trying 'Default'", territoryname));
			xmlterritory = mapxml.DocumentElement.SelectSingleNode("/Territories/Territory[@name='Default']");
		}
		if (xmlterritory == null)
		{
			return null;
		}
		Territory t = new Territory();
		t.name = territoryname;
		t.startLives = int.Parse(xmlterritory.Attributes["startlives"].Value);
		t.startMoney = int.Parse(xmlterritory.Attributes["startmoney"].Value);
		string[] mapxy = xmlterritory.Attributes["mapxy"].Value.Split(',');
		t.map.x = int.Parse(mapxy[0]);
		t.map.y = int.Parse(mapxy[1]);
		string[] neighbours = xmlterritory.Attributes["neighbours"].Value.Split(',');
		t.neighbours = new int[neighbours.Length];
		int i = 0;
		for (i = 0; i < neighbours.Length; i++)
		{
			t.neighbours[i] = int.Parse(neighbours[i]);
		}
		ReadTurretNodes(xmlterritory.SelectNodes("TurretNodes/Location"), t);
		ReadWaypoints(xmlterritory.SelectNodes("Waypoints/Location"), t);
		ReadEnemyWaves(xmlterritory.SelectNodes("Waves/Wave"), t);
		// we wouldn't need this if the xml file was properly filled out
		Validate(t);
		return t;
	}

	static void ReadTurretNodes(XmlNodeList turretnodes, Territory t)
	{
		t.turretnodes = new MapPoint[turretnodes.Count];
		int i = 0;
		foreach (XmlNode xn in turretnodes)
		{
			t.turretnodes[i].x = int.Parse(xn.Attributes["x"].Value);
			t.turretnodes[i].y = int.Parse(xn.Attributes["y"].Value);
			i++;
		}
	}

	static void ReadWaypoints(XmlNodeList waypoints, Territory t)
	{
		t.waypoints = new MapPoint[waypoints.Count];
		int i = 0;
		foreach (XmlNode xn in waypoints)
		{
			t.waypoints[i].x = int.Parse(xn.Attributes["x"].Value);
			t.waypoints[i].y = int.Parse(xn.Attributes["y"].Value);
			i++;
		}
	}

	static void ReadEnemyWaves(XmlNodeList waves, Territory t)
	{
		t.waves = new EnemyWave[waves.Count][];
		int i = 0;
		foreach (XmlNode wave in waves)
		{
			XmlNodeList enemies = wave.SelectNodes("Enemy");
			t.waves[i] = new EnemyWave[enemies.Count];
			int j = 0;
			foreach (XmlNode enemy in enemies)
			{
				t.waves[i][j] = new EnemyWave { };
				Attacker.Type type;
				string enemytype = enemy.Attributes["type"] != null ? enemy.Attributes["type"].Value : "";
				switch (enemytype)
				{
					case "Jeep": type = Attacker.Type.Jeep; break;
					case "Tank": type = Attacker.Type.Tank; break;
					case "HeavyTank": type = Attacker.Type.HeavyTank; break;
					case "Buggy": type = Attacker.Type.Buggy; break;
					case "Gunship": type = Attacker.Type.Gunship; break;
					default: type = Attacker.Type.None; break;
				}
				t.waves[i][j].type = type;
				XmlAttribute xacount = enemy.Attributes["count"];
				XmlAttribute xarate = enemy.Attributes["rate"];
				XmlAttribute xadelay = enemy.Attributes["delay"];
				if (xacount != null) t.waves[i][j].count = int.Parse(xacount.Value);
				if (xadelay != null) t.waves[i][j].delay = float.Parse(xadelay.Value);
				if (xarate != null) t.waves[i][j].rate = float.Parse(xarate.Value);
				j++;
			}
			i++;
		}
	}

	static void Validate(Territory t)
	{
		if (t.waypoints.Length == 0)
		{
			t.waypoints = new MapPoint[2] {
				new MapPoint { x = 1, y = 1 },
				new MapPoint { x = 14, y = 14 }
			};
		}
		if (t.waves.Length == 0)
		{
			t.waves = new EnemyWave[1][] {
				new EnemyWave[1] {
					new EnemyWave{ type=Attacker.Type.Tank, count=1 }
				}
			};
			t.startLives = 1;
			t.startMoney = 100;
		}
	}
}
