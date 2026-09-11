using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableMovement : MonoBehaviour
{
    // 수류탄이 날아가는 방향
    // 현재 위치 > 카메라가 바라보고 있는 방향
    private Rigidbody _throwableRB;

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        
    }

    private void Throw()
    {
        
    }
    
    private void Explode()
    {
        Destroy(gameObject, 3f);
    }
    
    private void CacheComponents()
    {
        _throwableRB = GetComponent<Rigidbody>();
    }
}
