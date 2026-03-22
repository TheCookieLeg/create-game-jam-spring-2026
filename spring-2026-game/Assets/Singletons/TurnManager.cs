using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int enemyIndex = 0;

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

        enemy = Instantiate(enemyPrefab[enemyIndex]);
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
        StartCoroutine(newEncounter());
    }

    IEnumerator newEncounter()
    {
        yield return new WaitForSeconds(2);
        GUIManager.instance.ChangeText($"{enemyPrefab[enemyIndex].name} has been brutally slain...");

        enemyScript.onEnemyActionCompleted -= enemyEndTurn;
        enemyScript.onEnemyDeath -= enemyDead;
        GUIManager.instance.UnsubscribeFromEvents();

        yield return new WaitForSeconds(2);
        Destroy(enemy.gameObject);
        enemyIndex++;

        if (enemyIndex > 3)
        {
            GUIManager.instance.ChangeText("You did it... They're all finally gone...");
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        GUIManager.instance.ChangeText($"{enemyPrefab[enemyIndex].name} approaches you...");
        yield return new WaitForSeconds(3);
        enemy = Instantiate(enemyPrefab[enemyIndex]);

        if (enemy != null) {
            enemyScript = enemy.GetComponent<Enemy>();
        } else {
            Debug.LogWarning("Enemy was not found. Try instantiating a new enemy");
        }

        enemyScript.onEnemyActionCompleted += enemyEndTurn;
        enemyScript.onEnemyDeath += enemyDead;

        GUIManager.instance.UpdateEnemyReference();

        switchTurn?.Invoke(player);
        yield return new WaitForSeconds(1.2f);
        GUIManager.instance.enemyEndTurn(enemy);
    }


    public GameObject GetPlayerInstance() { return player; }
    public GameObject GetEnemyInstance() { return enemy; }

}
