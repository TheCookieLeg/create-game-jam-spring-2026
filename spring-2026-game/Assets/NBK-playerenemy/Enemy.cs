using System;
using Unity.Jobs;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<GameObject> onEnemyActionCompleted;
    public event Action<GameObject> onEnemyDeath;

    [SerializeField] private int hp = 10;
    [SerializeField] private int damage = 5;
    //private TurnHandler turnHandler;
    //private GUIManager GUImanager;
    //private Player player;

    private void OnEnable()
    {
        TurnManager.instance.switchTurn += startTurn;   
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
        attack();
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

    private void death()
    {
        onEnemyDeath?.Invoke(this.gameObject);
    }

}
