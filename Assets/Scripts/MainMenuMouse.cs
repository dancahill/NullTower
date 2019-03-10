using UnityEngine;

public class MainMenuMouse : MonoBehaviour, IClickable
{
	public MainMenu m_MainMenu;

	public void ClickAction()
	{
		//print("" + name + " ClickAction() ...");
		if (name == "Turret") m_MainMenu.Play();
		else if (name == "Tank") m_MainMenu.Quit();
	}
}
