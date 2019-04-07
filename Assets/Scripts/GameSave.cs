using System.IO;
using UnityEngine;

public class GameSave 
{
	public static void LoadGame()
	{
		string filepath = Application.persistentDataPath + "/game1.json";
		Debug.Log("reading " + filepath);
		try
		{
			string json = File.ReadAllText(filepath);
			JsonUtility.FromJsonOverwrite(json, GameManager.instance.Game);
		}
		catch (FileNotFoundException)
		{
			Debug.Log("saved game doesn't exist yet. i'll make one");
			SaveGame();
		}
	}

	public static void SaveGame()
	{
		string filepath = Application.persistentDataPath + "/game1.json";
		Debug.Log("writing " + filepath);
		File.WriteAllText(filepath, JsonUtility.ToJson(GameManager.instance.Game, true));
	}

	public static void LoadSettings()
	{
		string filepath = Application.persistentDataPath + "/settings.json";
		Debug.Log("reading " + filepath);
		GameSettings s = GameManager.instance.Settings;
		try
		{
			string json = File.ReadAllText(filepath);
			JsonUtility.FromJsonOverwrite(json, s);
		}
		catch (FileNotFoundException)
		{
			Debug.Log("settings file doesn't exist yet. i'll make one");
			SaveSettings();
		}
	}

	public static void SaveSettings()
	{
		string filepath = Application.persistentDataPath + "/settings.json";
		Debug.Log("writing " + filepath);
		GameSettings s = GameManager.instance.Settings;
		File.WriteAllText(filepath, JsonUtility.ToJson(s, true));
	}
}
