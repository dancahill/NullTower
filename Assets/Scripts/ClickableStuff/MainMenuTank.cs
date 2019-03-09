using UnityEngine;

public class MainMenuTank : ClickableAbstract
{
	public SceneFader sceneFader;

	public override void ClickAction()
	{
		Debug.Log("App Quit");
		Application.Quit();
	}
}
