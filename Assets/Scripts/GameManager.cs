using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
	public float CameraZoom = 0;
	public bool PlayMusic = true;
	public bool PlaySound = true;
}

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager instance;
	#endregion
	public GameSettings Settings;
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
