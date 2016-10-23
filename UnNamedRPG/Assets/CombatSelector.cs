using UnityEngine;
using System.Collections;

public class CombatSelector : MonoBehaviour {


    public int selectMode = 0;

    private bool modeLock = false;
    private const int SELECT_MODE = 0;
    private const int MOVE_MODE = 1;
    private const int ATTACK_MODE = 2;
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
    private PlayerGrid playerGridSelect = null;
    private Player playerSelected = null;
    private EnemyGrid enemyGridSelect = null;
    private Enemy enemySelected = null;
    private CombatArea combatArea;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(-START_DIVIDE_PLAYER, TILE_SPACE);
        horizPos = 0;
        vertPos = 0;
        playerSide = true;
        playerSelected = null;
        combatArea = GameObject.Find("CombatArea").GetComponent<CombatArea>();
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("onPlayerSide is: " + playerSide);
            if(playerSide && playerSelected == null)
            {
                playerGridSelect = combatArea.findPlayerGrid(horizPos, vertPos);
                Debug.Log("Player at: " + playerGridSelect.getHorizPos() +
                    ", " + playerGridSelect.getVertPos());
                if (playerGridSelect.occupied!=0)
                {
                    playerSelected = playerGridSelect.playersOnTile[0];
                }
            }
            else if(playerSide && playerSelected != null)
            {
                playerGridSelect = combatArea.findPlayerGrid(horizPos, vertPos);
                playerSelected.moveTile(playerGridSelect);
                combatArea.deletePhysGrid();
                combatArea.buildPhysGrid();
                playerSelected = null;
            }
            else if(!playerSide && playerSelected ==null)
            {
                enemyGridSelect = combatArea.findEnemyGrid(horizPos, vertPos);
                Debug.Log("Enemy at: " + enemyGridSelect.getHorizPos() +
                          ", " + enemyGridSelect.getVertPos());
                if(enemyGridSelect.occupied!=0)
                {
                    enemySelected = enemyGridSelect.enemiesOnTile[0];
                }
                combatArea.deletePhysGrid();
                combatArea.buildPhysGrid();
                //TODO: run examine or something on enemy select
            }
            else if(!playerSide && playerSelected != null)
            {
                enemyGridSelect = combatArea.findEnemyGrid(horizPos, vertPos);
                Debug.Log("EnemyGrid at: " + enemyGridSelect.getHorizPos() +
                    ", " + enemyGridSelect.getVertPos());
                if (enemyGridSelect.occupied!=0)
                {
                    enemySelected = enemyGridSelect.enemiesOnTile[0];
                    playerSelected.hitEnemyRegular(enemySelected);
                    Debug.Log("Enemy.health: " + enemySelected.health);
                }
                playerSelected = null;
                combatArea.deletePhysGrid();
                combatArea.buildPhysGrid();
            }
        }
    }
}
