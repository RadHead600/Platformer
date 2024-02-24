using System.Collections;
using UnityEngine;

public class Shild : Units
{
    [SerializeField]
    private int armorQuantity;

    private int hp;

    private void Start()
    {
        HP = armorQuantity;
    }

    public IEnumerator ReceiveDamageArmor(int quantity)
    {
        hp = HP;
        yield return new WaitForSeconds(0.1f);

        if (hp > 100)
        {
            HP = 100;
        }

        if (hp - quantity <= 0)
        {
            HP = 0;
            gameObject.SetActive(false);
        }
    }

    public override int ReceiveDamage(int quantity)
    {
        StartCoroutine(ReceiveDamageArmor(quantity));
        return HP -= quantity;
    }
}
