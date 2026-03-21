using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance {get; private set;}
    private GameObject player;
    private GameObject enemy;
    private Player playerScript;
    private Enemy enemyScript;

    void Awake() 
    {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }

        player = GameObject.FindWithTag("Player");
        if (player != null) { 
            playerScript = player.GetComponent<Player>();
        } else {
            Debug.LogWarning("Player was not found. Try instantiating a new player");
        }

        enemy = GameObject.FindWithTag("Enemy");
        if (enemy != null) {
            enemyScript = enemy.GetComponent<Enemy>();
        } else {
            Debug.LogWarning("Enemy was not found. Try instantiating a new enemy");
        }
    }

    private void OnEnable()
    {
        playerScript.onPlayerActionCompleted += PlayerEndTurn;
        playerScript.onPlayerDeath += playerDead;

        enemyScript.onEnemyActionCompleted += enemyEndTurn;
        enemyScript.onEnemyDeath += enemyDead;
    }

    private void OnDisable()
    {
        playerScript.onPlayerActionCompleted -= PlayerEndTurn;
        playerScript.onPlayerDeath -= playerDead;

        enemyScript.onEnemyActionCompleted -= enemyEndTurn;
        enemyScript.onEnemyDeath -= enemyDead;
    }



    private void PlayerEndTurn(GameObject player)
    {
        Debug.Log($"{player.name} has ended their turn");
    }

    private void playerDead(GameObject player)
    {
        Debug.Log($"{player.name} has died");
    }


    private void enemyEndTurn(GameObject enemy)
    {
        Debug.Log($"{enemy.name} has ended their turn");
    }

    private void enemyDead(GameObject enemy)
    {
        Debug.Log($"{enemy.name} has ended their turn");
    }


    public GameObject GetPlayerInstance() { return player; }
    public GameObject GetEnemyInstance() { return enemy; }

}
