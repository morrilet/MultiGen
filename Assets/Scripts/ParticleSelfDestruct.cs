using UnityEngine;
using System.Collections;

public class ParticleSelfDestruct : MonoBehaviour 
{
	void Update () 
	{
		if (!this.GetComponent<ParticleSystem> ().IsAlive()) 
		{
			Destroy (this.gameObject);
		}
	}
}
