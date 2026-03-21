using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance {get; private set;}
    private GameObject player;
    private GameObject enemy;
    private Player playerScript;
    private Enemy enemyScript;

    [SerializeField] private GameObject actionBar;
    [SerializeField] private TextMeshPro attackDesc;
    [SerializeField] private Transform[] runePositions;

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


        playerScript.onPlayerActionCompleted += playerEndTurn;
        playerScript.onPlayerDeath += playerDead;

        enemyScript.onEnemyActionCompleted += enemyEndTurn;
        enemyScript.onEnemyDeath += enemyDead;
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
        actionBar.SetActive(false);
        attackDesc.gameObject.SetActive(true);
        
        //if statement, rune attack eller normal attack

        //Enable Enemy action
    }

    private void playerDead(GameObject player)
    {
        Debug.Log($"GUI: {player.name} died");
        actionBar.SetActive(false);
        attackDesc.gameObject.SetActive(true);
        attackDesc.text = "you dead";
    }


    private void enemyEndTurn(GameObject enemy)
    {
        Debug.Log($"GUI: {enemy.name} ended their turn");

        attackDesc.gameObject.SetActive(false);
        actionBar.SetActive(true);

        //for(int i = 0; i <= playerScript.inventory.length(); i++)
            //instantiate(playerScript.inventory.get(i), runePositions[i]);
    }

    private void enemyDead(GameObject enemy)
    {
        Debug.Log($"GUI: {enemy.name} died");

        actionBar.SetActive(false);
        attackDesc.gameObject.SetActive(true);

        attackDesc.text = "enemy dead";
    }
}
