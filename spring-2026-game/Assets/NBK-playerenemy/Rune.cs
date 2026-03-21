using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private Sprite runeSprite;
    [SerializeField] private int damage;
    [SerializeField] private int debuffTurns;
    [SerializeField] private int debuffDamage;

    public Debuff debuff;

    [System.Serializable]
    public enum Debuffs
    {
        NoDebuff,
        Poison,
        Stun,
        Weaken,
        Heal
    }


    [SerializeField] public Debuffs debuf;

    public void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = runeSprite;
        debuff = new Debuff((int) debuf, debuffTurns, debuffDamage);
    }
}
