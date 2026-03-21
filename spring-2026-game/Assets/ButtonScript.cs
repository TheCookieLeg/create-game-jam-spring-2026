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
            yield return null; // wait one frame
        }

        player = TurnManager.instance.GetPlayerInstance();
        Button button = GetComponent<Button>();

        switch (type)
        {
            case 0:
                //Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.NoDebuff,0,2));
                });
                break;

            case 1:
                //Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    button.interactable = false;
                    player.GetComponent<Player>().attack(new Debuff(1,0,10));
                });
                //button.interactable = false;
                break;
            
            case 2:
                //Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    button.interactable = false;
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Stun,2,1));
                });
                //button.interactable = false;
                break;

            case 3:
                //Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    button.interactable = false;
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Weaken,2,1));
                });
                //button.interactable = false;
                break;

            case 4:
                //Debug.Log($"Set {this.gameObject.name} to attack {type}");
                button.onClick.AddListener( () => {
                    button.interactable = false;
                    player.GetComponent<Player>().attack(new Debuff((int)TurnManager.Debuffs.Heal,0,-4));
                });
                //button.interactable = false;
                break;
        }
    }



}

