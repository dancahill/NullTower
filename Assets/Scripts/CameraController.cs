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
		if (GameManager.GameIsOver)
		{
			this.enabled = false;
			return;
		}
		if (Input.GetKeyDown("f"))
		{
			Time.timeScale = Time.timeScale == 4 ? 1 : 4;
		}
		if (Input.GetKeyDown("c"))
		{
			PlayerStats.Money += 100;
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKeyDown("m"))
				Manager.manager.playMusic = !Manager.manager.playMusic;
			if (Input.GetKeyDown("s"))
				Manager.manager.playSound = !Manager.manager.playSound;
		}
		//if (Input.GetKeyDown(KeyCode.Escape))
		//{
		//	doMovement = !doMovement;
		//}
		//if (!doMovement) return;
		//if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		//if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
		if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		//if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
		if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		//if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
		if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
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
