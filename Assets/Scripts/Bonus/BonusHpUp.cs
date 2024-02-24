using UnityEngine;

public class BonusHpUp : Bonus
{
    [SerializeField] private int _hpAdd;

    protected override void GiveBonus()
    {
        unit.HP += _hpAdd;
        Destroy(gameObject);
    }

}
