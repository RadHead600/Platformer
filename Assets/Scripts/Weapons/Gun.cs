
public class Gun : Weapons
{
    void OnEnable()
    {
        rechargeStart = recharge;
        magazineStart = magazine;
        delayStart = delay;
    }
}
