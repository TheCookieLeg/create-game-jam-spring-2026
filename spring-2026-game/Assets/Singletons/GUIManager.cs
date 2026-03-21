using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance {get; private set;}
    private GameObject player;
    private GameObject enemy;
    private Player playerScript;
    private Enemy enemyScript;

    void Start() 
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

        playerScript.onPlayerActionCompleted += playerEndTurn;
        playerScript.onPlayerDeath += playerDead;

        enemyScript.onEnemyActionCompleted += enemyEndTurn;
        enemyScript.onEnemyDeath += enemyDead;
    }


    private void OnEnable()
    {


        
    }

    private void OnDisable()
    {
        playerScript.onPlayerActionCompleted -= playerEndTurn;
        playerScript.onPlayerDeath -= playerDead;

        enemyScript.onEnemyActionCompleted -= enemyEndTurn;
        enemyScript.onEnemyDeath -= enemyDead;
    }

    private void playerEndTurn(GameObject player)
    {
        Debug.Log($"GUI: {player.name} ended their turn");
    }

    private void playerDead(GameObject player)
    {
        Debug.Log($"GUI: {player.name} died");
    }


    private void enemyEndTurn(GameObject enemy)
    {
        Debug.Log($"GUI: {enemy.name} ended their turn");
    }

    private void enemyDead(GameObject enemy)
    {
        Debug.Log($"GUI: {enemy.name} died");
    }
}
