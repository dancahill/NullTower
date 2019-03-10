using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	public GameObject buildEffect;
	public GameObject sellEffect;

	private Node selectedNode;
	public NodeUI nodeUI;

	public void SelectNodeToBuild(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}
		selectedNode = node;
		nodeUI.SetBuildTarget(node);
	}

	public void SelectNodeToUpgrade(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}
		selectedNode = node;
		nodeUI.SetUpgradeTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTurretToBuild(TurretBlueprint turret)
	{
		DeselectNode();
	}
}
