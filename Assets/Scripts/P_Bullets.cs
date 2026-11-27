using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Bullets : MonoBehaviour
{
	public float speed;

	void Update()
	{
		transform.position += new Vector3(0 + Mathf.Sin(Time.deltaTime), 5) * speed * Time.deltaTime; //Moves the bullet upwards

		if (GetComponent<Renderer>().isVisible == false) //Destroyed the bullet if out of screen
		{
			DestroyObject(gameObject);
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Contains("Boundary"))
		{
			Destroy(gameObject);
		}
	}
	*/
}
