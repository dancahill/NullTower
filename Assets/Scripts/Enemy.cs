﻿using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public enum Type
	{
		None,
		Basic,
		Fast,
		Tough,
		Jeep,
		Tank
	};
	public float startSpeed = 10f;
	public float startHealth = 100f;

	[HideInInspector]
	public float speed;
	private float health;
	public int worth = 50;

	public float range = 15f;
	public float turnSpeed = 60f;

	public float fireRate = 1f;
	private float fireCountdown = 0f;

	public GameObject deathEffect;
	[Header("Unity Stuff")]

	public Transform partToRotate;
	private Transform target;
	private Turret targetTurret;
	public string turretTag = "Turret";


	public GameObject bulletPrefab;
	public Transform firePoint;

	public Image healthBar;

	private bool isDead = false;

	void Start()
	{
		speed = startSpeed;
		health = startHealth;
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}


	private void Update()
	{
		if (target == null)
		{
			return;
		}
		LockOnTarget();
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
		if (target == null) return;
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		//Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, 1).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	void Shoot()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		TankBullet bullet = bulletGO.GetComponent<TankBullet>();
		if (bullet == null) return;
		bullet.Seek(target);
		if (Manager.manager.playSound)
		{
			AudioSource audio = gameObject.AddComponent<AudioSource>();
			AudioClip clip = (AudioClip)Resources.Load("Sounds/FuturisticWeaponsSet/hand_gun/shot_hand_gun");
			if (clip != null)
				audio.PlayOneShot(clip);
			else
				Debug.Log("missing shot sound for tank");
		}
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
		PlayerStats.Money += worth;
		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		WaveSpawner.EnemiesAlive--;
		if (WaveSpawner.EnemiesAlive < 1)
		{
			PlayerStats.Rounds++;
		}
		Destroy(gameObject);
	}
}
