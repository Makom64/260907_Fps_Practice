using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int _damage;
    private float _speed;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 맞음");
        }
        
        Destroy(gameObject);
    }
    
    private void Update()
    {
        MoveForward();
    }

    public void SetData(int damage, float speed, float existTime)
    {
        _damage = damage;
        _speed = speed;
        
        Destroy(gameObject, existTime);
    }
    
    
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    
    /*
    private void GiveDamage()
    {
        
    }
    */
    
}
