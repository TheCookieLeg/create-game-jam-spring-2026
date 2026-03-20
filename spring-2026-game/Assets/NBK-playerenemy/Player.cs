using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int hp = 10;
    [SerializeField] private int damage = 5;
    //private Enemy enemy;
    //private TurnHandler turnHandler;
    //private GUIManager GUImanager;
    //private List<Debuff> debuffs = new List<Debuff>();
    //private List<Rune> inventory = new List<Rune>();

    public void onEncounterStart()
    {
        //Enemy = Turnhandler.instance.getEnemy();
    }

    public void startTurn()
    {
        //GUImanager.instance.startPlayer(int hp, List<Rune> inventory)
        //foreach(Debuff debuff in debuffs)
            //debuff.doDebuff
    }

    public void attack()
    {
        //enemy.takeDamage(damage, debuff)
    }

    public void takeDamage(int damage/*,  Debuff debuff */) 
    {
        hp -= damage;
        if (hp <= 0)
        {
            death();
        }
        //Debuffs.add(debuff)
    }

    private void endTurn()
    {
        //GUI
        //foreach(Debuff debuff in debuffs)
            //debuff.doDebuff()
            //if (debuff.turns <=0)
                //debuffs.remove(debuff)

        Debuff debuff = new Debuff(1,1);

        if (hp <= 0)
        {
            death();
        }
    }

    private void death()
    {

    }

}
