using UnityEngine;
using System.Collections;

/// <summary>
/// Moves 2 background images of equal size to make a scrolling effect.
/// </summary>

public class Background : MonoBehaviour 
{
	public GameObject background1, background2; //The 2 backgrounds to move and alternate between for scrolling effect.
	public float scrollSpeed; 				    //Speed at which to scroll the backgrounds.

	float horizExtent, vertExtent; //Screen extents.
	Vector3 top, bottom; 		   //Top and bottom positions for the backgrounds to scroll between.
	Vector3 backgroundSize;        //The size of the backgrounds, based on the first one.

	Vector3 offset;

	public void Start()
	{
		//Get screen extents.
		vertExtent = Camera.main.orthographicSize;
		horizExtent = vertExtent * Screen.width / Screen.height;

		//Get size of the backgrounds.
		backgroundSize = background1.GetComponent<Renderer> ().bounds.size;

		//Determine where top and bottom locations are.
		top    = new Vector3 (0, backgroundSize.y, 0);
		bottom = new Vector3 (0, -backgroundSize.y, 0);
		offset = new Vector3 (0, .025f, 0);

		//Set background starting positions.
		background1.transform.position = top;
		background2.transform.position = Vector3.zero;

		//Set the width of the backgrounds to fit the screen.
		Vector3 newLocalScale = background1.transform.localScale;
		newLocalScale.x = (horizExtent * 2) / backgroundSize.x;
		background1.transform.localScale = newLocalScale;
		background2.transform.localScale = newLocalScale;
	}

	public void Update()
	{
		ScrollBackground (background1);
		ScrollBackground (background2);
	}

	public void ScrollBackground(GameObject bg)
	{
		if(bg.transform.position.y <= bottom.y)
		{
			bg.transform.position = top - offset;
		}

		Vector3 position = bg.transform.position;
		position.y -= scrollSpeed;
		bg.transform.position = position;
	}
}
