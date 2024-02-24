using UnityEngine;

public abstract class Enemy : Units
{
    [SerializeField]
    protected GameObject player;

    [SerializeField]
    internal int points;

    protected int damage;

    public int Damage { get => damage; set => damage = value; }

    public override void Die()
    {
        SaveParameters.levelPoints[SaveParameters.levelActive] += points;
        Destroy(gameObject);
    }

}
