using UnityEngine;
using System.Collections;

public class BeeGun : Gun
{
	float addBeeCounter;
	int beesInGun;
	float baseRotationDeviation;
	float baseSpeedDeviation;

	// Use this for initialization
	void Start ()
	{
		beesInGun = 1;
		baseRotationDeviation = rotationDeviation;
		baseSpeedDeviation = bulletSpeedDeviation;
	}

	public override void Update()
	{
		base.Update ();
		addBeeCounter += Time.deltaTime;
	}

	public override void HandleInput()
	{
		if(Input.GetButtonDown("Fire") && fireRateCounter >= fireRate)
		{
			beesInGun = 1;
		}
		if (Input.GetButton ("Fire") && beesInGun <= 30)
		{
			if (addBeeCounter >= .4f)
			{
				beesInGun += 2;
				addBeeCounter = 0;
				rotationDeviation += 1.5f;
				bulletSpeedDeviation += .4f;
			}
		}
		if (Input.GetButtonUp ("Fire"))
		{
			Shoot (beesInGun);
			bulletSpeedDeviation = baseSpeedDeviation;
			rotationDeviation = baseRotationDeviation;
		}
	}
}
