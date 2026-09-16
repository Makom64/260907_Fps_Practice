using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretBullet : MonoBehaviour, IPoolable
{
    [field : SerializeField] public int damage { get; private set; }
    private Rigidbody _rb;
    public WaitForSeconds _returnTime;
    public float _timeLimit { get; set; }
    
    public ObjectPool Pool { get; set; } // 이 총알이 돌아갈 오브젝트 풀
    public Transform poolableTransform { get => transform; } // { get; }
    public Turret shootedturret;
    public Status turretStatus;

    private void Awake()
    {
        CacheComponents();
        _returnTime = new WaitForSeconds(_timeLimit);
    }
    
    private void Update()
    {
        ShootBullet();
    }

    public void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
        shootedturret.AttackTarget(damage * turretStatus._damage);
    }

    private void CacheComponents()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void ShootBullet()
    {
        _rb.velocity = transform.forward * 1f;
    }

    public IEnumerator Returnroutine()
    {
        yield return _returnTime;
        ReturnToPool();
        Debug.Log(Time.time);
    }
    
    public void ReturnToPool()
    {
        poolableTransform.gameObject.SetActive(false);
        Pool.TurretBulletReturn(this);
    }
}
