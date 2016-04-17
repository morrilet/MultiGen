using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	float baseXScale;

	public Sprite gunImage;
	public Sprite gunFlippedImage;
	public bool flipped; 

	public bool isAuto;

	public int bulletsPerShot;

	public float fireRate;
	[HideInInspector]
	public float fireRateCounter;

	public float rotationDeviation;
	public float bulletSpeedDeviation;

	public GameObject bulletPrefab;
	public GameObject muzzleFlash;

	public string shootSound;

	public Vector3 bulletOffset;
	//Bullet data
	public float bulletSpeed;

	public virtual void Start()
	{
		fireRateCounter = fireRate;
		baseXScale = transform.localScale.x;
		flipped = false;
	}

	public virtual void Update ()
	{
		fireRateCounter += Time.deltaTime;
		HandleInput ();
		LookAtMouse ();

		if (flipped)
			GetComponent<SpriteRenderer> ().sprite = gunFlippedImage;
		else if (!flipped)
			GetComponent<SpriteRenderer> ().sprite = gunImage;
	}

	public virtual void HandleInput ()
	{
		if (isAuto)
		{
			if(Input.GetButton("Fire") && fireRateCounter >= fireRate)
				Shoot(bulletsPerShot);
		}
		if (!isAuto)
		{
			if (Input.GetButtonDown ("Fire") && fireRateCounter >= fireRate)
				Shoot (bulletsPerShot);
		}
	}

	void LookAtMouse()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);

		//Get rotation in radians
		float rotation = transform.eulerAngles.z;
		
		rotation = Mathf.Atan2 (mousePos.y - transform.position.y, mousePos.x - transform.position.x);

		//Convert to degrees
		rotation *= Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler (transform.eulerAngles.x, transform.eulerAngles.y, rotation);
	}

	public virtual void Shoot (int bulletsToInstantiate)
	{
//		Camera.main.GetComponent<CameraFollowTrap> ().ScreenShake (.05f, .1f);
//		if(shootSound != null)
//			AudioManager.instance.PlaySoundEffectVariation(shootSound, 1, 3);
		fireRateCounter = 0;
		for (int i = 0; i < bulletsToInstantiate; i++)
		{
			InstantiateBullet ();
		}
	}

	void InstantiateBullet ()
	{
		Quaternion rotationHolder = new Quaternion ();
		rotationHolder.eulerAngles = new Vector3 (0, 0, Random.Range (-rotationDeviation, rotationDeviation) + transform.rotation.eulerAngles.z);

		GameObject bullet = Instantiate (bulletPrefab, transform.position + bulletOffset, rotationHolder) as GameObject;

		//Pass Parameters to instantiated bullet
		bullet.GetComponent<Bullet> ().bulletSpeed = bulletSpeed + Random.Range(-bulletSpeedDeviation, bulletSpeedDeviation);
		bullet.GetComponent<Bullet> ().bulletSpeedDeviation = bulletSpeedDeviation;

		Physics2D.IgnoreCollision (bullet.GetComponent<Collider2D> (), transform.parent.GetComponent<Collider2D> ());
	}
}
