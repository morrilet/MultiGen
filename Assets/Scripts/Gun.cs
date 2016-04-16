using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public bool isAuto;

	public int bulletsPerShot;

	public float fireRate;
	float fireRateCounter;

	public float rotationDeviation;

	public float bulletSpeed;
	public float bulletSpeedDeviation;

	public GameObject bullet;
	public GameObject muzzleFlash;

	void Start ()
	{
		
	}

	void Update ()
	{
		LookAtMouse ();
	}

	public void Shoot ()
	{
		
	}

	void InstantiateBullet ()
	{
		
	}

	void LookAtMouse()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);

		//Get rotation in radians
		float rotation = Mathf.Atan2 (mousePos.y - transform.position.y, mousePos.x - transform.position.x);
		//Convert to degrees
		rotation *= (180 / Mathf.PI);

		transform.rotation = Quaternion.Euler (0, 0, rotation);
	}
}
