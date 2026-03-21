using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int type = 0;

    void Start()
    {
        player = TurnManager.instance.GetPlayerInstance();
        if (player == null) {Debug.LogWarning("KILL YOURSELF");}
        Button button = GetComponent<Button>();

        switch (type)
        {
            case 0:
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.NoDebuff,0,0));
                });
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                break;

            case 1:
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff(1,0,10));
                });
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                break;
            
            case 2:
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Stun,2,1));
                });
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                break;

            case 3:
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Weaken,2,1));
                });
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                break;

            case 4:
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Heal,0,-4));
                });
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                break;
        }
    }



}

