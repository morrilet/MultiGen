using UnityEngine;
using System.Collections;

public class Vial : MonoBehaviour {

	public CharacterToChangeTo characterToChangeTo;

	public Sprite lemmyVial;
	public Sprite corduVial;
	public Sprite francusVial;

	float duration;
	Vector3 topPos;
	Vector3 bottomPos;

	void Start ()
	{
		switch (characterToChangeTo)
		{
		case CharacterToChangeTo.Lemmy:
			GetComponent<SpriteRenderer> ().sprite = lemmyVial;
			break;
		case CharacterToChangeTo.Cordulator:
			GetComponent<SpriteRenderer> ().sprite = corduVial;
			break;
		case CharacterToChangeTo.Francus:
			GetComponent<SpriteRenderer> ().sprite = francusVial;
			break;
		}

		duration = 2.5f;
		topPos = transform.position + new Vector3 (0, .15f);
		bottomPos = transform.position + new Vector3 (0, -.15f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		float lerpValue = Mathf.PingPong (Time.time, duration) / duration;
		transform.position = Vector3.Lerp (topPos, bottomPos, lerpValue);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<Player> ().health += 10;

			switch (characterToChangeTo)
			{
			case CharacterToChangeTo.Lemmy:
				other.gameObject.GetComponent<Player> ().SwitchCharacter(1);
				break;
			case CharacterToChangeTo.Cordulator:
				other.gameObject.GetComponent<Player> ().SwitchCharacter(2);
				break;
			case CharacterToChangeTo.Francus:
				other.gameObject.GetComponent<Player> ().SwitchCharacter (3);
				break;
			}

			Destroy (gameObject);
		}
	}

	public enum CharacterToChangeTo
	{
		Lemmy,
		Cordulator,
		Francus
	}
}
