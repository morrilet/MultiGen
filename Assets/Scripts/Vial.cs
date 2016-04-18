using UnityEngine;
using System.Collections;

public class Vial : MonoBehaviour {

	public CharacterToChangeTo characterToChangeTo;

	public Sprite lemmyVial;
	public Sprite corduVial;
	public Sprite francusVial;

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
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
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
