using UnityEngine;

public class CharacterWeapon : Unit
{
    [SerializeField] private GameObject _hand;

    public Weapon Weapon => _weapon;
    public GameObject Hand => _hand;

    private Weapon _weapon;

    private void Start()
    {
        AddWeaponToCharacter();
    }

    private void AddWeaponToCharacter()
    {
        Vector3 weaponPosition = new Vector3(_hand.transform.position.x, _hand.transform.position.y, 0);
        if (SaveParameters.weaponsBuy is null)
        {
            _weapon = Instantiate(
                Resources.Load<Weapon>("WeaponsPrefabs/Gun"),
                weaponPosition,
                Quaternion.identity
                );
        }
        else
        {
            _weapon = Instantiate(
                SaveParameters.weaponsBuy[SaveParameters.weaponEquip],
                weaponPosition,
                Quaternion.identity
                );
        }
        _weapon.transform.parent = _hand.transform;
    }
}
