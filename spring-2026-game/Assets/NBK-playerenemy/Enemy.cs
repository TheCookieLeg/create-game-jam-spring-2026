using System;
using System.Collections;
using System.Collections.Generic;
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
    private List<Debuff> debuffs = new List<Debuff>();
    private bool stunned = false;
    private bool weakend = false;

    private void OnEnable()
    {
        TurnManager.instance.switchTurn += startTurn;  
    }

    private void Start()
    {
        transform.name = transform.name.Replace("(clone)", "").Trim();
        isDead = false;

        PlaySpawnSound();
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
        //StartCoroutine(DelayedAttack(current));

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
        // optional
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
        endTurn();
    }

    private void death()
    {
        PlayDeathSound();
        onEnemyDeath?.Invoke(this.gameObject);
    }

    IEnumerator DelayedAttack(GameObject current)
    {
        if (isDead) { yield break; }

        yield return new WaitForSeconds(3);
        GUIManager.instance.ChangeText(this.normalAttackDesc);
        yield return new WaitForSeconds(3);
        attack();
    }

    private void PlaySpawnSound()
    {
        string eventPath = GetSpawnEventPath();
        if (!string.IsNullOrEmpty(eventPath))
        {
            RuntimeManager.PlayOneShot(eventPath);
        }
    }

    private void PlayAttackSound()
    {
        string eventPath = GetAttackEventPath();
        if (!string.IsNullOrEmpty(eventPath))
        {
            RuntimeManager.PlayOneShot(eventPath);
        }
    }

    private void PlayDeathSound()
    {
        string eventPath = GetDeathEventPath();
        if (!string.IsNullOrEmpty(eventPath))
        {
            RuntimeManager.PlayOneShot(eventPath);
        }
    }

    private string GetSpawnEventPath()
    {
        string enemyName = transform.name.ToLower();

        if (enemyName.Contains("gravso"))
            return "event:/Monsters/gravso_spawn";

        if (enemyName.Contains("ellepige"))
            return "event:/Monsters/ellepige_spawn";

        if (enemyName.Contains("lygtemand"))
            return "event:/Monsters/lygtemand_spawn";

        if (enemyName.Contains("mosekone"))
            return "event:/Monsters/mosekone_spawn";

        return "";
    }

}
