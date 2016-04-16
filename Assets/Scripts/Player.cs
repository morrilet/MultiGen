using UnityEngine;
using System.Collections;

public class Player : Entity
{
	public float speed;

	Vector3 playerVelocity;

	void Start ()
	{
	
	}

	void Update ()
	{
		HandleInput ();
		Move (playerVelocity);
	}

	void HandleInput()
	{
		playerVelocity = new Vector3 (Input.GetAxis ("Horizontal") * speed, Input.GetAxis ("Vertical") * speed, 0);
	}

	void Move(Vector3 velocity)
	{
		transform.position = Vector3.Lerp (transform.position, velocity + transform.position, Time.deltaTime);
	}

}
