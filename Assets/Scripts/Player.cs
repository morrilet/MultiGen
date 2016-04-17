using UnityEngine;
using System.Collections;

public class Player : Entity
{
	public float speed;

	Rigidbody2D rb;
	Animator animator;

	[HideInInspector]
	public GameObject currentCell;

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
		FlipGun ();
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "AIPathCell") 
		{
			currentCell = other.gameObject;
		}
	}

	void FlipGun()
	{
		GameObject gun = transform.FindChild ("Gun").gameObject;

		float gunRotation = gun.transform.eulerAngles.z;
		if (gunRotation > 180)
			gunRotation = Mathf.Abs(360 - gun.transform.eulerAngles.z);

		if (gunRotation > 90)
			gun.GetComponent<Gun> ().flipped = true;
		else
			gun.GetComponent<Gun> ().flipped = false;

		Debug.Log (gunRotation);
	}

	void UpdateMovement()
	{
		Vector2 velocity = new Vector2 (Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, Time.deltaTime),
			Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, Time.deltaTime));
		rb.velocity = velocity;
		animator.SetFloat ("Speed", Mathf.Abs (velocity.magnitude));
	}
}
