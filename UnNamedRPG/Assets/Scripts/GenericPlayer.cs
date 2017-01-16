using UnityEngine;
using System.Collections;

public class GenericPlayer : Player
{
    public GenericPlayer()
    {
        health = 100;
        attack = 10;
        defense = 8;
        dexterity = 7;
        magic = 5;
        moved = false;
        attacked = false;
        experience = 0;
        level = 1;
        combatZone = GameObject.Find("CombatArea").GetComponent<CombatArea>();
        position = combatZone.findPlayerGrid(CombatArea.CENTER_HORIZ,
                                             CombatArea.CENTER_VERT);
        position.fillPlayer(this);
    }

    public GenericPlayer(int level, int experience)
    {
        health = 100 + 30 * level;
        attack = 10 + level;
        defense = 8 + level / 3 * 2;
        dexterity = 7 + level * 3 / 2;
        magic = 5 + level / 3;
        moved = false;
        attacked = false;
        this.experience = experience;
        this.level = level;
        combatZone = GameObject.Find("CombatArea").GetComponent<CombatArea>();
        position = combatZone.findPlayerGrid(CombatArea.CENTER_HORIZ,
                                             CombatArea.CENTER_VERT);
        position.fillPlayer(this);

    }

    public GenericPlayer(int level, int experience, int horizPos, int vertPos)
    {
        health = 100 + 30 * level;
        attack = 10 + level;
        defense = 8 + level / 3 * 2;
        dexterity = 7 + level * 3 / 2;
        magic = 5 + level / 3;
        moved = false;
        attacked = false;
        this.experience = experience;
        this.level = level;
        combatZone = GameObject.Find("CombatArea").GetComponent<CombatArea>();
        position = combatZone.findPlayerGrid(horizPos,
                                             vertPos);
        position.fillPlayer(this);

    }



}
