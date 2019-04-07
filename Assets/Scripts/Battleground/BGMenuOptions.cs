using UnityEngine;

public class BGMenuOptions : MonoBehaviour
{
	public GameObject pauseMenu;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		Time.timeScale = pauseMenu.activeSelf ? 0f : 1f;
	}

	public void Continue()
	{
		Time.timeScale = 1f;
		GameManager.instance.sceneController.FadeAndLoadScene("RiskMap");
	}

	public void MainMenu()
	{
		Time.timeScale = 1f;
		GameManager.instance.sceneController.FadeAndLoadScene("MainMenu");
	}

	public void MapMenu()
	{
		Time.timeScale = 1f;
		GameManager.instance.sceneController.FadeAndLoadScene("RiskMap");
	}

	public void Retry()
	{
		Time.timeScale = 1f;
		GameManager.instance.sceneController.FadeAndLoadScene(GameManager.instance.sceneController.CurrentScene, GameManager.instance.TerritoryName, GameManager.instance.attackMode);
	}
}
