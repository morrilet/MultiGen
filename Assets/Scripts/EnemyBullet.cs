using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	[HideInInspector]
	public float bulletSpeed; //Speed of bullet
	[HideInInspector]
	public float bulletSpeedDeviation; //Used for shotguns
	[HideInInspector]
	public float damage;
	//[HideInInspector]
	//public float sleepFramesOnHit;

	//	public GameObject impactEffect;
	[HideInInspector]
	public Vector3 startPos;

	public virtual void Start()
	{
		startPos = transform.position;
		bulletSpeed += Random.Range (-1 * bulletSpeedDeviation, bulletSpeedDeviation);

		//GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		//for (int i = 0; i < enemies.Length; i++) 
		//{
			//Physics2D.IgnoreCollision (GetComponent<Collider2D> (), enemies[i].GetComponent<Collider2D> ());
		//}
	}

	public virtual void Update ()
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

		if (coll.gameObject.tag == "Player")
		{
			//Camera.main.GetComponent<CameraFollowTrap> ().ScreenShake (.1f, .075f);
			//GameManager.instance.Sleep (2);
			Destroy (gameObject);
		}
	}
}
