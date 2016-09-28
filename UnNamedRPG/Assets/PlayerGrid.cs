using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGrid {

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
    public List<Player> playersOnTile;

    public PlayerGrid(int horiz, int vert) //make a grid tile on the given horizontal
                                           //and vertical coordinates
    {
        occupied = 0;
        floorType = 0;
        horizPos = horiz;
        vertPos = vert;
        playersOnTile = new List<Player>();

    }

    public PlayerGrid(int horiz, int vert, int type)
    {
        occupied = 0;
        this.floorType = type;
        horizPos = horiz;
        vertPos = vert;
        playersOnTile = new List<Player>();
    }

    public void fillPlayer(Player player)
    {
        playersOnTile.Add(player);
        occupied++;
    }

    public bool removePlayer(Player player)
    {
        if (playersOnTile.Contains(player))
        {
            playersOnTile.Remove(player);
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