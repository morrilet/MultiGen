using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour 
{
	float baseXScale;

	public Sprite gunImage;
	public Sprite gunFlippedImage;
	public bool flipped; 

	public bool isAuto;

	public float fireRate;
	[HideInInspector]
	public float fireRateCounter;

	public float rotationDeviation;
	public float bulletSpeedDeviation;

	public GameObject bulletPrefab;

	public string shootSound;

	public Vector3 bulletOffset;
	//Bullet data
	public float bulletSpeed;
	public float damage;

	public virtual void Start()
	{
		fireRateCounter = fireRate;
		baseXScale = transform.localScale.x;
		flipped = false;
	}

	public virtual void Update ()
	{
		if (!GameManager.instance.isPaused)
		{
			fireRateCounter += Time.deltaTime;

			if (flipped)
				GetComponent<SpriteRenderer> ().sprite = gunFlippedImage;
			else if (!flipped)
				GetComponent<SpriteRenderer> ().sprite = gunImage;
		}
	}

	public void LookAtTarget(GameObject target)
	{
		Vector3 targetPos = target.transform.position;

		//Get rotation in radians
		float rotation = transform.eulerAngles.z;

		rotation = Mathf.Atan2 (targetPos.y - transform.position.y, targetPos.x - transform.position.x);

		//Convert to degrees
		rotation *= Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler (transform.eulerAngles.x, transform.eulerAngles.y, rotation);
	}

	public virtual void Shoot ()
	{
		//		Camera.main.GetComponent<CameraFollowTrap> ().ScreenShake (.05f, .1f);
		if(shootSound != null)
			AudioManager.instance.PlayRandomFromList(AudioManager.instance.lemmyBeegun);

		fireRateCounter = 0;
		InstantiateBullet ();
	}

	void InstantiateBullet ()
	{
		Quaternion rotationHolder = new Quaternion ();
		rotationHolder.eulerAngles = new Vector3 (0, 0, Random.Range (-rotationDeviation, rotationDeviation) + transform.rotation.eulerAngles.z);

		GameObject bullet = Instantiate (bulletPrefab, transform.position + bulletOffset, rotationHolder) as GameObject;

		//Pass Parameters to instantiated bullet
		bullet.GetComponent<EnemyBullet> ().bulletSpeed = bulletSpeed + Random.Range(-bulletSpeedDeviation, bulletSpeedDeviation);
		bullet.GetComponent<EnemyBullet> ().bulletSpeedDeviation = bulletSpeedDeviation;
		bullet.GetComponent<EnemyBullet> ().damage = damage;

		Physics2D.IgnoreCollision (bullet.GetComponent<Collider2D> (), transform.parent.GetComponent<Collider2D> ());
	}
}
