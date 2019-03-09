using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
	public enum Type
	{
		Basic,
		Missile,
		Laser
	};
	public Type m_TurretType;

	public GameObject node;

	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]
	public float range = 15f;

	public float startHealth = 200;
	public float health;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Laser")]
	public bool useLaser = false;
	public int damageOverTime = 30;
	public float slowAmount = .5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

	public GameObject deathEffect;

	public Image healthBar;

	void Start()
	{
		health = startHealth;
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		// if we already have a valid target, keep firing at it
		if (target != null && Vector3.Distance(transform.position, target.transform.position) <= range)
		{
			return;
		}
		// else look for a new target
		// different turrets might prefer different targets in a future version
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}
		else
		{
			target = null;
		}
	}

	void Update()
	{
		if (target == null)
		{
			if (useLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}
			return;
		}
		LockOnTarget();
		if (useLaser)
		{
			Laser();
		}
		else
		{
			if (fireCountdown <= 0)
			{
				Shoot();
				fireCountdown = 1 / fireRate;
			}
			fireCountdown -= Time.deltaTime;
		}
	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	void Laser()
	{
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Slow(slowAmount);

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}
		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;
		impactEffect.transform.position = target.position + dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	void Shoot()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		if (bullet == null) return;
		bullet.Seek(target, m_TurretType);
		if (Manager.manager.PlaySound)
		{
			AudioSource audio = gameObject.AddComponent<AudioSource>();
			AudioClip clip;
			switch (m_TurretType)
			{
				case Type.Missile: clip = (AudioClip)Resources.Load("Sounds/FuturisticWeaponsSet/bazooka/shot_bazooka"); break;
				case Type.Laser: clip = (AudioClip)Resources.Load("Sounds/FuturisticWeaponsSet/machine)gun/shot_machinegun 1"); break;
				default: clip = (AudioClip)Resources.Load("Sounds/FuturisticWeaponsSet/hand_gun/shot_hand_gun"); break;
			}
			if (clip != null)
				audio.PlayOneShot(clip);
			else
				Debug.Log("missing shot sound for " + m_TurretType);
		}
		// just to test the damage while there's no enemy that actually does damage
		//TakeDamage(2.5f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		healthBar.fillAmount = health / startHealth;
		//if (health <= 0 && !isDead) Die();
		if (health <= 0) Die();
	}

	void Die()
	{
		//isDead = true;
		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(gameObject);
	}
}
