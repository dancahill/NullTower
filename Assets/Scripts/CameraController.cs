using UnityEngine;

public class CameraController : MonoBehaviour
{
	//private bool doMovement = true;
	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;

	void Update()
	{
		if (GameManager.GameIsOver)
		{
			this.enabled = false;
			return;
		}
		//if (Input.GetKeyDown(KeyCode.Escape))
		//{
		//	doMovement = !doMovement;
		//}
		//if (!doMovement) return;
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
		{
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
		{
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
		{
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Vector3 pos = transform.position;
		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		pos.x = Mathf.Clamp(pos.x, 0, 75);
		pos.z = Mathf.Clamp(pos.z, -80, -5);
		//print("x=" + pos.x.ToString() + ",z=" + pos.z.ToString());

		transform.position = pos;

		if (Application.platform == RuntimePlatform.Android)
		{
			FingerDrag();
			PinchZoom();
		}
	}

	void FingerDrag()
	{
		if (Input.touchCount == 1)
		{
			Touch touchZero = Input.GetTouch(0);
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			Vector3 pos = transform.position;
			transform.Translate(Vector3.right * touchZeroPrevPos.x * Time.deltaTime, Space.World);
			transform.Translate(Vector3.up * touchZeroPrevPos.y * Time.deltaTime, Space.World);
			pos.x = Mathf.Clamp(pos.x, 0, 75);
			pos.z = Mathf.Clamp(pos.z, -80, -5);
			transform.position = pos;
		}
	}

	void PinchZoom()
	{
		float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
							  //float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

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


			//GUILayout.Label("deltaMagnitudeDiff=" + deltaMagnitudeDiff.ToString());
			//private void OnGUI()
			//{
			//	string s = string.Format("\n\n\n\n [{0}]\n [{1}]\n [{2}]", Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
			//	GUILayout.Label(s);
			//}


			//float scroll = Input.GetAxis("Mouse ScrollWheel");
			Vector3 pos = transform.position;
			//pos.y -= deltaMagnitudeDiff * 1000 * scrollSpeed * Time.deltaTime;
			pos.y += deltaMagnitudeDiff * perspectiveZoomSpeed * scrollSpeed * Time.deltaTime;
			pos.y = Mathf.Clamp(pos.y, minY, maxY);

			pos.x = Mathf.Clamp(pos.x, 0, 75);
			pos.z = Mathf.Clamp(pos.z, -80, -5);
			//print("x=" + pos.x.ToString() + ",z=" + pos.z.ToString());

			transform.position = pos;



			// If the camera is orthographic...
			//if (camera.isOrthoGraphic)
			//{
			//	// ... change the orthographic size based on the change in distance between the touches.
			//	camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
			//	// Make sure the orthographic size never drops below zero.
			//	camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
			//}
			//else
			//{
			//	// Otherwise change the field of view based on the change in distance between the touches.
			//	camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
			//	// Clamp the field of view to make sure it's between 0 and 180.
			//	camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
			//}
		}
	}
}
