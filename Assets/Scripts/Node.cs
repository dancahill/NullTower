﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	BuildManager buildManager;
	public Color hoverColor = Color.blue;
	public Color notEnoughMoneyColor = Color.red;
	public Vector3 positionOffset;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		buildManager = BuildManager.instance;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		if (turret != null)
		{
			buildManager.SelectNodeToUpgrade(this);
			return;
		} else {
			buildManager.SelectNodeToBuild(this);
		}
		//if (!buildManager.CanBuild) return;
		//BuildTurret(buildManager.GetTurretToBuild());
	}

	public void BuildTurret(TurretBlueprint blueprint)
	{
		if (PlayerStats.Money < blueprint.cost)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}
		PlayerStats.Money -= blueprint.cost;
		GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;
		turretBlueprint = blueprint;
		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
		//Debug.Log("Turret Built!");
	}

	public void UpgradeTurret()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}
		PlayerStats.Money -= turretBlueprint.upgradeCost;
		Destroy(turret);
		GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;
		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
		isUpgraded = true;
		//Debug.Log("Turret upgraded!");
	}

	public void SellTurret()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount();
		GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(turret);
		turretBlueprint = null;
	}

	private void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		//if (!buildManager.CanBuild) return;
		//if (buildManager.HasMoney)
		//{
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
