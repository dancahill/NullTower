using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

//handle data stuff

public partial class SaveDataManager : MonoBehaviour
{
	public static SaveDataManager manager;

	public bool erraseData;// debug bool to clean temp

	//crap to save
	public int score;
	public int moneyAmount;

	public bool[] upgrades;
	public int[] upgradesLevel;

	public List<string> playerTerittories;
	public List<string> neutralTerittories;
	public List<string> computerTerittories;

	//settings data
	public string playerName;

	// should be safe now that manager should always be valid
	public bool PlaySound;
	public bool PlayMusic;

	// save data stuff
	BinaryFormatter bf = new BinaryFormatter();

	void Awake()
	{
		//AppGlobals.Start();
		if (manager == null)
		{ //if there is not a dataManager already in this scene
			manager = this; // the control is this object
		}
		else if (manager != this)
		{ // if the dataManager is not this
			Destroy(gameObject); //kill this game object
		}
		//DontDestroyOnLoad(gameObject); // don't destroy this game object to have it persists between scenes
		NewVars<SaveData>(); //init data crap
		NewVars<SettingsData>();

		if (erraseData)
		{
			EraseData<SaveData>();
			EraseData<SettingsData>();
		}
		else
		{
			LoadData<SaveData>();
			LoadData<SettingsData>();
		}
	}

	void Update()
	{
		//MouseInput();
		if (Input.GetKeyDown(KeyCode.F11))
		{
			SaveData<SaveData>();
		}
		if (Input.GetKeyDown(KeyCode.T)) SaveData<SaveData>();
		if (Input.GetKeyDown(KeyCode.Y)) LoadData<SaveData>();
		if (Input.GetKeyDown(KeyCode.U)) EraseData<SaveData>();
		if (Input.GetKeyDown(KeyCode.G)) SaveData<SettingsData>();
		if (Input.GetKeyDown(KeyCode.H)) LoadData<SettingsData>();
		if (Input.GetKeyDown(KeyCode.J)) EraseData<SettingsData>();
	}

	public void LoadData<T>() //trying to pass T as type
	{
		if (typeof(T) == typeof(SaveData))
		{
			print("load SaveData");
			SaveData t = DeserializeByType<SaveData>();

			score = t.score;
			moneyAmount = t.moneyAmount;
			upgrades = t.upgrades;
			upgradesLevel = t.upgradesLevel;
			playerTerittories = t.playerTerittories;
			neutralTerittories = t.neutralTerittories;
			computerTerittories = t.computerTerittories;

		}
		else if (typeof(T) == typeof(SettingsData))
		{
			print("load SettingsData");
			SettingsData t = DeserializeByType<SettingsData>();

			playerName = t.playerName;
			PlaySound = t.playSound;
			PlayMusic = t.playMusic;
		}
	}

	public void SaveData<T>()
	{
		if (typeof(T) == typeof(SaveData))
		{
			print("save SaveData");

			SaveData t = new SaveData();
			t.score = score;
			t.moneyAmount = moneyAmount;


			t.upgrades = upgrades;
			t.upgradesLevel = upgradesLevel;

			t.playerTerittories = playerTerittories;
			t.neutralTerittories = neutralTerittories;
			t.computerTerittories = computerTerittories;
			SerializeByType<T>(t);
		}
		else if (typeof(T) == typeof(SettingsData))
		{
			print("save SettingsData");
			//SettingsData t = new SettingsData();
			//t.playerName = playerName;
			//t.playSound = PlaySound;
			//t.playMusic = PlayMusic;
			SettingsData t = new SettingsData
			{
				playerName = playerName,
				playSound = PlaySound,
				playMusic = PlayMusic
			};

			SerializeByType<T>(t);
		}
	}

	public void NewVars<T>()
	{
		if (typeof(T) == typeof(SaveData))
		{
			score = 0;
			moneyAmount = 0;
			upgrades = new bool[2];
			upgradesLevel = new int[2];

			playerTerittories = new List<string>();
			neutralTerittories = new List<string>();
			computerTerittories = new List<string>();
		}
		else if (typeof(T) == typeof(SettingsData))
		{
			playerName = "";
			PlaySound = false;
			PlayMusic = false;
		}

	}

	public void EraseData<T>()
	{
		NewVars<T>();
		SaveData<T>();
		print("replaced " + typeof(T).ToString());
	}

	void SerializeByType<T>(object t)
	{
		FileStream file = File.Create(Application.persistentDataPath + "/" + typeof(T).ToString() + ".dat"); //create the file with the specified file path (dynamic per platform)
		bf.Serialize(file, t);//serialize the object to the file we just created
		file.Close();
	}

	T DeserializeByType<T>()
	{
		if (File.Exists(Application.persistentDataPath + "/" + typeof(T).ToString() + ".dat"))
		{
			FileStream file = File.Open(Application.persistentDataPath + "/" + typeof(T).ToString() + ".dat", FileMode.Open); // load found file from the path into the generic file stream
			T t = (T)bf.Deserialize(file); //cast whatever you found in the file as the playerdata object
			file.Close();
			return t;
		}
		else
		{
			NewVars<T>();
			SaveData<T>();
			FileStream file = File.Open(Application.persistentDataPath + "/" + typeof(T).ToString() + ".dat", FileMode.Open); // load found file from the path into the generic file stream
			T t = (T)bf.Deserialize(file); //cast whatever you found in the file as the playerdata object
			file.Close();
			print("couldn't find " + typeof(T).ToString() + " created new");
			return t;
		}
	}
}

[Serializable]
public class SaveData
{
	public int score;
	public int moneyAmount;

	public bool[] upgrades;
	public int[] upgradesLevel;

	public List<string> playerTerittories;
	public List<string> neutralTerittories;
	public List<string> computerTerittories;
}

[Serializable]
public class SettingsData
{
	public string playerName;
	public bool playSound;
	public bool playMusic;
}
