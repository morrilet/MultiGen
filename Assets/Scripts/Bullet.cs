﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[HideInInspector]
	public float bulletSpeed; //Speed of bullet
	[HideInInspector]
	public float bulletSpeedDeviation; //Used for shotguns
	[HideInInspector]
	public float damage;
	[HideInInspector]
	public float sleepFramesOnHit;

//	public GameObject impactEffect;
	[HideInInspector]
	public Vector3 startPos;

	public void Start()
	{
		startPos = transform.position;
		bulletSpeed += Random.Range (-1 * bulletSpeedDeviation, bulletSpeedDeviation);
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider2D> ());
	}

	public void Update () 
	{
		if (!GameManager.instance.isPaused) 
		{
			transform.position += bulletSpeed * transform.right * Time.deltaTime;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Wall")
		{
//			GameObject impact = Instantiate (impactEffect, transform.position, transform.rotation) as GameObject;
			Destroy (gameObject);
		}

		if (coll.gameObject.tag == "Enemy")
		{
			Camera.main.GetComponent<CameraFollowTrap> ().ScreenShake (.2f, .15f);
			GameManager.instance.Sleep (1);
			Destroy (gameObject);
		}
	}
}
