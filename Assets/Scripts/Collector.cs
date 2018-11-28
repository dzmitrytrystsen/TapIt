using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private GameObject[] backgrounds;
    private GameObject[] grounds;
    private GameObject cameraCollider;

    private float lastBackgroundX;
    private float lastGroundX;
    private float lastCameraColliderX;

	void Awake ()
	{
	    backgrounds = GameObject.FindGameObjectsWithTag("Background");
	    grounds = GameObject.FindGameObjectsWithTag("Ground");
	    cameraCollider = GameObject.FindGameObjectWithTag("CameraCollider");

	    lastBackgroundX = backgrounds[0].transform.position.x;
	    lastGroundX = grounds[0].transform.position.x;
	    lastCameraColliderX = cameraCollider.transform.position.x;

	    for (int i = 1; i < backgrounds.Length; i++)
	    {
	        if (lastBackgroundX < backgrounds[i].transform.position.x)
	        {
	            lastBackgroundX = backgrounds[i].transform.position.x;
	        }
	    }

	    for (int i = 1; i < grounds.Length; i++)
	    {
	        if (lastGroundX < grounds[i].transform.position.x)
	        {
	            lastGroundX = grounds[i].transform.position.x;
	        }
	    }

	    if (lastCameraColliderX < cameraCollider.transform.position.x)
	    {
	        lastCameraColliderX = cameraCollider.transform.position.x;
	    }
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D) target).size.x;

            temp.x = lastBackgroundX + width;

            target.transform.position = temp;

            lastBackgroundX = temp.x;
        }

        else if (target.tag == "Ground")
        {
            Vector3 temp = target.transform.position;
            float width = ((BoxCollider2D) target).size.x;

            temp.x = lastGroundX + width;

            target.transform.position = temp;

            lastGroundX = temp.x;
        }

        else if (target.tag == "CameraCollider")
        {
            Vector3 temp = target.transform.position;
            float width = 20f;

            temp.x = lastCameraColliderX + width;

            target.transform.position = temp;

            lastCameraColliderX = temp.x;
        }
    }
}
