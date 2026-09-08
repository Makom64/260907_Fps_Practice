using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int damage; // 수류탄 데미지
    [SerializeField] private float _grenadeDelay; // 이 시간에 도달하면 수류탄 터짐
    private SphereCollider _collider;
    
    private IDamageable damageable; // 데미지 입히기 위해 담아줄 변수
    private float _delayCount;
    private bool _timeToExplode {get{return _delayCount >= _grenadeDelay;}} // 


    private void Start()
    {
        Debug.Log(_delayCount);
    }

    private void Update()
    {
        DelayCount();
        Explode();
    }

    private void OnDestroy()
    {
        damageable?.TakeDamage(damage);
    }

    private void DelayCount()
    {
        _delayCount += Time.deltaTime;
    }

    private void Explode()
    {
        if (!_timeToExplode)
        {
            return;
        }
        
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() == null)
        {
            return;
        }
        damageable = other.GetComponent<IDamageable>();
        Debug.Log("3초 뒤에 수류탄 터집니다~");
    }
}
