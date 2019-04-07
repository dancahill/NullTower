using UnityEngine;

public class TurretHead : MonoBehaviour
{
	public GameObject turret;

	private void OnMouseUp()
	{
		Turret t = turret.GetComponent<Turret>();
		Node n = t.node.GetComponent<Node>();
		n.SelectNode();
	}
}
