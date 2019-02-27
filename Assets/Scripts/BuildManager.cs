using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;
	// change to push
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

	//private TurretBlueprint turretToBuild;
	private Node selectedNode;
	public NodeUI nodeUI;

	//public bool CanBuild { get { return turretToBuild != null; } }

	//public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public void SelectNodeToBuild(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}
		selectedNode = node;
		//turretToBuild = null;
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
		//turretToBuild = null;
		nodeUI.SetUpgradeTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTurretToBuild(TurretBlueprint turret)
	{
		//turretToBuild = turret;
		DeselectNode();
	}

	//public TurretBlueprint GetTurretToBuild()
	//{
	//	return turretToBuild;
	//}
}
