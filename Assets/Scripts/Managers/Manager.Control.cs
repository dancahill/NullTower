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
				Transform clicked = WhatClicked(); // <- don't call WhatClicked() twice
				if (clicked)
				{ //if not null do some stuff with it
					IClickable clickable = clicked.GetComponent<IClickable>();
					if (clickable != null) clickable.ClickAction(); //call the implementation of this interface
					else throw new System.Exception("'" + clicked.name + "' is not IClickable!"); // <- tell me what's causing the error
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
