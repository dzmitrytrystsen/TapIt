using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.U2D;
using UnityEngine;

public class GroundScript : MonoBehaviour
{

    private Rigidbody2D myRigidbody2D;

    [SerializeField] private float speed = 3f;

    void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

	void Update ()
	{
		myRigidbody2D.velocity = new Vector2(speed, 0) * Time.deltaTime;
	}
}
