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
    private Debuff debuff;
    private string specialAtkDesc;


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
        actionBar.SetActive(false);
        attackDesc.gameObject.SetActive(true);


        if (debuff != null)
        {
            attackDesc.text = "you activate a rune and ";
            switch (debuff.type)
            {
                case (int)TurnManager.Debuffs.Poison:
                    attackDesc.text += "poisoned the enemy";
                    break;
                case (int)TurnManager.Debuffs.Stun:
                    attackDesc.text += "stunned the enemy";
                    break;
                case (int)TurnManager.Debuffs.Weaken:
                    attackDesc.text += "weakend the enemy";
                    break;
                case (int)TurnManager.Debuffs.Heal:
                    attackDesc.text += "healed yourself";
                    break;
                default:
                    break;
            }
            debuff = null;
        }
        else
        {
            attackDesc.text = "you hit";
        }



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
        if (specialAtkDesc != null)
        {
            attackDesc.text = specialAtkDesc;
        }

        for(int i = 0; i <= playerScript.inventory.Count; i++)
            Instantiate(playerScript.inventory[i], runePositions[i]);
    }

    private void enemyDead(GameObject enemy)
    {
        Debug.Log($"GUI: {enemy.name} died");

        actionBar.SetActive(false);
        attackDesc.gameObject.SetActive(true);

        attackDesc.text = "enemy dead";
    }

    public void attackType(Debuff debuff)
    {
        debuff = this.debuff;
    }

    public void specialAttack(string Desc)
    {
        specialAtkDesc = Desc;
    }
}
