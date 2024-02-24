using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected LayerMask blocks;

    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public Vector3 Direction { set => direction = value; }

    protected Vector3 direction;
    protected float speed;
    protected int damage;

    private void Start()
    {
        DestroyBullet();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Units unit = collider.GetComponentInChildren<Units>();

        if (collider.CompareTag("Head"))
        {
            unit = collider.GetComponentInParent<Units>();
            unit.ReceiveDamage(damage * 2);
            Destroy(gameObject);
        }
        else if (unit != null)
        {
            unit.ReceiveDamage(damage);
            Destroy(gameObject);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, blocks);

        if (colliders.Length > 0.8F)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void DestroyBullet()
    {
        Destroy(gameObject, 4);
    }
}
