using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public GameObject Attackable { get; }

    public void AttackTarget(int damage);
}
