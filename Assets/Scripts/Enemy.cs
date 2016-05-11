using UnityEngine;
using System.Collections;

public class Enemy : Entity 
{
	public Animator stateMachine;
	public LayerMask sightLayerMask; //The layers that the enemy AI can see.

	Player player;

	public GameObject currentCell;

	[HideInInspector]
	public EnemyGun gun;
	Color baseColor;

	float distanceToPlayer;
	bool playerInSight;

	public float speed;

	[HideInInspector]
	public float currentSpeed;
	Vector3 lastPosition;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
		gun = transform.FindChild ("Gun").gameObject.GetComponent<EnemyGun> ();
		currentCell = null;
		baseColor = GetComponent<SpriteRenderer> ().color;
	}

	void Update()
	{
		if (!GameManager.instance.isPaused) 
		{
			GetDistanceToPlayer ();
			GetPlayerInSight ();
		}
			
		currentSpeed = (lastPosition - transform.position).magnitude * 100f;

		gun.LookAtTarget (player.gameObject);
		FlipGun ();

		lastPosition = transform.position;
	}

	void FlipGun()
	{
		float gunRotation = gun.transform.eulerAngles.z;
		if (gunRotation > 180)
			gunRotation = Mathf.Abs(360 - gun.transform.eulerAngles.z);

		if (gunRotation > 90)
			gun.flipped = true;
		else
			gun.flipped = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "AIPathCell") 
		{
			currentCell = other.gameObject;
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "AIPathCell")
		{
			currentCell = other.gameObject;
		}
	}

	void GetDistanceToPlayer()
	{
		distanceToPlayer = Vector3.Distance (transform.position, player.transform.position);
		stateMachine.SetFloat ("distanceToPlayer", distanceToPlayer);
	}

	void GetPlayerInSight()
	{
		RaycastHit2D hit;
		hit = Physics2D.Raycast (transform.position, player.transform.position - transform.position, distanceToPlayer, sightLayerMask);

		//Debug.DrawLine (transform.position, player.transform.position, Color.blue);
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.tag == "Player")
			{
				playerInSight = true;
				Debug.DrawLine (transform.position, player.transform.position, Color.blue);
			}
			if (hit.collider.gameObject.tag == "Wall")
			{
				playerInSight = false;
				Debug.DrawLine (transform.position, player.transform.position, Color.red);
			}
		}

		stateMachine.SetBool ("playerInSight", playerInSight);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			health -= other.gameObject.GetComponent<Bullet> ().damage;

			if (health <= 0)
			{
				Die ();
			}

		}
	}

	void Die()
	{
		//Destroy (gameObject);
		stateMachine.SetBool("Dead", true);
	}
}