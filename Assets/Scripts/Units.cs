﻿using UnityEngine;

public abstract class Units : MonoBehaviour
{
    public int HP { get; set; }

    public virtual int ReceiveDamage(int damage)
    {
        return 0;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
