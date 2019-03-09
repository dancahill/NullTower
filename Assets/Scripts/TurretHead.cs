using UnityEngine;

public class TurretHead : ClickableAbstract
{
	public GameObject turret;

	public override void ClickAction()
	{
		//print("TurretHead for " + gameObject.name + " ClickAction() did stuff");
		Turret t = turret.GetComponent<Turret>();
		Node n = t.node.GetComponent<Node>();
		n.ClickAction();
	}
}
