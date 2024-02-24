using UnityEngine;

public class ShildDrop : Bonus
{
    [SerializeField]
    private int armorAdd;

    protected override void GiveBonus()
    {
        if (unit is Character)
        {
            ((Character)unit).AddShild(armorAdd);
            Destroy(gameObject);
        }
    }

}
