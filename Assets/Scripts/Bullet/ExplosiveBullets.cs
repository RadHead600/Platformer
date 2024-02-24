using System.Collections;
using UnityEngine;

public class ExplosiveBullets : Bullet, IGrounded
{
    [SerializeField]
    protected float radius;

    [SerializeField]
    private Sprite switchSprite;

    public float Offset
    {
        get => offset;
        set => offset = value;
    }

    private float offset;

    private Rigidbody2D rigidBody;
    private bool stopRun = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10.0F);
    }

    void Update()
    {
        if (!stopRun)
        {
            gameObject.GetComponent<ParticleSystem>().Play(false);
            Vector3 position = transform.position; position.z = 20.0F;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            if (direction.x > 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, -430f)), Time.deltaTime);
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 70f)), Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, -0.5f);
            }
        }

        Grounded();
    }

    public virtual void Grounded()
    {
        Vector3 position = transform.position; position.x = -1.0F;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, blocks);

        if (colliders.Length > 0.8F && gameObject.GetComponent<CircleCollider2D>() == null)
        {
            stopRun = true;
            StartCoroutine(AddCollider());
            Destroy(gameObject, 3f);
        }
    }

    private IEnumerator AddCollider()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(CreateExplosion());
        yield return new WaitForSeconds(0.2f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Units unit = collision.GetComponentInChildren<Units>();
        if (unit)
        {
            unit.ReceiveDamage(damage);
        }
    }

    protected IEnumerator CreateExplosion()
    {
        gameObject.GetComponent<ParticleSystem>().Play(true);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.AddComponent<CircleCollider2D>().radius = 0;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponentInChildren<SpriteRenderer>().sprite = switchSprite;
        transform.rotation = Quaternion.Euler(0, 0, -180);

        for (float q = 1f; q < radius; q += .1f)
        {
            GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(q, q, 1);
            gameObject.GetComponent<CircleCollider2D>().radius = q;
            yield return new WaitForSeconds(.009f);
        }
    }
}
