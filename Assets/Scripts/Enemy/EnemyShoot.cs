using System;
using System.Collections;
using UnityEngine;

public class EnemyShoot : Enemy, IWeaponRotate
{
    [SerializeField]
    private Transform hand;

    [SerializeField]
    private Transform body;

    [SerializeField]
    private int hp;

    private int offset;

    private Rigidbody2D rigidBody;

    private Vector3 difference;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if(player == null)
        {
            player = FindObjectOfType<Character>().gameObject;
        }
        HP = hp;
        StartCoroutine(Attack());
    }

    private void Update()
    {
            RotateWeapons();
    }

    public void RotateWeapons()
    {
        difference = player.transform.position - transform.position;
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + offset);

        Vector3 pos = body.transform.localScale;
        body.transform.localScale = new Vector3(
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

    public void SwapFace()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(gameObject.GetComponentInChildren<Weapons>().recharge);

        gameObject.GetComponentInChildren<Weapons>().isEnemy = true;
        gameObject.GetComponentInChildren<Weapons>().Attack(difference);

        StartCoroutine(Attack());
    }

    private IEnumerator HpCheck(int damage)
    {
        yield return new WaitForSeconds(0.1f);
        if(HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public override int ReceiveDamage(int damage)
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(transform.up * 2.0F, ForceMode2D.Impulse);
        StartCoroutine(HpCheck(damage));
        return HP -= damage;
    }
}