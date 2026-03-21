using System;
using System.Collections;
using Unity.Jobs;
using UnityEngine;

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
        //GUImanager.instance.startPlayer(int hp, List<Rune> inventory)
        if (this.gameObject != current) {return;}

        Debug.Log($"{this.gameObject.name} has started their turn");
        StartCoroutine(DelayedAttack(current));
        //attack();
    }

    public void startTurn()
    {
        //GUImanager.instance.startPlayer(int hp, List<Rune> inventory)
        //foreach(Debuff debuff in debuffs)
        //debuff.doDebuff
    }

    public void onEncounterStart()
    {
        //player = turnHandler.instance.getPlayer();
    }

    public void takeDamage(int damage, Debuff debuff)
    {
        hp -= damage;
        //Debuffs.add(debuff)
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
        TurnManager.instance.GetPlayerInstance().GetComponent<Player>().takeDamage(damage, debuff);
        endTurn();
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
        GUIManager.instance.ChangeText($"{current.name} attacks you");
        yield return new WaitForSeconds(3);
        attack();
    
    }

}
