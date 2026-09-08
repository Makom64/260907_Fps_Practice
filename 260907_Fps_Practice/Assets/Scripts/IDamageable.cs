using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public GameObject Damageables { get; }
    
    public void TakeDamage(int damage);

    public void DeathMessage();
}
