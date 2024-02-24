using System.Collections;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterWeapon _characterWeapon;
    [SerializeField] private int _minBulletInMagazine = 0;
    [SerializeField] private CharacterStatUI _characterStatUI;

    public IEnumerator Attack()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (_characterWeapon.Weapon.BulletInMagazine <= _characterWeapon.Weapon.WeaponParameters.MinBulletInMagazine)
        {
            _characterWeapon.Weapon.BulletInMagazine = _characterWeapon.Weapon.WeaponParameters.BulletInmagazine;
            yield return new WaitForSeconds(_characterWeapon.Weapon.WeaponParameters.RechargeTime);
            yield break;
        }

        _characterWeapon.Hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + _character.Offset + Random.Range(_characterWeapon.Weapon.WeaponParameters.Spread * -1, _characterWeapon.Weapon.WeaponParameters.Spread));
        _characterWeapon.Weapon.Attack(difference);
        _characterStatUI.ChangeText(_characterStatUI.BulletsText, _characterWeapon.Weapon.BulletInMagazine.ToString());
        yield return new WaitForSeconds(_characterWeapon.Weapon.WeaponParameters.Delay);
    }
}
