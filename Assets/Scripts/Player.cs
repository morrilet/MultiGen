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

		SwitchCharacter(GameManager.instance.currentCharacter);
		health = GameManager.instance.playerHealth;
	}

	void Update ()
	{
		if (!GameManager.instance.isPaused) 
		{
			if (health > maxHealth)
				health = maxHealth;
			UpdateMovement ();
			FlipGun ();
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("EnemyBullet")) 
		{
			Camera.main.GetComponent<CameraFollowTrap> ().ScreenShake (.2f, .5f);
			health -= other.gameObject.GetComponent<EnemyBullet> ().damage;
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "AIPathCell") 
		{
			currentCell = other.gameObject;

			if (health <= 0)
			{
				Die ();
			}
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
	}

	void UpdateMovement()
	{
		if (!GameManager.instance.isPaused)
		{
			Vector2 velocity = new Vector2 (Mathf.Lerp (0, Input.GetAxis ("Horizontal") * speed, Time.deltaTime),
				               Mathf.Lerp (0, Input.GetAxis ("Vertical") * speed, Time.deltaTime));
			rb.velocity = velocity;
			animator.SetFloat ("Speed", Mathf.Abs (velocity.magnitude));
		}

	}

	public void SwitchCharacter(int characterToSwitchTo)
	{
		GameObject poof = Instantiate (Resources.Load ("Poof", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
		poof.transform.parent = transform;

		animator.SetInteger ("Character", characterToSwitchTo);
		switch (characterToSwitchTo)
		{
		case 1://Lemmy
			activeGun = beeGun;
			currentCharacter = 1;
			beeGun.SetActive(true);
			revolver.SetActive(false);
			airGun.SetActive(false);
			break;
		case 2://Cordulator
			activeGun = airGun;

			poof.transform.GetComponent<ParticleSystem> ().startColor = new Color (141, 58, 153);
			currentCharacter = 2;
			airGun.SetActive(true);
			revolver.SetActive(false);
			beeGun.SetActive(false);
			break;
		case 3://Francus
			activeGun = revolver;

			poof.transform.GetComponent<ParticleSystem> ().startColor = new Color (85, 170, 0);
			currentCharacter = 3;
			revolver.SetActive(true);
			beeGun.SetActive(false);
			airGun.SetActive(false);
			break;
		}
	}

	void Die()
	{
	}
}
