using UnityEngine;
using System.Collections;

public class Player : Entity
{
	public float speed;

	Rigidbody2D rb;
	Animator animator;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Keypad1))
			animator.SetInteger ("Character", 1);
		if (Input.GetKeyDown (KeyCode.Keypad2))
			animator.SetInteger ("Character", 2);

		UpdateMovement ();
	}

	void UpdateMovement()
	{
		Vector2 velocity = new Vector2 (Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, Time.deltaTime),
			Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, Time.deltaTime));
		rb.velocity = velocity;
		animator.SetFloat ("Speed", Mathf.Abs (velocity.magnitude));
	}
}
