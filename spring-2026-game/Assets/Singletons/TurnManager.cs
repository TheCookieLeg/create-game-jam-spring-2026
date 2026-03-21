using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance {get; private set;}
    private Player player;
    private Enemy enemy;

    void Awake() 
    {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player == null) { Debug.LogError("Player is null"); }
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        if (enemy == null) { Debug.LogError("Enemy is null"); }
    }

    private void OnEnable()
    {
        player.onPlayerActionCompleted += PlayerEndTurn;
    }

    private void OnDisable()
    {
        player.onPlayerActionCompleted -= PlayerEndTurn;
    }



    private void PlayerEndTurn(Player player)
    {
        Debug.Log($"{player} has ended their turn");
    }




    public Player GetPlayerInstance() { return player; }
    public Enemy GetEnemyInstance() { return enemy; }

}
