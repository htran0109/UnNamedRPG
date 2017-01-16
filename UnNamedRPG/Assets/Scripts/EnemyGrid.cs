using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGrid : MonoBehaviour {

    private const int LEFT_HORIZ = 0;
    private const int CENTER_HORIZ = 1;
    private const int RIGHT_HORIZ = 2;
    private const int UP_VERT = 0;
    private const int CENTER_VERT = 1;
    private const int DOWN_VERT = 2;
    private const int NORMAL_FLOOR = 0;
    public int occupied = 0;
    private int horizPos = 0;
    private int vertPos = 0;
    private int floorType = 0;
    public List<Enemy> enemiesOnTile;

    public EnemyGrid(int horiz, int vert) //make a grid tile on the given horizontal
                                           //and vertical coordinates
    {
        occupied = 0;
        floorType = 0;
        horizPos = horiz;
        vertPos = vert;
        enemiesOnTile = new List<Enemy>();

    }

    public EnemyGrid(int horiz, int vert, int type)
    {
        occupied = 0;
        this.floorType = type;
        horizPos = horiz;
        vertPos = vert;
        enemiesOnTile = new List<Enemy>();
    }

    public void fillEnemy(Enemy enemy)
    {
        enemiesOnTile.Add(enemy);
        occupied++;
    }

    public bool removeEnemy(Enemy enemy)
    {
        if (enemiesOnTile.Contains(enemy)) {
            enemiesOnTile.Remove(enemy);
            occupied--;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int getHorizPos()
    {
        return horizPos;
    }

    public int getVertPos()
    {
        return vertPos;
    }


}
