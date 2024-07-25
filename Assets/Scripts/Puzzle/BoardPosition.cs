using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPosition : MonoBehaviour
{
    
    void Start()
    {
		Camera cam = Camera.main;
		Vector3 CamCenter = new Vector3(0.5f, 0.5f, 0);
		Vector3 worldCenter = cam.ViewportToWorldPoint(CamCenter);

		gameObject.transform.position = new Vector3(worldCenter.x - 0.137f, worldCenter.y - 0.528f, 0f);
	}

}
