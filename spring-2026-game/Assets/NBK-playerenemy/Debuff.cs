using UnityEngine;

public class Debuff
{
    public int type;
    public int turns;
    public int damage;
    public void doDebuff()
    {
        turns--;
    }
    public Debuff(int type, int turns, int damage = 0)
    {
        this.type = type;
        this.turns = turns;
        this.damage = damage;
    }



}
