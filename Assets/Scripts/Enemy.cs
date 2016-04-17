using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	Animator stateMachine;
	Player player;

	float distanceToPlayer;
	bool playerInSight;

	void Start()
	{
		stateMachine = GetComponent<Animator> ();
		player = GameObject.Find ("Player");
	}
}
