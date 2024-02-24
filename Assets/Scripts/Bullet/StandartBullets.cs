using UnityEngine;

public class StandartBullets : Bullet
{
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

}
