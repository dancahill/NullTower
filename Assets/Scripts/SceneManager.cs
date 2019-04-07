using UnityEngine;
using UnityEngine.AI;

public class SceneManager : MonoBehaviour
{
	public static SceneManager instance;
	GameManager manager;
	SceneController sceneController;

	void Awake()
	{
		instance = this;
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Persistent")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Persistent");
			return;
		}
		manager = GameManager.instance;
		sceneController = manager.sceneController;
	}

	private void Start()
	{
		string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		GameManager.instance.soundManager.PlayMusic();
		if (scene == "GameOver")
		{
			return;
		}
		if (scene != "MainMenu")
		{
		}
	}

	private void OnEnable()
	{
		if (!sceneController) return;
		sceneController.BeforeSceneUnload += Save;
		sceneController.AfterSceneLoad += Load;
	}

	private void OnDisable()
	{
		if (!sceneController) return;
		sceneController.BeforeSceneUnload -= Save;
		sceneController.AfterSceneLoad -= Load;
	}

	private void Load()
	{
		//manager.m_DungeonState.LoadState(sceneController.CurrentScene);
	}

	private void Save()
	{
		//manager.m_DungeonState.SaveState(sceneController.CurrentScene);
	}
}
