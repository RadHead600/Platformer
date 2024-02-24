using UnityEngine;

public class Shild : Unit
{
    [SerializeField] private int _armorQuantity;
    [SerializeField] private CharacterStatUI _characterStatUI;

    private void Start()
    {
        HP = _armorQuantity;
    }

    public void AddArmor(int quantity)
    {
        HpChange(HP + quantity);
    }

    public override int ReceiveDamage(int quantity)
    {
        HpChange(HP - quantity);
        if (HP <= 0)
            Die();
        return HP;
    }

    private void HpChange(int hp)
    {
        HP = hp;
        _characterStatUI.ChangeText(_characterStatUI.ArmorText, HP.ToString());
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }
}
