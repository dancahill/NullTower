using UnityEngine;

public class CameraController : MonoBehaviour
{
	//private bool doMovement = true;
	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 60f;
	private Vector3 dragOrigin;

	float m_DragSpeed = 2000f;

	void Update()
	{
		if (BattleManager.GameIsOver)
		{
			this.enabled = false;
			return;
		}
		//if (Application.platform == RuntimePlatform.WindowsEditor)
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.F))
		{
			Time.timeScale = Time.timeScale == 4 ? 1 : 4;
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			BattleManager.instance.stats.Money += 100;
		}
#endif
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if (Input.GetKeyDown(KeyCode.M))
				GameManager.instance.Settings.PlayMusic = !GameManager.instance.Settings.PlayMusic;
			if (Input.GetKeyDown(KeyCode.S))
				GameManager.instance.Settings.PlaySound = !GameManager.instance.Settings.PlaySound;
		}
		else
		{
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
				if (Input.GetKey(KeyCode.S)) Debug.Log("like i said, the S key is already in use - try F11");
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
			}
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
			}
		}
		FingerDrag();
		if (Application.platform == RuntimePlatform.Android)
		{
			PinchZoom();
		}
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Vector3 pos = transform.position;
		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);
		pos.x = Mathf.Clamp(pos.x, 0, 75);
		pos.z = Mathf.Clamp(pos.z, -75, -5);
		//print("x=" + pos.x.ToString() + ",z=" + pos.z.ToString());
		transform.position = pos;
	}

	void FingerDrag()
	{
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			return;
		}
		if (!Input.GetMouseButton(0)) return;
		Vector3 p1 = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		float x = p1.x * m_DragSpeed;
		float y = p1.y * m_DragSpeed;
		dragOrigin = Input.mousePosition;
		Vector3 pos = transform.position;
		pos.x -= x * Time.deltaTime;
		pos.z -= y * Time.deltaTime;
		pos.x = Mathf.Clamp(pos.x, 0, 75);
		pos.z = Mathf.Clamp(pos.z, -80, -5);
		transform.position = pos;
	}

	void PinchZoom()
	{
		float zoomspeed = 0.5f;

		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			Vector3 pos = transform.position;
			pos.y += deltaMagnitudeDiff * zoomspeed * scrollSpeed * Time.deltaTime;
			pos.y = Mathf.Clamp(pos.y, minY, maxY);
			pos.x = Mathf.Clamp(pos.x, 0, 75);
			pos.z = Mathf.Clamp(pos.z, -80, -5);
			transform.position = pos;
		}
	}
}
