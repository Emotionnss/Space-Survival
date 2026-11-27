using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Move : MonoBehaviour
{

	public GameObject E_Bullet;
	public static float speed, fireRate;
	public AudioClip[] e_sfx;

	private GameObject[] waypoints;
	private float nextFire;
	private int health = 40, curWaypoint;
	private AudioSource aud;
	private Animator anim;
	private Rigidbody2D rig;
	private Vector3 target, moveDirection;
	private bool dead = false;



	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		aud = GetComponent<AudioSource>();
		rig = GetComponent<Rigidbody2D>();
		waypoints = GameObject.FindGameObjectsWithTag("Way");
		aud.PlayOneShot(e_sfx[1]);
	}

	void Update()
	{
		if (dead == false) //If not dead, continue to move and shoot but if dead stop movement and destroy gameobject
		{
			WayMovement();

			AutoFiring();

			Destroyed();
		}
		else
		{
			rig.Sleep();
			Destroy(gameObject, anim.GetCurrentAnimatorClipInfo(0).Length);
		}
	}

	void OnTriggerEnter2D(Collider2D other) //Collision for when hit by either player's bullet or player
	{
		if (other.gameObject.tag == "P_Bullet")
		{
			health -= 10;
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Player")
		{
			health -= 50;
			GameManager.lives--;
		}
	}

	void Destroyed()
	{
		if (health <= 0) //Dead Enemy
		{
			anim.Play("Destroyed");
			aud.PlayOneShot(e_sfx[0]);
			GameManager.spawned--;
			GameManager.kills += 1;
			dead = true;
		}
	}

	void WayMovement() //Gives movement between a certain waypoint in the game
	{
		if (curWaypoint < waypoints.Length)
		{
			target = waypoints[curWaypoint].transform.position;
			moveDirection = target - transform.position;

			if (moveDirection.magnitude <= 1)
			{
				curWaypoint += Random.Range(0, waypoints.Length);
			}
			else
			{
				rig.velocity = moveDirection.normalized * speed;
			}
		}
		else
		{
			curWaypoint = 0;
		}
	}

	void AutoFiring() //Used for shooting
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			if (E_Bullet != null)
			{
				Instantiate(E_Bullet, GetComponentInChildren<Transform>().position, Quaternion.identity);
			}
		}
	}
}
