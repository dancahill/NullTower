using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
	public GameObject BuildUI;
	public GameObject UpgradeUI;
	public GameObject m_Camera;

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;

	public Text upgradeCost;
	public Button upgradeButton;
	public Text sellAmount;
	public Button sellButton;
	private Node target;

	public void SetBuildTarget(Node _target)
	{
		this.target = _target;
		transform.position = target.GetBuildPosition();
		UpgradeUI.SetActive(false);
		BuildUI.SetActive(true);
		Vector3 dir = m_Camera.transform.position - UpgradeUI.transform.position;
		//dir.x = 0;
		//BuildUI.transform.LookAt(dir);
		BuildUI.transform.LookAt(m_Camera.transform);
	}

	public void SetUpgradeTarget(Node _target)
	{
		this.target = _target;
		transform.position = target.GetBuildPosition();
		if (!target.isUpgraded)
		{
			upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeCost.text = "MAX";
			upgradeButton.interactable = false;
		}
		sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
		BuildUI.SetActive(false);
		UpgradeUI.SetActive(true);
		Vector3 v = m_Camera.transform.position - UpgradeUI.transform.position;
		v.x = 0;
		//UpgradeUI.transform.LookAt(m_Camera.transform);
		UpgradeUI.transform.LookAt(transform.position - v);
	}

	public void Hide()
	{
		BuildUI.SetActive(false);
		UpgradeUI.SetActive(false);
	}

	public void Build(string TurretType)
	{
		switch (TurretType)
		{
			case "StandardTurret": target.BuildTurret(standardTurret); break;
			case "MissileLauncher": target.BuildTurret(missileLauncher); break;
			case "LaserBeamer": target.BuildTurret(laserBeamer); break;
			default: Debug.Log("turret type " + TurretType + " not known"); break;
		}
		BuildManager.instance.DeselectNode();
	}

	public void Upgrade()
	{
		target.UpgradeTurret();
		BuildManager.instance.DeselectNode();
	}

	public void Sell()
	{
		target.SellTurret();
		BuildManager.instance.DeselectNode();
	}
}
