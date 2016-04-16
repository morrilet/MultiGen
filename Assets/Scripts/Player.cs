using UnityEngine;
using System.Collections;

public class Player : Entity
{
	public float speed;

	Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		UpdateMovement ();
	}

	void UpdateMovement()
	{
		rb.velocity = new Vector2 (Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, Time.deltaTime),
			Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, Time.deltaTime));
	}
}
