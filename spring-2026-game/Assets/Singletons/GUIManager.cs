using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance {get; private set;}
    private GameObject player;
    private GameObject enemy;
    private Player playerScript;
    private Enemy enemyScript;

    [SerializeField] private GameObject actionBar;
    [SerializeField] private TextMeshProUGUI attackDesc;
    [SerializeField] private Transform[] runePositions;
    private Debuff debuff;
    private string specialAtkDesc;


    IEnumerator Start() 
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        while (TurnManager.instance == null || TurnManager.instance.GetPlayerInstance() == null)
        {
            yield return null; // wait one frame
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
        if (player == null) {return;}
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
        StartCoroutine(EndPlayerTurnWithDelay(player));

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
        Debug.Log("HELLO??");
        attackDesc.gameObject.SetActive(false);
        actionBar.SetActive(true);
        if (specialAtkDesc != null)
        {
            attackDesc.text = specialAtkDesc;
        }

        //for(int i = 0; i <= playerScript.inventory.Count; i++)
            //Instantiate(playerScript.inventory[i], runePositions[i]);
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
        Debug.Log("ATTACK TYPE HERE");
        this.debuff = debuff;
        Debug.Log($"Switched GUI debuff to: {debuff.type}");
    }

    public void specialAttack(string Desc)
    {
        specialAtkDesc = Desc;
    }

    public void ChangeText(GameObject current)
    {
        attackDesc.text = $"{current.name} attacks you";
    }


    IEnumerator EndPlayerTurnWithDelay(GameObject player)
    {
        Debug.Log($"GUI: {player.name} ended their turn");
        actionBar.SetActive(false);
        attackDesc.gameObject.SetActive(true);

        Debug.Log("Debuff status: " + (debuff != null));
        //Debug.Log($"In the enumerator, the debuff type is {debuff.type}");
        

        if (debuff != null)
        {
            attackDesc.text = "you activate a rune and ";
            Debug.Log("First text changed");
            switch (debuff.type)
            {
                case (int)TurnManager.Debuffs.Poison:
                    attackDesc.text += $"dealt {debuff.damage} damage to the enemy";
                    break;
                case (int)TurnManager.Debuffs.Stun:
                    attackDesc.text += "stunned the enemy";
                    break;
                case (int)TurnManager.Debuffs.Weaken:
                    attackDesc.text += "weakened the enemy";
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
        yield return new WaitForSeconds(2);
    }
}
