using System;
using UnityEngine;

public class HelicopterMachineGun : Enemy, IWeaponRotate
{
    [SerializeField]
    private GameObject machineGun;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int hp;

    private int offset;

    private Vector3 difference;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Character>().gameObject;
        }
        Destroy(gameObject, 120);
        HP = hp;
    }

    private void Update()
    {
        RotateWeapons();
        Fly();
        Attack();

        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public void RotateWeapons()
    {
        difference = player.transform.position - machineGun.transform.position;
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        machineGun.transform.rotation = Quaternion.Euler(0f, 0f, rotate + offset);

        Vector3 pos = machineGun.transform.localScale;
        machineGun.transform.localScale = new Vector3(
            (difference.x < 0 ? Math.Abs(pos.x) * -1 : Math.Abs(pos.x)) * (transform.localScale.x > 0 ? -1 : 1),
            pos.y,
            pos.z
            );

        if (pos.x < 0)
        {
            offset = 0;
            if (transform.localScale.x < 0)
                offset = -180;
        }
        else
        {
            offset = -180;
            if (transform.localScale.x < 0)
                offset = 0;
        }

    }

    private void Attack()
    {
        Weapons weaponAttack = GetComponentInChildren<Weapons>();

        weaponAttack.isEnemy = true;
        weaponAttack.Attack(difference);
    }

    private void Fly()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    public override int ReceiveDamage(int damage)
    {
        return HP -= damage;
    }

}
