using System;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance {get; private set;}

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject[] enemyPrefab;

    private GameObject player;
    private GameObject enemy;
    private Player playerScript;
    private Enemy enemyScript;
    private GameObject currentTurn;

    public event Action<GameObject> switchTurn;

    [System.Serializable]
    public enum Debuffs
    {
        NoDebuff,
        Poison,
        Stun,
        Weaken,
        Heal
    }

    void Awake() 
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        player = Instantiate(playerPrefab);
        if (player != null) { 
            playerScript = player.GetComponent<Player>();
        } else {
            Debug.LogWarning("Player was not found. Try instantiating a new player");
        }

        enemy = Instantiate(enemyPrefab[3]);
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
        switchTurn?.Invoke(enemy);
    }

    private void playerDead(GameObject player)
    {
        Debug.Log($"{player.name} has died");
    }


    private void enemyEndTurn(GameObject enemy)
    {
        Debug.Log($"{enemy.name} has ended their turn");
        switchTurn?.Invoke(player);
    }

    private void enemyDead(GameObject enemy)
    {
        Debug.Log($"{enemy.name} has died, oh no!");
    }


    public GameObject GetPlayerInstance() { return player; }
    public GameObject GetEnemyInstance() { return enemy; }

}
