using System.Collections;
using UnityEngine;

public class EnemyAttack : Enemy
{
    [SerializeField] private RotateWeapon _rotateWeapon;
    [SerializeField] private GameObject _hand;
    [SerializeField] private Weapon _weapon;

    private float _offset;
    private Vector3 _difference;
    private Character _character;
    private WeaponParameters _weaponParameters;
    private Coroutine _attackCoroutine;

    private void Start()
    {
        _weaponParameters = _weapon.WeaponParameters;

        if (_character == null)
            _character = FindObjectOfType<Character>();

        _attackCoroutine = StartCoroutine(Attack());
    }

    private void FixedUpdate()
    {
        _difference = _character.transform.position - transform.position;
        _rotateWeapon.WeaponRotate(ref _offset, _difference, _hand);
    }

    public void SwapFace()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator Attack()
    {
        if (_weapon.BulletInMagazine <= _weaponParameters.MinBulletInMagazine)
        {
            _weapon.BulletInMagazine = _weaponParameters.BulletInmagazine;
            yield return new WaitForSeconds(_weapon.WeaponParameters.RechargeTime);
        }

        _weapon.Attack(_difference);
        yield return new WaitForSeconds(_weapon.WeaponParameters.Delay);
        _attackCoroutine = StartCoroutine(Attack());
    }
}