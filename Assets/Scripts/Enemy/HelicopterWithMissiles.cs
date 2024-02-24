using UnityEngine;

public class HelicopterWithMissiles : Enemy
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject missiles;

    void Start()
    {
        Destroy(gameObject, 120);
        HP = hp;
    }

    private void Update()
    {
        Attack();
        Fly();

        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    private void Fly()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private void Attack()
    {
        Vector3 difference =  missiles.transform.position + new Vector3(-5, -5, 0);
        Weapons weaponAttack = GetComponentInChildren<Weapons>();

        weaponAttack.isEnemy = true;
        weaponAttack.Attack(difference);
    }

    public override int ReceiveDamage(int damage)
    {
        return HP -= damage;
    }
}
