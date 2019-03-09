using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

//handle data stuff

public partial class Manager : MonoBehaviour
{
	public static Manager manager;

	public bool erraseData;// debug bool to clean temp

	//crap to save
	public string playerName;
	public int score;
	public int moneyAmount;

	private bool playSound = true;
	private bool playMusic = true;
	// annoying but necessary
	public static bool PlaySound
	{
		get { return manager ? manager.playSound : true; }
		set { if (manager) manager.playSound = PlaySound; }
	}
	public static bool PlayMusic
	{
		get { return manager ? manager.playMusic : true; }
		set { if (manager) manager.playMusic = PlayMusic; }
	}

	public bool[] upgrades;
	public int[] upgradesLevel;

	public List<string> playerTerittories;
	public List<string> neutralTerittories;
	public List<string> computerTerittories;


	// save data stuff
	BinaryFormatter bf = new BinaryFormatter();
	FileStream file; //generic filestream for serialized data 
	SaveData saveData; // object type player data

	void Awake()
	{
		//AppGlobals.InitialScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		AppGlobals.Start();
		//Debug.Log("initial scene='" + AppGlobals.InitialScene + "'");

		if (manager == null)
		{ //if there is not a dataManager already in this scene
			manager = this; // the control is this object
		}
		else if (manager != this)
		{ // if the dataManager is not this
			Destroy(gameObject); //kill this game object
		}
		DontDestroyOnLoad(gameObject); // don't destroy this game object to have it persists between scenes
		NewVars(); //init data crap

		if (erraseData)
		{
			EraseData();
		}
		else
		{
			LoadData();
		}
	}

	void Update()
	{
		MouseInput();
	}

	public void LoadData()
	{
		if (File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
		{
			file = File.Open(Application.persistentDataPath + "/SaveFile.dat", FileMode.Open); // load found file from the path into the generic file stream
			saveData = (SaveData)bf.Deserialize(file); //cast whatever you found in the file as the playerdata object
			file.Close();
			SetVarsForLoading();//change the values into the local variables
		}
	}

	public void SaveData()
	{
		//string _saveFileName = Manager.manager.SetFileName();
		saveData = new SaveData(); // get a blank saveData
		SetVarsForSaving(); //set the values from memory
		file = File.Create(Application.persistentDataPath + "/SaveFile.dat"); //create the file with the specified file path (dynamic per platform)
										      //print("saving to: "+Application.persistentDataPath + "/SaveFile.dat");
										      // C:/ Users / viordan / AppData / LocalLow / NullLogic / NullTower / SaveFile.dat
		bf.Serialize(file, saveData);//serialize the object to the file we just created
		file.Close();//close <- REALLY? lol old code man. 
	}

	public void NewVars()
	{
		playerName = "";
		score = 0;
		moneyAmount = 0;
		upgrades = new bool[2];
		upgradesLevel = new int[2];

		playerTerittories = new List<string>();
		neutralTerittories = new List<string>();
		computerTerittories = new List<string>();
	}

	public void SetVarsForLoading()
	{
		print("loading vars...");

		playerName = saveData.playerName;
		score = saveData.score;
		moneyAmount = saveData.moneyAmount;

		playSound = saveData.playSound;
		playMusic = saveData.playMusic;

		upgrades = saveData.upgrades;
		upgradesLevel = saveData.upgradesLevel;

		playerTerittories = saveData.playerTerittories;
		neutralTerittories = saveData.neutralTerittories;
		computerTerittories = saveData.computerTerittories;
	}

	public void SetVarsForSaving()
	{
		print("saving vars...");

		saveData.playerName = playerName;
		saveData.score = score;
		saveData.moneyAmount = moneyAmount;

		saveData.playSound = playSound;
		saveData.playMusic = playMusic;

		saveData.upgrades = upgrades;
		saveData.upgradesLevel = upgradesLevel;

		saveData.playerTerittories = playerTerittories;
		saveData.neutralTerittories = neutralTerittories;
		saveData.computerTerittories = computerTerittories;
	}

	public void EraseData()
	{
		if (File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
		{// if the file exists
			File.Delete(Application.persistentDataPath + "/SaveFile.dat");//delete file
		}
	}

}

[Serializable]
public class SaveData
{
	public string playerName;
	public int score;
	public int moneyAmount;

	public bool playSound;
	public bool playMusic;

	public bool[] upgrades;
	public int[] upgradesLevel;

	public List<string> playerTerittories;
	public List<string> neutralTerittories;
	public List<string> computerTerittories;
}
