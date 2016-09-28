using UnityEngine;
using System.Collections;

public class CombatSelector : MonoBehaviour {

    private const float START_DIVIDE_PLAYER = 4f;
    private const float START_DIVIDE_ENEMY = .4f;
    private const float TILE_SPACE = 1.2f;
    private const int LEFT_HORIZ = 0;
    private const int CENTER_HORIZ = 1;
    private const int RIGHT_HORIZ = 2;
    private const int UP_VERT = 0;
    private const int CENTER_VERT = 1;
    private const int DOWN_VERT = 2;
    private int horizPos = 0;
    private int vertPos = 0;
    private bool playerSide = true;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(-START_DIVIDE_PLAYER, TILE_SPACE);
        horizPos = 0;
        vertPos = 0;
        playerSide = true;
	}
	
	// Update is called once per frame
	void Update () {
        readKeys();
	}

    private void readKeys()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (vertPos > UP_VERT)
            {
                transform.Translate(new Vector3(0, TILE_SPACE, 0));
                vertPos--;
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (vertPos < DOWN_VERT)
            {
                transform.Translate(new Vector3(0, -TILE_SPACE, 0));
                vertPos++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (horizPos > LEFT_HORIZ) { 
                transform.Translate(new Vector3(-TILE_SPACE, 0, 0));
                horizPos--;
            }
            else if(!playerSide && horizPos == LEFT_HORIZ)
            {
                transform.Translate(new Vector3(-START_DIVIDE_ENEMY * 2 - TILE_SPACE,
                                                 0, 0));
                playerSide = true;
                horizPos = RIGHT_HORIZ;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (horizPos < RIGHT_HORIZ)
            {
                transform.Translate(new Vector3(TILE_SPACE, 0, 0));
                horizPos++;
            }
            else if (playerSide && horizPos == RIGHT_HORIZ)
            {
                transform.Translate(new Vector3(START_DIVIDE_ENEMY * 2 + TILE_SPACE,
                                                0, 0));
                playerSide = false;
                horizPos = LEFT_HORIZ;
            }
        }
    }
}
