﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Attacker : MonoBehaviour
{
	public enum Type
	{
		None,
		Jeep,
		Tank,
		HeavyTank,
		Buggy,
		Gunship
	};
	NavMeshAgent agent;

	public float startSpeed = 10f;
	public float startHealth = 100f;

	[HideInInspector] public float speed;
	private float health;
	public int worth = 50;

	public float range = 15f;
	public float m_TurnSpeed = 10f;

	public float fireRate = 0.5f;
	private float fireCountdown = 0f;

	public GameObject deathEffect;
	[Header("Unity Stuff")]

	public Transform partToRotate;
	private Transform target;
	private Turret targetTurret;
	public string turretTag = "Turret";


	public GameObject bulletPrefab;
	public Transform firePoint;

	public Transform canvas;

	public Image healthBar;

	private bool isDead = false;

	void Start()
	{
		canvas = transform.Find("Canvas");

		//agent = GetComponent<NavMeshAgent>();
		////agent = gameObject.AddComponent<NavMeshAgent>();
		//if (agent) agent.SetDestination(GameObject.Find("End(Clone)").transform.position);

		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(GameObject.Find("End").transform.position);

		speed = startSpeed;
		health = startHealth;
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	private void Update()
	{
		FixMeter();
		CheckEndPath();
		LockOnTarget();
		if (target == null)
		{
			return;
		}
		if (fireCountdown <= 0)
		{
			Shoot();
			fireCountdown = 1 / fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void UpdateTarget()
	{
		// quick hack, wrong, but it works
		if (partToRotate == null) return;

		// if we already have a valid target, keep firing at it
		if (target != null && Vector3.Distance(transform.position, target.transform.position) <= range)
		{
			return;
		}
		// else look for a new target
		GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestTurret = null;

		foreach (GameObject turret in turrets)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, turret.transform.position);

			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestTurret = turret;
			}
		}
		if (nearestTurret != null && shortestDistance <= range)
		{
			target = nearestTurret.transform;
			targetTurret = nearestTurret.GetComponent<Turret>();
		}
		else
		{
			target = null;
		}
	}

	void LockOnTarget()
	{
		if (partToRotate == null) return;
		// look at the target, or look forward if no target selected
		Vector3 dir = target ? target.position - transform.position : transform.forward;
		// slow down when aiming - maybe it should stop?
		agent.speed = target ? startSpeed / 4 : startSpeed;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * m_TurnSpeed).eulerAngles;
		//Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, 1).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	void Shoot()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		TankBullet bullet = bulletGO.GetComponent<TankBullet>();
		if (bullet == null) return;
		bullet.Seek(target);
		GameManager.instance.soundManager.PlaySound("FuturisticWeaponsSet/hand_gun/shot_hand_gun");
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		healthBar.fillAmount = health / startHealth;
		if (health <= 0 && !isDead) Die();
	}

	public void Slow(float amount)
	{
		speed = startSpeed * (1f - amount);
	}

	void Die()
	{
		isDead = true;
		BattleManager.instance.stats.Money += worth;
		GameObject effects = GameObject.Find("Effects");
		if (!effects) effects = new GameObject("Effects");
		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity, effects.transform);
		Destroy(effect, 5f);
		BGWaveManager.EnemiesAlive--;
		if (BGWaveManager.EnemiesAlive < 1) BattleManager.instance.stats.Rounds++;
		Destroy(gameObject);
	}

	void CheckEndPath()
	{
		if (agent.hasPath && agent.remainingDistance > 1) return;
		if (agent.remainingDistance > 1f) return;
		BGWaveManager.EnemiesAlive--;
		if (BattleManager.instance.stats.Lives > 0) BattleManager.instance.stats.Lives--;
		if (BattleManager.instance.stats.Lives > 0 && BGWaveManager.EnemiesAlive < 1) BattleManager.instance.stats.Rounds++;
		Destroy(gameObject);
	}

	void FixMeter()
	{
		Transform cam = Camera.main.transform;
		canvas.LookAt(canvas.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
	}
}