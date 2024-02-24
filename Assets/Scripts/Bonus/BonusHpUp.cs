using UnityEngine;

public class BonusHpUp : Bonus
{
    [SerializeField]
    private int hpAdd;

    protected override void GiveBonus()
    {
        unit.GetComponentInChildren<Units>().HP += hpAdd;
        Destroy(gameObject);
    }

}
