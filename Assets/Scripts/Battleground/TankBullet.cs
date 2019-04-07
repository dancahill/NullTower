using UnityEngine;

public class TankBullet : MonoBehaviour
{
	private Transform target;
	public GameObject impactEffect;
	public float speed = 70.0f;
	public int damage;

	public void Seek(Transform _target)
	{
		target = _target;
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
		Damage(target);
		Destroy(gameObject);
	}

	void Damage(Transform enemy)
	{
		Turret e = enemy.GetComponent<Turret>();
		if (e != null)
		{
			e.TakeDamage(damage);
		}
	}
}
