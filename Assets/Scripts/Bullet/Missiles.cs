    using UnityEngine;

public class Missiles : ExplosiveBullets, IGrounded
{
    private bool stopRun;

    private void Start()
    {
        Destroy(gameObject, 10.0F);
    }

    void Update()
    {
        if (!stopRun)
        {
            Vector3 position = transform.position; position.z = 20.0F;
            transform.position = Vector3.MoveTowards(transform.position, (transform.position - direction), speed * Time.deltaTime);
            Grounded();

        }   
    }

    public override void Grounded()
    {
        Vector3 position = transform.position; position.x = -1.0F;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.8f, blocks);

        if (colliders.Length > 0.8F)
        {
            stopRun = true;
            StartCoroutine(CreateExplosion());
            Destroy(gameObject, 3f);
        }
    }
}
