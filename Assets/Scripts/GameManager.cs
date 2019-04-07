using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettings
{
	public float CameraZoom = 0;
	public bool PlayMusic = true;
	public bool PlaySound = true;
}

[Serializable]
public class GameData
{
	public string playerName;
	public List<Territory> territories;

	public GameData()
	{
		playerName = "Player";
		territories = new List<Territory>();
	}
}

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager instance;
	#endregion
	public GameSettings Settings;
	public GameData Game;
	[HideInInspector] public SoundManager soundManager;
	[HideInInspector] public SceneController sceneController;
	public string Territory;

	private void Awake()
	{
		instance = this;
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Persistent")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Persistent");
			return;
		}
		Settings = new GameSettings();
		GameSave.LoadSettings();
		Game = new GameData();
		sceneController = FindObjectOfType<SceneController>();
		if (!sceneController) throw new UnityException("Scene Controller missing. Make sure it exists in the Persistent scene.");
		if (sceneController.CurrentScene == "") sceneController.CurrentScene = "MainMenu";
	}

	private void Start()
	{
		soundManager = gameObject.AddComponent<SoundManager>();
		soundManager.PlayMusic();
	}
}
