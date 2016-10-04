using UnityEngine;
using System.Collections;

public class CombatArea : MonoBehaviour {

    public Transform playerTile;
    public Transform playerTileFilled;
    public Transform enemyTile;
    public Transform enemyTileFilled;

    public const int LEFT_HORIZ = 0;
    public const int CENTER_HORIZ = 1;
    public const int RIGHT_HORIZ = 2;
    public const int UP_VERT = 0;
    public const int CENTER_VERT = 1;
    public const int DOWN_VERT = 2;
    public const int NORMAL_FLOOR = 0;
    private const float START_DIVIDE_PLAYER = 4f;
    private const float START_DIVIDE_ENEMY = .4f;
    private const float TILE_SPACE = 1.2f;
    private PlayerGrid[,] playerSide;
    private EnemyGrid[,] enemySide;
    private int sizeOfGrids = 3;
    private Player firstPlayer;
    private Player secondPlayer;
    private Player thirdPlayer;
    private Player fourthPlayer;
    // Use this for initialization
    void Start () {
        //initialize grids
        playerSide = new PlayerGrid[sizeOfGrids,sizeOfGrids];
        enemySide = new EnemyGrid[sizeOfGrids, sizeOfGrids];
        for(int i = 0; i < sizeOfGrids; i++)
        {
            for(int j = 0; j < sizeOfGrids; j++)
            {
                playerSide[i, j] = new PlayerGrid(i, j);
                enemySide[i, j] = new EnemyGrid(i, j);
            }
        }
        formationMaking();
        buildPhysGrid();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void formationMaking()
    {
        firstPlayer = new GenericPlayer(1, 0, 1, 1);
    }
    public void deletePhysGrid()
    {
        for(int i = 0; i < sizeOfGrids; i++)
        {
            for(int j = 0; j < sizeOfGrids; j++)
            {
                Object.Destroy(GameObject.Find("playerTile" + i + j));
                Object.Destroy(GameObject.Find("enemyTile" + i + j));
            }
        }

    }
    public void buildPhysGrid()
    {
        //show visuals for the combat
        //players
        for (int i = 0; i < sizeOfGrids; i++)
        {
            for(int j = 0; j < sizeOfGrids; j++)
            {
                
                if(findPlayerGrid(i,j).occupied == 0)
                {
                    //instantiate empty tile
                    Object obj = 
                        Instantiate(playerTile, new Vector3(-START_DIVIDE_PLAYER + 
                        i * TILE_SPACE,(1-j)*TILE_SPACE,0),Quaternion.identity);
                    obj.name = "playerTile" + i + j;
                }
                else
                {
                    //instantiate filled tile
                    Object obj = 
                        Instantiate(playerTileFilled, new Vector3(-START_DIVIDE_PLAYER
                        + i * TILE_SPACE,(1 - j) * TILE_SPACE, 0), Quaternion.identity);
                    obj.name = "playerTile" + i + j;
                }
            }
        }

        //enemies
        for (int i = 0; i < sizeOfGrids; i++)
        {
            for (int j = 0; j < sizeOfGrids; j++)
            {
                if (findEnemyGrid(i, j).occupied == 0)
                {
                    //instantiate empty tile
                    Object obj = 
                        Instantiate(enemyTile, new Vector3(START_DIVIDE_ENEMY +
                        i * TILE_SPACE, (1 - j) * TILE_SPACE, 0), Quaternion.identity);
                    obj.name = "enemyTile" + i + j;
                }
                else
                {
                    //instantiate filled tile
                    Object obj =
                        Instantiate(enemyTileFilled, new Vector3(START_DIVIDE_ENEMY
                        + i * TILE_SPACE, (1 - j) * TILE_SPACE, 0), Quaternion.identity);
                    obj.name = "enemyTile" + i + j;
                }
            }
        }
    }

    public PlayerGrid findPlayerGrid(int horizPos, int vertPos)
    {
        Debug.Log(horizPos + ", " + vertPos);
        if (horizPos <= RIGHT_HORIZ && vertPos <= DOWN_VERT)
        {
            return playerSide[horizPos,vertPos];
        }
        else {
            Debug.Log("Error in the selection of a position");
            return null;
        }
    }

    public EnemyGrid findEnemyGrid(int horizPos, int vertPos)
    {
        if (horizPos <= RIGHT_HORIZ && vertPos <= DOWN_VERT)
        {
            return enemySide[horizPos, vertPos];
        }
        else
        {
            Debug.Log("Error in the selection of a position");
            return null;
        }
    }
}
