using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using FMODUnity;

public class Player : MonoBehaviour
{
    public event Action<GameObject> onPlayerActionCompleted;
    public event Action<GameObject> onPlayerDeath;

    [SerializeField] private int hp = 10;
    [SerializeField] private int damage = 5;

    private List<Debuff> debuffs = new List<Debuff>();
    public List<Rune> inventory = new List<Rune>();

    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        TurnManager.instance.switchTurn += startTurn;
    }

    private void OnDisable()
    {
        TurnManager.instance.switchTurn -= startTurn;
    }

    private void Start()
    {
        Debug.Log("Finding health");
        healthText = GameObject.FindWithTag("HealthText").GetComponent<TextMeshProUGUI>();

        if (healthText == null)
        {
            Debug.LogWarning("Couldnt find health :(");
            return;
        }

        healthText.text = $"Health: {hp}";
    }

    public void onEncounterStart()
    {
        // optional
    }

    public void startTurn(GameObject current)
    {
        if (this.gameObject != current) return;

        Debug.Log($"{this.gameObject.name} has started their turn");
        doDebuff();
    }

    public void attack(Debuff debuff)
    {
        Debug.Log("Attack with debuff: " + debuff.type);

        Enemy enemy = TurnManager.instance.GetEnemyInstance().GetComponent<Enemy>();

        // Normal attack
        if (debuff.type == 0)
        {
            RuntimeManager.PlayOneShot("event:/Player/player_attack");
            enemy.takeDamage(damage, debuff);
            endTurn();
            return;
        }

        // Heal rune
        if (debuff.type == 4)
        {
            RuntimeManager.PlayOneShot("event:/Player/player_spell_heal");
            GUIManager.instance.attackType(debuff);

            heal(debuff.damage);
            enemy.takeDamage(0, debuff);

            endTurn();
            return;
        }

        // Direct damage rune
        if (debuff.damage != 0 && debuff.turns == 0)
        {
            RuntimeManager.PlayOneShot("event:/Player/player_spell_damage");
            GUIManager.instance.attackType(debuff);

            enemy.takeDamage(damage + debuff.damage, debuff);

            endTurn();
            return;
        }

        // Stun / Weaken / fallback
        switch (debuff.type)
        {
            case 2:
                RuntimeManager.PlayOneShot("event:/Player/player_spell_stun");
                break;

            case 3:
                RuntimeManager.PlayOneShot("event:/Player/player_spell_weaken");
                break;

            default:
                RuntimeManager.PlayOneShot("event:/Player/player_spell_damage");
                break;
        }

        GUIManager.instance.attackType(debuff);
        enemy.takeDamage(damage, debuff);

        endTurn();
    }

    public void heal(int n)
    {
        hp += n;
        if (healthText != null)
        {
            healthText.text = $"Health: {hp}";
        }
    }

    public void takeDamage(int damage, Debuff debuff = null)
    {
        hp -= damage;

        if (healthText != null)
        {
            healthText.text = $"Health: {hp}";
        }

        if (hp <= 0)
        {
            death();
        }

        if (debuff != null)
        {
            debuffs.Add(debuff);
        }
    }

    public void endTurn()
    {
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
            debuff.turns--;
        }
    }
}