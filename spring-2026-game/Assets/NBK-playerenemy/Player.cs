using System;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public event Action<GameObject> onPlayerActionCompleted;
    public event Action<GameObject> onPlayerDeath;
    // Event for player death?

    [SerializeField] private int hp = 10;
    [SerializeField] private int damage = 5;
    //private Enemy enemy;
    //private TurnHandler turnHandler;
    //private GUIManager GUImanager;
    private List<Debuff> debuffs = new List<Debuff>();
    public List<Rune> inventory = new List<Rune>();

    private void OnEnable()
    {
        TurnManager.instance.switchTurn += startTurn;
        
    }

    private void OnDisable()
    {
        TurnManager.instance.switchTurn -= startTurn;
    }

    public void onEncounterStart()
    {
        //Enemy = Turnhandler.instance.getEnemy();
    }

    public void startTurn(GameObject current)
    {
        //GUImanager.instance.startPlayer(int hp, List<Rune> inventory)
        if (this.gameObject != current) {return;}

        Debug.Log($"{this.gameObject.name} has started their turn");

        doDebuff();
    }

    public void attack(Debuff debuff)
    {
        Debug.Log("Attack with the debuff: " + debuff.type);
        Enemy enemy = TurnManager.instance.GetEnemyInstance().GetComponent<Enemy>();

        if (debuff.type == 0)
        {
            enemy.takeDamage(damage, debuff);
            endTurn();
            return;
        }

        if (debuff.damage != 0 && debuff.turns == 0)
        {
            GUIManager.instance.attackType(debuff);
            enemy.takeDamage(damage + debuff.damage, debuff);
            
            endTurn();
            return;
        }
        
        GUIManager.instance.attackType(debuff);
        enemy.takeDamage(damage, debuff);
        endTurn();

        
    }

    public void takeDamage(int damage,  Debuff debuff = null) 
    {
        hp -= damage;
        if (hp <= 0)
        {
            death();
        }
        if (debuff != null)
        {
            debuffs.Add(debuff);
        }
        //Debuffs.add(debuff)
    }

    public void endTurn()
    {
        //GUI
        //foreach(Debuff debuff in debuffs)
            //debuff.doDebuff()
            //if (debuff.turns <=0)
                //debuffs.remove(debuff)
    

        if (hp <= 0)
        {
            death();
        }

        onPlayerActionCompleted?.Invoke(this.gameObject);

    }

    private void death()
    {
        onPlayerDeath?.Invoke(this.gameObject);
    }

    private void doDebuff()
    {
        foreach (Debuff debuff in debuffs)
        {
            switch (debuff.type)
            {
                case 1:
                    //poison
                    debuff.turns--;
                    break;
                case 2:
                    //stun
                    debuff.turns--;
                    break;
                case 3:
                    //weaken
                    debuff.turns--;
                    break;
                default:
                    debuff.turns--;
                    break;
            }
        }
    }

    /* -= TEMPORARY BELOW =- */

}
