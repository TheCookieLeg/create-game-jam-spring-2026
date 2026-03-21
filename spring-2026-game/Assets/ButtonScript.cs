using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int type = 0;

    IEnumerator Start()
    {
        while (TurnManager.instance == null || TurnManager.instance.GetPlayerInstance() == null)
        {
            var tm = TurnManager.instance;

            Debug.Log("Instance null? " + (tm == null));

            if (tm != null)
            {
                var player = tm.GetPlayerInstance();
                Debug.Log("Player null? " + (player == null));
            }
            yield return null; // wait one frame
        }

        player = TurnManager.instance.GetPlayerInstance();
        if (player == null) {Debug.LogWarning("KILL YOURSELF");}
        Button button = GetComponent<Button>();

        Debug.Log("HELLO??");

        switch (type)
        {
            case 0:
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.NoDebuff,0,0));
                });
                break;

            case 1:
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff(1,0,10));
                });
                break;
            
            case 2:
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Stun,2,1));
                });
                break;

            case 3:
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Weaken,2,1));
                });
                break;

            case 4:
                Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Heal,0,-4));
                });
                break;
        }
    }



}

