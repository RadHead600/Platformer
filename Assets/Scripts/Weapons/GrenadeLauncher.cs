
public class GrenadeLauncher : Weapons
{
    void OnEnable()
    {
        rechargeStart = recharge;
        magazineStart = magazine;
        delayStart = delay;
    }
}
