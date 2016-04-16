using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	float baseXScale;

	public bool isAuto;

	public int bulletsPerShot;

	public float fireRate;
	float fireRateCounter;

	public float rotationDeviation;
	public float bulletSpeedDeviation;

	public GameObject bulletPrefab;
	public GameObject muzzleFlash;

	//Bullet data
	public float bulletSpeed;

	void Start()
	{
		fireRateCounter = fireRate;
		baseXScale = transform.localScale.x;
	}

	void Update ()
	{
		fireRateCounter += Time.deltaTime;
		HandleInput ();
	}

	void HandleInput ()
	{
		if (isAuto)
		{
			if(Input.GetButton("Fire") && fireRateCounter >= fireRate)
				Shoot();
		}
		if (!isAuto)
		{
			if (Input.GetButtonDown ("Fire") && fireRateCounter >= fireRate)
				Shoot ();
		}

		LookAtMouse ();
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

	public void Shoot ()
	{
		fireRateCounter = 0;
		for (int i = 0; i < bulletsPerShot; i++)
		{
			InstantiateBullet ();
		}
	}

	void InstantiateBullet ()
	{
		Quaternion rotationHolder = new Quaternion ();
		rotationHolder.eulerAngles = new Vector3 (0, 0, Random.Range (-rotationDeviation, rotationDeviation) + transform.rotation.eulerAngles.z);

		GameObject bullet = Instantiate (bulletPrefab, transform.position, rotationHolder) as GameObject;

		//Pass Parameters to instantiated bullet
		bullet.GetComponent<Bullet> ().bulletSpeed = bulletSpeed;
		bullet.GetComponent<Bullet> ().bulletSpeedDeviation = bulletSpeedDeviation;

		Physics2D.IgnoreCollision (bullet.GetComponent<Collider2D> (), transform.parent.GetComponent<Collider2D> ());
	}
}
