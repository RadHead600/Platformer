using UnityEngine;

public class ArmorPiercingBullets : Bullet
{
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    protected new void OnTriggerEnter2D(Collider2D collider)
    {
        Units unit = collider.GetComponentInChildren<Units>();

        if (collider.CompareTag("Head"))
        {
            unit = collider.GetComponentInParent<Units>();
            unit.ReceiveDamage(damage * 2);
        }
        else if (unit)
        {
            unit.ReceiveDamage(damage);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, blocks);

        if (colliders.Length > 0.8F)
        {
            Destroy(gameObject);
        }
    }

    protected override void DestroyBullet()
    {
        Destroy(gameObject, 5);
    }
}
