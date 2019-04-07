using UnityEngine;
using UnityEngine.UI;

public class TerritoryFlag : MonoBehaviour
{
	public Canvas canvas;
	public Text text;
	public Territory territory;

	void Start()
	{
		FixRotation();
		//text.text = territory.name + "\n" + territory.highScore + "/" + 3;
		text.text = "";
		if (territory.highScore == 3)
		{
			text.text += "***";
			text.color = Color.green;
		}
		else if (territory.highScore == 2)
		{
			text.text += "**";
			text.color = Color.yellow;
		}
		else if (territory.highScore == 1)
		{
			text.text += "*";
			text.color = Color.red;
		}
		else
		{
			canvas.gameObject.SetActive(false);
		}
	}

	void FixRotation()
	{
		Transform cam = Camera.main.transform;
		canvas.transform.LookAt(canvas.transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
	}
}
