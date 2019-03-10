using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[HideInInspector]
	GameObject faderObject;
	[HideInInspector]
	FaderTest sceneFader;

	private void Awake()
	{
		AppGlobals.Start();
		faderObject = new GameObject("FaderThing");
		sceneFader = faderObject.AddComponent<FaderTest>();
	}

	public void Play()
	{
		sceneFader.FadeTo("RiskMap");
	}

	public void Quit()
	{
		Debug.Log("App Quit");
		Application.Quit();
	}
}
