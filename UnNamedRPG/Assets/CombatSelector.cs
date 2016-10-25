using UnityEngine;
using System.Collections;

public class CombatSelector : MonoBehaviour
{


    public int selectMode = 0;
    public GameObject moveButton;
    public GameObject attackButton;
    public GameObject statusButton;

    private int modeLock = 0;
    private const int INITIAL_SELECT = 0;
    private const int MENU_SELECT = 1;
    private const int SECONDARY_SELECT = 2;
    private const int STATUS_VIEW_SELECT = 3;
    private const int STATUS_MODE = 0;
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
    void Start()
    {
        transform.position = new Vector3(-START_DIVIDE_PLAYER, TILE_SPACE);
        horizPos = 0;
        vertPos = 0;
        playerSide = true;
        playerSelected = null;
        combatArea = GameObject.Find("CombatArea").GetComponent<CombatArea>();
    }

    // Update is called once per frame
    void Update()
    {
        readKeys();
        cleanSlate();
    }

    private void cleanSlate()
    {
        if (modeLock == INITIAL_SELECT)
        {
            playerSelected = null;
            enemySelected = null;
        }
        if(modeLock != MENU_SELECT)
        {
            if(GameObject.FindGameObjectsWithTag("buttons").Length > 0)
            {
                for(int i = 0; i < GameObject.FindGameObjectsWithTag("buttons").Length; i++)
                {
                    GameObject.Destroy(GameObject.FindGameObjectsWithTag("buttons")[i]);
                }
            }
        }
    }


    private void readKeys()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            modeLock = INITIAL_SELECT;
            selectMode = STATUS_MODE;
            Debug.Log("Resetting to Initial Select");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (modeLock == MENU_SELECT)
            {
                if (selectMode > STATUS_MODE)
                {
                    selectMode--;
                    if(selectMode == STATUS_MODE)
                    {
                        Debug.Log("Status");
                    } 
                    else if(selectMode == MOVE_MODE)
                    {
                        Debug.Log("Move");
                    }
                    else if(selectMode == ATTACK_MODE)
                    {
                        Debug.Log("Attack");
                    }
                }
            }
            else if(modeLock == STATUS_VIEW_SELECT)
            {

            }
            else if (vertPos > UP_VERT)
            {
                transform.Translate(new Vector3(0, TILE_SPACE, 0));
                vertPos--;
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (modeLock == MENU_SELECT)
            {
                if (selectMode < ATTACK_MODE)
                {
                    selectMode++;
                    if (selectMode == STATUS_MODE)
                    {
                        Debug.Log("Status");
                    }
                    else if (selectMode == MOVE_MODE)
                    {
                        Debug.Log("Move");
                    }
                    else if (selectMode == ATTACK_MODE)
                    {
                        Debug.Log("Attack");
                    }
                }
            }
            else if (modeLock == STATUS_VIEW_SELECT)
            {

            }
            else if (vertPos < DOWN_VERT)
            {
                transform.Translate(new Vector3(0, -TILE_SPACE, 0));
                vertPos++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (modeLock == MENU_SELECT)
            {

            }
            else if (modeLock == STATUS_VIEW_SELECT)
            {

            }
            else if (horizPos > LEFT_HORIZ)
            {
                transform.Translate(new Vector3(-TILE_SPACE, 0, 0));
                horizPos--;
            }
            else if (!playerSide && horizPos == LEFT_HORIZ)
            {
                transform.Translate(new Vector3(-START_DIVIDE_ENEMY * 2 - TILE_SPACE,
                                                 0, 0));
                playerSide = true;
                horizPos = RIGHT_HORIZ;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(modeLock == MENU_SELECT)
            {

            }
            else if (modeLock == STATUS_VIEW_SELECT)
            {

            }
            else if (horizPos < RIGHT_HORIZ)
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
            if (modeLock == MENU_SELECT)
            {
                if (selectMode != STATUS_MODE)
                {
                    modeLock = SECONDARY_SELECT;
                    Debug.Log("Selecting Secondary Target");
                }
                else
                {
                    modeLock = STATUS_VIEW_SELECT;
                    Debug.Log("Showing Status");
                }
            }

            else if (modeLock == STATUS_VIEW_SELECT)
            {
                modeLock = INITIAL_SELECT;
            }
            else if (playerSide && playerSelected == null && modeLock == INITIAL_SELECT)
            {
                playerGridSelect = combatArea.findPlayerGrid(horizPos, vertPos);
                Debug.Log("Player at: " + playerGridSelect.getHorizPos() +
                    ", " + playerGridSelect.getVertPos());
                if (playerGridSelect.occupied != 0)
                {
                    playerSelected = playerGridSelect.playersOnTile[0];
                    modeLock = MENU_SELECT;
                    GameObject new1 = GameObject.Instantiate(moveButton);
                    new1.transform.SetParent(GameObject.Find("CombatCanvas").transform);
                    new1 = GameObject.Instantiate(attackButton);
                    new1.transform.SetParent(GameObject.Find("CombatCanvas").transform);
                    new1 = GameObject.Instantiate(statusButton);
                    new1.transform.SetParent(GameObject.Find("CombatCanvas").transform);
                    if (selectMode == STATUS_MODE)
                    {
                        Debug.Log("Status");
                    }
                    else if (selectMode == MOVE_MODE)
                    {
                        Debug.Log("Move");
                    }
                    else if (selectMode == ATTACK_MODE)
                    {
                        Debug.Log("Attack");
                    }
                }
            }
            else if (playerSide && playerSelected != null && modeLock == SECONDARY_SELECT && selectMode == MOVE_MODE)
            {
                playerGridSelect = combatArea.findPlayerGrid(horizPos, vertPos);
                playerSelected.moveTile(playerGridSelect);
                combatArea.deletePhysGrid();
                combatArea.buildPhysGrid();
                playerSelected = null;
                modeLock = INITIAL_SELECT;
            }
            else if (!playerSide && playerSelected == null && modeLock == INITIAL_SELECT)
            {
                enemyGridSelect = combatArea.findEnemyGrid(horizPos, vertPos);
                Debug.Log("Enemy at: " + enemyGridSelect.getHorizPos() +
                          ", " + enemyGridSelect.getVertPos());
                if (enemyGridSelect.occupied != 0)
                {
                    enemySelected = enemyGridSelect.enemiesOnTile[0];
                    modeLock = STATUS_VIEW_SELECT;
                }
                combatArea.deletePhysGrid();
                combatArea.buildPhysGrid();
                //TODO: run examine or something on enemy select
            }
            else if (!playerSide && playerSelected != null && modeLock == SECONDARY_SELECT && selectMode == ATTACK_MODE)
            {
                enemyGridSelect = combatArea.findEnemyGrid(horizPos, vertPos);
                Debug.Log("EnemyGrid at: " + enemyGridSelect.getHorizPos() +
                    ", " + enemyGridSelect.getVertPos());
                if (enemyGridSelect.occupied != 0)
                {
                    enemySelected = enemyGridSelect.enemiesOnTile[0];
                    playerSelected.hitEnemyRegular(enemySelected);
                    Debug.Log("Enemy.health: " + enemySelected.health);
                    modeLock = INITIAL_SELECT;
                }
                playerSelected = null;
                combatArea.deletePhysGrid();
                combatArea.buildPhysGrid();
            }
        }
    }
}
