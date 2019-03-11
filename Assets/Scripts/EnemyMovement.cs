using UnityEngine;

//[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
	//public GameObject movementDirection;
/*
	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	private void Start()
	{
		enemy = GetComponent<Enemy>();
		if (Waypoints.points != null) target = Waypoints.points[0];
	}

	private void Update()
	{
		return;
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
		enemy.speed = enemy.startSpeed;

		if (movementDirection != null)
		{
			//movementDirection.transform.LookAt(target);
			float turnSpeed = 10f;
			Vector3 dir2 = target.position - movementDirection.transform.position;
			Quaternion lookRotation = Quaternion.LookRotation(dir2);
			Vector3 rotation = Quaternion.Lerp(movementDirection.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
			movementDirection.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
		}
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
		if (WaveSpawner.EnemiesAlive < 1)
		{
			PlayerStats.Rounds++;
		}
		Destroy(gameObject);
	}
*/
}
