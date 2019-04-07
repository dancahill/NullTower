using UnityEngine;

public class MainMenu : MonoBehaviour
{
	private void OnMouseDown()
	{
		//Debug.Log("mouse event: '" + name + "'");
		if (name == "Turret")
		{
			GameManager.instance.sceneController.FadeAndLoadScene("RiskMap");
		}
		else if (name == "Tank")
		{
			Application.Quit();
		}
	}
}
