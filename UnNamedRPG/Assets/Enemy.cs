using UnityEngine;
using System.Collections;

public class Enemy {
    public int health;
    public int attack;
    public int defense;
    public int dexterity;
    public int magic;
    public bool moved;
    public bool attacked;
    public EnemyGrid position;

    public void hitPlayerRegular(Player player)
    {

    }

    public int getDex()
    {
        return dexterity;
    }
    public int getDef()
    {
        return defense;
    }
}

