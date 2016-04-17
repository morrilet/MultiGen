using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public float maxHealth;
	[HideInInspector]
	public float health;

	public virtual void Awake ()
	{
		health = maxHealth;
	}
}
