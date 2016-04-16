using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public float maxHealth;
	float health;

	public virtual void Awake ()
	{
		health = maxHealth;
	}
}
