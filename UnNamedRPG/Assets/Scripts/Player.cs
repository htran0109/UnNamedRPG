﻿using UnityEngine;
using System.Collections;

public class Player
{
    public int health;
    public int attack;
    public int defense;
    public int dexterity;
    public int magic;
    public int experience;
    public int level;
    public bool moved;
    public bool attacked;
    public PlayerGrid position;
    public CombatArea combatZone;



    public void hitEnemyRegular(Enemy enemy)
    {
        if (!this.attacked)
        {
            if (Random.Range(0f, 100f) - (enemy.getDex() - this.getDex()) >= 10)
            //determine if hit, base percentage is 90% at even dexterity
            {
                Debug.Log("Player hit");
                enemy.health -= Mathf.Max((this.attack -
                                           enemy.getDef()), 1) * 10; //calc damage
                if(enemy.health <= 0)
                {
                    enemy.position.removeEnemy(enemy);
                }
            }
            else
            {
                Debug.Log("Player missed");
            }
            attacked = true;

        }
        else
        {
            Debug.Log("Player has already attacked");
        }
    }

    public int getDex()
    {
        return dexterity;
    }
    public int getDef()
    {
        return defense;
    }
    public void resetTurn()
    {
        attacked = false;
        moved = false;
    }

    public bool moveTile(PlayerGrid newPos)
    {
        if (!moved)
        {
            if (Mathf.Abs(newPos.getHorizPos() - this.position.getHorizPos()) +
               Mathf.Abs(newPos.getVertPos() - this.position.getVertPos()) == 1)
            {
                this.position.removePlayer(this);
                newPos.fillPlayer(this);
                this.position = newPos;
                Debug.Log("Moved");
                moved = true;
                return true;
            }
            else
            {
                Debug.Log("Tile too far away for move!");
                return false;
            }

        }
        else
        {
            Debug.Log("Player already moved");
            return false;
        }
    }
}
