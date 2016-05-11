using UnityEngine;

public class Room
{
	public int xPos;                      // The x coordinate of the lower left tile of the room.
	public int yPos;                      // The y coordinate of the lower left tile of the room.
	public int roomWidth;                     // How many tiles wide the room is.
	public int roomHeight;                    // How many tiles high the room is.
	public Direction enteringCorridor;    // The direction of the corridor that is entering this room.

	public int enemyCount;
	private Vector2[] enemyLocations;

	// This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
	public void SetupRoom (IntRange widthRange, IntRange heightRange, int columns, int rows)
	{
		// Set a random width and height.
		roomWidth = widthRange.Random;
		roomHeight = heightRange.Random;

		// Set the x and y coordinates so the room is roughly in the middle of the board.
		xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
		yPos = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);

		SetupAIPathCell ();
	}

	// This is an overload of the SetupRoom function and has a corridor parameter that represents the corridor entering the room.
	public void SetupRoom (IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
	{
		// Set the entering corridor direction.
		enteringCorridor = corridor.direction;

		// Set random values for width and height.
		roomWidth = widthRange.Random;
		roomHeight = heightRange.Random;

		switch (corridor.direction)
		{
		// If the corridor entering this room is going north...
		case Direction.North:
			// ... the height of the room mustn't go beyond the board so it must be clamped based
			// on the height of the board (rows) and the end of corridor that leads to the room.
			roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);

			// The y coordinate of the room must be at the end of the corridor (since the corridor leads to the bottom of the room).
			yPos = corridor.EndPositionY;

			// The x coordinate can be random but the left-most possibility is no further than the width
			// and the right-most possibility is that the end of the corridor is at the position of the room.
			xPos = Random.Range (corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);

			// This must be clamped to ensure that the room doesn't go off the board.
			xPos = Mathf.Clamp (xPos, 0, columns - roomWidth);
			break;
		case Direction.East:
			roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
			xPos = corridor.EndPositionX;

			yPos = Random.Range (corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
			yPos = Mathf.Clamp (yPos, 0, rows - roomHeight);
			break;
		case Direction.South:
			roomHeight = Mathf.Clamp (roomHeight, 1, corridor.EndPositionY);
			yPos = corridor.EndPositionY - roomHeight + 1;

			xPos = Random.Range (corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
			xPos = Mathf.Clamp (xPos, 0, columns - roomWidth);
			break;
		case Direction.West:
			roomWidth = Mathf.Clamp (roomWidth, 1, corridor.EndPositionX);
			xPos = corridor.EndPositionX - roomWidth + 1;

			yPos = Random.Range (corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
			yPos = Mathf.Clamp (yPos, 0, rows - roomHeight);
			break;
		}

		SetupAIPathCell ();
		PlaceEnemies ();
	}

	public void AddVial()
	{
		int vialType = Random.Range (1, 4);
		Vector2 vialPos = new Vector2 (xPos + (roomWidth * Random.Range (.1f, .9f)), yPos + (roomHeight * Random.Range (.1f, .9f)));

		GameObject vial = GameObject.Instantiate (Resources.Load("Vial", typeof(GameObject)), vialPos, Quaternion.identity) as GameObject;
		switch (vialType) 
		{
		case 1:
			vial.GetComponent<Vial> ().characterToChangeTo = Vial.CharacterToChangeTo.Lemmy;
			break;
		case 2:
			vial.GetComponent<Vial> ().characterToChangeTo = Vial.CharacterToChangeTo.Cordulator;
			break;
		case 3:
			vial.GetComponent<Vial> ().characterToChangeTo = Vial.CharacterToChangeTo.Francus;
			break;
		}
	}

	public void AddLadder()
	{
		Vector2 ladderPos = new Vector2(xPos + (roomWidth * Random.Range(.25f, .75f)), yPos + (roomHeight * Random.Range(.25f, .75f)));
		GameObject ladder = GameObject.Instantiate(Resources.Load("Ladder", typeof(GameObject)), ladderPos, Quaternion.identity) as GameObject;
	}

	private void PlaceEnemies()
	{
		enemyCount = Random.Range ((int)(GameManager.instance.currentLevel / 4f), 2 + (int)(GameManager.instance.currentLevel / 2f));
		enemyLocations = new Vector2[enemyCount];
		for (int i = 0; i < enemyLocations.Length; i++)
		{
			enemyLocations [i].x = Random.Range (xPos, xPos + roomWidth);
			enemyLocations [i].y = Random.Range (yPos, yPos + roomHeight);

			GameObject enemy = GameObject.Instantiate (Resources.Load("Enemy", typeof(GameObject)), enemyLocations[i], Quaternion.identity) as GameObject;
		}
	}

	private void SetupAIPathCell()
	{
		Vector3 cellPos = new Vector3(xPos + (roomWidth / 2), yPos + (roomHeight / 2));
		if (roomHeight % 2 == 0) 
		{
			cellPos.y = (int)cellPos.y - .5f;
		}
		if (roomWidth % 2 == 0) 
		{
			cellPos.x = (int)cellPos.x - .5f;
		}
		GameObject cell = GameObject.Instantiate (Resources.Load("AIPathCell", typeof(GameObject)), cellPos, Quaternion.identity) as GameObject;
		cell.transform.localScale = new Vector3 (roomWidth, roomHeight, 1f);
	}
}