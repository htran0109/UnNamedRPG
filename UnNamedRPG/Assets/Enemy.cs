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
    public int experience;
    public int level;
    public EnemyGrid position;
    public CombatArea combatZone;

    public void hitPlayerRegular(Player player)
    {
        if (!this.attacked)
        {
            if (Random.Range(0f, 100f) - (player.getDex() - this.getDex()) >= 10)
            //determine if hit, base percentage is 90% at even dexterity
            {
                player.health -= Mathf.Max((this.attack -
                                           player.getDef()), 1) * 10; //calc damage
            }
            else
            {
                Debug.Log("Player missed");
                attacked = true;
            }

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

    public bool moveTile(EnemyGrid newPos)
    {
        if (!moved)
        {
            if (Mathf.Abs(newPos.getHorizPos() - this.position.getHorizPos()) +
               Mathf.Abs(newPos.getVertPos() - this.position.getVertPos()) == 1)
            {
                this.position.removeEnemy(this);
                newPos.fillEnemy(this);
                this.position = newPos;
                Debug.Log("Moved");
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

