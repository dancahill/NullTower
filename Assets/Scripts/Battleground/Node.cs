﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	BGBuildManager buildManager;
	public Color hoverColor = Color.blue;
	public Color notEnoughMoneyColor = Color.red;
	public Vector3 positionOffset;

	[HideInInspector] public GameObject turret;
	[HideInInspector] public TurretBlueprint turretBlueprint;
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		buildManager = BGBuildManager.instance;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		SelectNode();
	}

	public void SelectNode()
	{
		if (BattleManager.instance.attackMode) return;
		if (turret != null)
		{
			buildManager.SelectNodeToUpgrade(this);
			return;
		}
		else
		{
			buildManager.SelectNodeToBuild(this);
		}
		//if (!buildManager.CanBuild) return;
		//BuildTurret(buildManager.GetTurretToBuild());
	}


	public void BuildTurret(TurretBlueprint blueprint)
	{
		if (BattleManager.instance.stats.Money < blueprint.cost)
		{
			GameToast.Add("Not enough money to build that!");
			return;
		}
		BattleManager.instance.stats.Money -= blueprint.cost;
		GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

		Turret t = _turret.GetComponent<Turret>();
		t.node = gameObject;

		turret = _turret;
		turretBlueprint = blueprint;
		GameObject effects = GameObject.Find("Effects");
		if (!effects) effects = new GameObject("Effects");
		if (!buildManager)
		{
			//Debug.Log("buildmanager is null");
			buildManager = BGBuildManager.instance;
		}
		if (!buildManager) Debug.Log("buildmanager is still null");
		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity, effects.transform);
		Destroy(effect, 5f);
		GameToast.Add("Turret Built!");
		isUpgraded = false;
	}

	public void UpgradeTurret()
	{
		if (BattleManager.instance.stats.Money < turretBlueprint.upgradeCost)
		{
			GameToast.Add("Not enough money to upgrade that!");
			return;
		}
		BattleManager.instance.stats.Money -= turretBlueprint.upgradeCost;
		Vector3 oldrotation = new Vector3(turret.transform.eulerAngles.x, turret.transform.eulerAngles.x, turret.transform.eulerAngles.z);
		Destroy(turret);
		GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		Turret t = _turret.GetComponent<Turret>();
		t.node = gameObject;

		turret.transform.eulerAngles = oldrotation;
		GameObject effects = GameObject.Find("Effects");
		if (!effects) effects = new GameObject("Effects");
		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity, effects.transform);
		Destroy(effect, 5f);
		isUpgraded = true;
		GameToast.Add("Turret upgraded!");
	}

	public void SellTurret()
	{
		BattleManager.instance.stats.Money += turretBlueprint.GetSellAmount();
		GameObject effects = GameObject.Find("Effects");
		if (!effects) effects = new GameObject("Effects");
		GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity, effects.transform);
		Destroy(effect, 5f);
		Destroy(turret);
		turretBlueprint = null;
		GameToast.Add("Turret sold!");
	}

	private void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		//if (!buildManager.CanBuild) return;
		//if (buildManager.HasMoney)
		//{
		if (rend != null)
			rend.material.color = hoverColor;
		//}
		//else
		//{
		//	rend.material.color = notEnoughMoneyColor;
		//}
	}

	private void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
