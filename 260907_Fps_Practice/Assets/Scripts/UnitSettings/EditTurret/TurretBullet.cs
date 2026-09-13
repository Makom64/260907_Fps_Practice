using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour, IPoolable
{
    private Rigidbody _bulletRB;
    
    public ObjectPool Pool { get; set; }
    public Transform poolableTransform { get; }

    private void Awake()
    {
        CacheComponents();
    }

    private void CacheComponents()
    {
        _bulletRB = GetComponent<Rigidbody>();
    }

    public void ReturnToPool()
    {
        
    }
}
