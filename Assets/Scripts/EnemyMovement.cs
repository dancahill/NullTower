using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	private void Start()
	{
		enemy = GetComponent<Enemy>();
		target = Waypoints.points[0];
	}

	private void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
		enemy.speed = enemy.startSpeed;
	}

	private void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}
		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

	void EndPath()
	{
		if (PlayerStats.Lives > 0) PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}
}
