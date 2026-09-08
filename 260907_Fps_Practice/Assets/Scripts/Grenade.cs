using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int damage; // 수류탄 데미지
    [SerializeField] private float _grenadeDelay;
    
    private Rigidbody _grenade; 
    private SphereCollider _grenadeCollider;
    private IDamageable _damageable;

    private void Awake()
    {
        _grenade = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Explode();
        GrenadeMove();
    }

    private void OnDestroy()
    {
        Debug.Log("수류탄 폭발");
        Debug.Log($"{damage}의 데미지");
    }

    private void Explode()
    {
        Destroy(this.gameObject,  _grenadeDelay);
    }

    private void GrenadeMove()
    {
        
    }
    
}
