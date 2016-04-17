using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Animator stateMachine;
	public LayerMask sightLayerMask; //The layers that the enemy AI can see.

	Player player;

	float distanceToPlayer;
	bool playerInSight;

	float speed;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void Update()
	{
		GetDistanceToPlayer ();
		GetPlayerInSight ();
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
}