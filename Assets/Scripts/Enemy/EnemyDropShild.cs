using UnityEngine;

public class EnemyDropShild : EnemyShoot
{
    [SerializeField]
    private GameObject shild;

    public override void Die()
    {
        SaveParameters.levelPoints[SaveParameters.levelActive] += points;
        Instantiate(shild, gameObject.transform.position, shild.transform.rotation);
        Destroy(gameObject);
    }
}
