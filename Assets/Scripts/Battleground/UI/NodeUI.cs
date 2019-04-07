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
	public Text repairCost;
	public Button repairButton;
	public Text sellAmount;
	public Button sellButton;
	private Node target;

	public void Update()
	{
		if (UpgradeUI.activeSelf && target)
		{
			int cost = target.RepairCost();
			repairCost.text = "$" + cost;
			repairButton.interactable = (cost > 0 && Time.time >= target.RepairCooldown);
		}
	}

	public void SetBuildTarget(Node _target)
	{
		this.target = _target;
		transform.position = target.GetBuildPosition();
		UpgradeUI.SetActive(false);
		BuildUI.SetActive(true);
		Vector3 dir = m_Camera.transform.position - BuildUI.transform.position;
		dir.x = 0;
		BuildUI.transform.rotation = Quaternion.LookRotation(dir);
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
		//UpgradeUI.transform.LookAt(transform.position - v);
		Transform t = UpgradeUI.transform;
		Camera cam = Camera.main;
		t.LookAt(t.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
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
		BGBuildManager.instance.DeselectNode();
	}

	public void Upgrade()
	{
		target.UpgradeTurret();
		BGBuildManager.instance.DeselectNode();
	}

	public void Repair()
	{
		if (Time.time < target.RepairCooldown) return;
		target.RepairTurret();
		BGBuildManager.instance.DeselectNode();
	}

	public void Sell()
	{
		target.SellTurret();
		BGBuildManager.instance.DeselectNode();
	}
}
