using UnityEngine;
using System.Collections;

public class Player : Entity
{
	public float speed;
	public int currentCharacter; //1 = Lemmy, 2 = Cordulator, 3 = Francus

	Rigidbody2D rb;
	Animator animator;

	GameObject beeGun;
	GameObject revolver;
	GameObject airGun;

	GameObject activeGun;

	[HideInInspector]
	public GameObject currentCell;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		beeGun = transform.FindChild ("Bee Gun").gameObject;
		revolver = transform.FindChild ("Revolver").gameObject;
		airGun = transform.FindChild ("HairDryerGun").gameObject;

		UpdateCharacter(currentCharacter);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Keypad1))
			currentCharacter = 1;
		if (Input.GetKeyDown (KeyCode.Keypad2))
			currentCharacter = 2;
		if (Input.GetKeyDown (KeyCode.Keypad3))
			currentCharacter = 3;
		
		UpdateCharacter (currentCharacter);
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
		GameObject gun = activeGun;

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

	void UpdateCharacter(int characterToSwitchTo)
	{
		animator.SetInteger ("Character", currentCharacter);

		switch (characterToSwitchTo)
		{
		case 1://Lemmy
			activeGun = beeGun;

			beeGun.SetActive(true);
			revolver.SetActive(false);
			airGun.SetActive(false);
			break;
		case 2://Cordulator
			activeGun = airGun;

			airGun.SetActive(true);
			revolver.SetActive(false);
			beeGun.SetActive(false);
			break;
		case 3://Francus
			activeGun = revolver;

			revolver.SetActive(true);
			beeGun.SetActive(false);
			airGun.SetActive(false);
			break;
		}
	}
}
