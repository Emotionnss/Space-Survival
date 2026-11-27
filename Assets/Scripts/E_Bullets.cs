using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Bullets : MonoBehaviour
{
	public GameObject player;
	public float speed;
	private Vector3 playerPosition, curPosition, newDirection;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player"); //Player pathing

		BulletType();
	}

	void Update()
	{
		if (GetComponent<Renderer>().isVisible == false) //Destroys if out of screen
		{
			DestroyObject(gameObject);
		}

		BulletMovement();
	}

	void BulletType() //Type of bullet shot
	{
		if (player != null)
		{
			playerPosition = new Vector3(player.transform.position.x, player.transform.position.y);

			curPosition = transform.position;

			newDirection = (playerPosition - curPosition).normalized;
		}
	}

	void BulletMovement() //Type of movement
	{
		transform.position += newDirection * Time.deltaTime * speed;
	}
}
