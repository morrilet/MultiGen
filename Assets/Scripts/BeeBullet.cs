using UnityEngine;
using System.Collections;

public class BeeBullet : Bullet
{
	public float frequency = 20f;
	public float magnitude = .5f;

	Vector3 axis;
	Vector3 pos;

	public override void Start ()
	{
		pos = transform.position;
		axis = transform.right;
	}

	public override void Update ()
	{
//		pos += transform.right * Time.deltaTime;
//		transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
		base.Update();
	}
}
