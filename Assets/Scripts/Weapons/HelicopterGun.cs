
public class HelicopterGun : Weapons
{
    void OnEnable()
    {
        rechargeStart = recharge;
        magazineStart = magazine;
        delayStart = delay;
    }
}
