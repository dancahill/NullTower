using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public string levelToLoad;
	public SceneFader sceneFader;

	// just testing for now
	public GameObject faderObject;
	public FaderTest faderTest;

	private void Awake()
	{
		AppGlobals.Start();
		faderObject = new GameObject("FaderThing");
		faderTest = faderObject.AddComponent<FaderTest>();
	}

	public void Play()
	{
		//sceneFader.FadeTo(levelToLoad);
		faderTest.FadeTo(levelToLoad);
	}

	public void Quit()
	{
		Debug.Log("App Quit");
		Application.Quit();
	}
}
