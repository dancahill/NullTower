using UnityEngine;

//handle control stuff

public partial class Manager : MonoBehaviour
{
	RaycastHit hit;

	public void MouseInput()
	{
		if (Input.GetMouseButtonUp(0))
		{
			try
			{
				if (WhatClicked())
				{ //if not null do some stuff with it
					WhatClicked().GetComponent<IClickable>().ClickAction(); //call the implementation of this interface
				}
			}
			catch (System.Exception ex)
			{
				AudioSource audio = gameObject.AddComponent<AudioSource>();
				AudioClip clip = (AudioClip)Resources.Load("Sounds/Warcraft/Human/Hpissed2");
				if (clip != null) audio.PlayOneShot(clip);
				Debug.LogWarning(ex.ToString());
			}
		}
	}

	Transform WhatClicked()
	{ // return the transform of what I clicked on
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// cast a ray from the camera through the click
		if (Physics.Raycast(ray, out hit, 2000))
		{ // if you hit something
			Debug.DrawLine(ray.origin, hit.point);// draws the ray line for debug in editor
			return hit.transform; // return the transform 
		}
		else return null;
	}
}
