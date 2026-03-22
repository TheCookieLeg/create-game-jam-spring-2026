using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using static TurnManager;

public class Enemy : MonoBehaviour
{
    public event Action<GameObject> onEnemyActionCompleted;
    public event Action<GameObject> onEnemyDeath;

    [SerializeField] private int hp = 10;
    [SerializeField] private int damage = 5;
    [SerializeField] private string normalAttackDesc;
    [SerializeField] private string specialAttackDesc;

    [SerializeField] private int debuffTurns;
    [SerializeField] private int debuffDamage;
    [SerializeField] private TurnManager.Debuffs debuff;
    private bool isDead = false;
    private List<Debuff> debuffs = new List<Debuff>();
    private bool stunned = false;
    private bool weakend = false;

    //private TurnHandler turnHandler;
    //private GUIManager GUImanager;
    //private Player player;

    private void OnEnable()
    {
        TurnManager.instance.switchTurn += startTurn;  
         
    }

    private void Start()
    {
        transform.name = transform.name.Replace("(clone)", "").Trim();
        isDead = false;
    }

    private void OnDisable()
    {
        TurnManager.instance.switchTurn -= startTurn;
    }


    public void startTurn(GameObject current)
    {
        stunned = false;
        weakend = false;
        //GUImanager.instance.startPlayer(int hp, List<Rune> inventory)
        doDebuff();
        if (this.gameObject != current) {return;}

        Debug.Log($"{this.gameObject.name} has started their turn");
        //StartCoroutine(DelayedAttack(current));

        StartCoroutine(DelayedAttack(current));

        //attack();
    }

    public void startTurn()
    {
        //GUImanager.instance.startPlayer(int hp, List<Rune> inventory)
        //foreach(Debuff debuff in debuffs)
        //doDebuff();

        if (stunned)
        {
            endTurn();
            Debug.Log("enemy stunned2");
        }
    }

    public void onEncounterStart()
    {
        //player = turnHandler.instance.getPlayer();
    }

    public void takeDamage(int damage, Debuff debuff)
    {
        hp -= damage;
        if (debuff.type != 0 && debuff.damage != 0)
        {
            debuffs.Add(debuff);
        }
        if (hp <= 0)
        {
            isDead = true;
            death();
        }
    }

    private void endTurn()
    {
        //GUI
        //foreach(Debuff debuff in debuffs)
        //debuff.doDebuff()

        onEnemyActionCompleted?.Invoke(this.gameObject);
    }

    public void attack()
    {
        Debuff debuff = new Debuff((int)TurnManager.Debuffs.NoDebuff, 0);
        
        if (weakend)
        {
            TurnManager.instance.GetPlayerInstance().GetComponent<Player>().takeDamage(0, debuff);
            endTurn();
        }
        else
        {
            TurnManager.instance.GetPlayerInstance().GetComponent<Player>().takeDamage(damage, debuff);
            endTurn();
        }
    }
    
    public void specialAttack()
    {
        Debuff debuff = new Debuff((int)this.debuff, debuffTurns, debuffDamage);
        TurnManager.instance.GetPlayerInstance().GetComponent<Player>().takeDamage(damage, debuff);
        //GUIManager.instance.specialAttack();
        endTurn();
    }

    private void death()
    {
        onEnemyDeath?.Invoke(this.gameObject);
    }


    IEnumerator DelayedAttack(GameObject current)
    {
        if (isDead) {yield break;}
        yield return new WaitForSeconds(3);
        if (!stunned)
        {
            GUIManager.instance.ChangeText(this.normalAttackDesc);
            yield return new WaitForSeconds(3);
            attack();
        }
        else
        {
            endTurn();
        }
    
    }

    private void doDebuff()
    {
        foreach (Debuff debuff in debuffs)
        {
            Debug.Log("enemy " + debuff.type + debuff.turns + debuff.damage);
            switch (debuff.type)
            {
                case 1:
                    //poison
                    debuff.turns--;
                    break;
                case 2:
                    //stun
                    stunned = true;
                    debuff.turns--;
                    break;
                case 3:
                    //weaken
                    weakend = true;
                    debuff.turns--;
                    break;
                default:
                    debuff.turns--;
                    break;
            }
            /*if (debuff.turns <= 0)
            {
                debuffs.Remove(debuff);
            }*/
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            if (debuffs[i].turns <= 0)
            {
                debuffs.RemoveAt(i);
            }
        }
        /*foreach (Debuff debuff in debuffs)
        {
            if (debuff.turns <= 0)
            {
                debuffs.Remove(debuff);
            }
        }*/
    }

}
