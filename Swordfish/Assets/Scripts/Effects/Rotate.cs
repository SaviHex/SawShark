using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float speed = 250;

	private Transform t;

	void Start () 
	{
        t = GetComponent<Transform>();
	}
		
	void FixedUpdate () 
	{        
        t.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime));
	}
}
