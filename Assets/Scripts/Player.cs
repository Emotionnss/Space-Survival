using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject bullet;
	[Tooltip("Configures the speed of the player movement.")]
	public float speed;
	private float nextFire;

	void Update()
	{
		/*
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary || (Input.GetTouch(0)).phase == TouchPhase.Moved)
            {
                Vector3 touchPosition;
                touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10));
                transform.position = Vector3.Lerp(transform.position, touchPosition, speed * Time.deltaTime);

                if (Time.time > nextFire)
                {
                    Shooting(0.15f);
                }
            }
        } */

		Dragging();
	}

	void Shooting(float fireRate) //Shooting controls
	{
		nextFire = Time.time + fireRate;
		Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
	}

	void Dragging() //Movement controls
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
			transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);

			if (Time.time > nextFire)
			{
				Shooting(0.15f);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) //If shot by enemy, minus health
	{
		if (col.gameObject.tag == "Enemy")
		{
			Destroy(col.gameObject);
			GameManager.lives--;
		}
	}
}
