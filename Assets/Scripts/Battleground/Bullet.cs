﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Turret.Type m_BulletType;

	private Transform target;
	public GameObject impactEffect;
	public float speed = 70.0f;
	public int damage = 50;
	public float explosionRadius = 0;

	public void Seek(Transform _target, Turret.Type type)
	{
		target = _target;
		m_BulletType = type;
	}

	void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;
		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}
		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	void HitTarget()
	{
		GameObject effects = GameObject.Find("Effects");
		if (!effects) effects = new GameObject("Effects");
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation, effects.transform);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0)
		{
			Explode();
		}
		else
		{
			Damage(target);
		}

		Destroy(gameObject);
	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy" || collider.tag == "EnemyAir")
			{
				Damage(collider.transform);
			}
		}
		GameManager.instance.soundManager.PlaySound("FuturisticWeaponsSet/bazooka/explosion_bazooka");
	}

	void Damage(Transform enemy)
	{
		Attacker e = enemy.GetComponent<Attacker>();

		// test for rockets hitting gunships
		if (e == null) e = enemy.parent.GetComponent<Attacker>();

		if (e != null)
		{
			e.TakeDamage(damage);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
