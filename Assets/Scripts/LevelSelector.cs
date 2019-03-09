using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
	public SceneFader fader;
	public Button[] levelButtons;

	private void Awake()
	{
		AppGlobals.Start();
	}

	void Start()
	{
		int levelReached = PlayerPrefs.GetInt("levelReached", 1);

		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i < levelReached) levelButtons[i].interactable = true;
			else levelButtons[i].interactable = false;
		}
	}

	public void Select(string levelName)
	{
		fader.FadeTo(levelName);
	}
}
