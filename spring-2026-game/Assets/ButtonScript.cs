using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = TurnManager.instance.GetPlayerInstance();

        Button button = GetComponent<Button>();
        //button.onClick.AddListener(player.GetComponent<Player>().attack);
    }

}
