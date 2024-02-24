using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField]
    internal float recharge;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float speed;

    [SerializeField]
    internal int magazine;

    [SerializeField]
    protected float delay;

    [SerializeField]
    internal float spread;

    [SerializeField]
    private GameObject posAttack;

    [SerializeField]
    private ResourcesBullet resourcesBulletLoad;

    internal bool isEnemy = false;

    protected float rechargeStart;
    protected int magazineStart;
    protected float delayStart;


    internal float timerRecharge;
    private float timerDelay;
    private int magazineReload;

    private enum ResourcesBullet
    {
        StandartBullet,
        ArmorPiercingBullet,
        ExplosiveBullet,
        Missiles
    }

    private void Start()
    {
        timerRecharge = rechargeStart;
        timerDelay = delayStart;
        magazineReload = magazineStart;
    }

    private void Update()
    {
        if(magazine <= 0)
        {
            timerDelay -= Time.deltaTime;
            if(timerDelay <= 0)
            {
                magazine = magazineReload;
                timerDelay = delayStart;
            }
        }
        else if(timerRecharge > 0)
        {
            timerRecharge -= Time.deltaTime;
        }
    }

    public virtual void Attack(Vector3 difference)
    {
        if (timerRecharge <= 0)
        {
            Bullet bullet = Resources.Load<Bullet>("BulletPrefabs/" + resourcesBulletLoad.ToString());

            Bullet newBullet = Instantiate(bullet, posAttack.transform.position, posAttack.transform.rotation);

            if(isEnemy)
                newBullet.gameObject.layer = LayerMask.NameToLayer("BulletEnemy");

            newBullet.Speed = speed;
            newBullet.Damage = damage;
            newBullet.Direction = newBullet.transform.right * (difference.x < 0 ? -1 : 1);
            timerRecharge = rechargeStart;

            ChangeNumBullets(-1);
        }
    }

    public int ChangeNumBullets(int quantity)
    {
        return magazine += quantity;
    }
}
