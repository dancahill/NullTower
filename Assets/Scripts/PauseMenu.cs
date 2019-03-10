using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject ui;
	//public SceneFader sceneFader;
	FaderTest sceneFader;

	private void Awake()
	{
		FaderTest[] faders = Resources.FindObjectsOfTypeAll<FaderTest>();
		if (faders.Length == 1) sceneFader = faders[0];
		// else what the fuck?
	}

	private void Update()
	{
		// use GetKeyDown, not GetKey - holding the key shouldn't toggle the menu continuously
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		ui.SetActive(!ui.activeSelf);
		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
			//Time.fixedDeltaTime
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void Retry()
	{
		//Time.timeScale = 1;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		sceneFader.FadeTo(SceneManager.GetActiveScene().name, GameManager.Territory);
	}

	public void Menu()
	{
		//Time.timeScale = 1;
		sceneFader.FadeTo("RiskMap");
	}
}
