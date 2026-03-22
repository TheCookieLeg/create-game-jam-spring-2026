using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Animate : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(triggerAnimation);
    }
    public void triggerAnimation()
    {
        GetComponent<Animator>().SetTrigger("trigger");
    }
}
