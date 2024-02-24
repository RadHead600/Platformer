
public class SniperRifle : Weapons
{
    void OnEnable()
    {
        rechargeStart = recharge;
        magazineStart = magazine;
        delayStart = delay;
    }
}
