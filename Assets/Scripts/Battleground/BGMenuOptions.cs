using UnityEngine;

public class BGMenuOptions : MonoBehaviour
{
	public GameObject pauseMenu;

	private void Update()
	{
		// use GetKeyDown, not GetKey - holding the key shouldn't toggle the menu continuously
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		if (pauseMenu.activeSelf)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void Continue()
	{
		GameManager.instance.sceneController.FadeAndLoadScene("RiskMap");
	}

	public void MainMenu()
	{
		GameManager.instance.sceneController.FadeAndLoadScene("MainMenu");
	}

	public void MapMenu()
	{
		GameManager.instance.sceneController.FadeAndLoadScene("RiskMap");
	}

	public void Retry()
	{
		//GameManager.instance.sceneController.FadeAndLoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, GameManager.instance.Territory);
		GameManager.instance.sceneController.FadeAndLoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}
}
