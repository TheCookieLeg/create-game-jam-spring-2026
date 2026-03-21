using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private Sprite runeSprite;
    [SerializeField] private int damage;
    [SerializeField] private int debuffTurns;
    [SerializeField] private int debuffDamage;

    public Debuff debuff;



    [SerializeField] public TurnManager.Debuffs debuf;

    public void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = runeSprite;
        debuff = new Debuff((int) debuf, debuffTurns, debuffDamage);
    }
}
