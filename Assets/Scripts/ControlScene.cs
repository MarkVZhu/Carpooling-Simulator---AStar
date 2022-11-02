using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScene : MonoBehaviour
{
	public float  moveSpeed = 1000f;
	public float viewSpeed = 8f;

	// Update is called once per frame
	void Update()
	{
		// Zoom in and out
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > viewSpeed) 
		{
			GetComponent<Camera>().fieldOfView -= viewSpeed;
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			GetComponent<Camera>().fieldOfView += viewSpeed;
		}

		// Moving camera
		if (Input.GetMouseButton(1))
		{ 
			float h = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
			float v = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
			this.transform.Translate(-h, -v, 0, Space.World);
		}  
	}
}