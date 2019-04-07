using UnityEngine;
using UnityEngine.UI;

public class TerritoryFlag : MonoBehaviour
{
	public Canvas canvas;
	public Text text;
	public Territory territory;

	void Update()
	{
		FixRotation();
		//text.text = territory.name + "\n" + territory.highScore + "/" + 3;
		text.text = "***".Substring(0, territory.highScore) + "---".Substring(territory.highScore, 3 - territory.highScore);
		if (territory.ownership == Territory.Ownership.Player)
			text.color = Color.green;
		else if (territory.ownership == Territory.Ownership.AI)
			text.color = Color.red;
		else
			text.color = Color.yellow;
	}

	void FixRotation()
	{
		Transform cam = Camera.main.transform;
		canvas.transform.LookAt(canvas.transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
	}
}
