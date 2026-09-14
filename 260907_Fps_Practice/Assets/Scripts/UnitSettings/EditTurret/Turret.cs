using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 터렛이 하는 역할
// 1. 터렛의 전체적인 시스템을 관리한다.
//    1.1. 터렛의 대가리 회전이나 공격하는 기능을 담당한다.
// 2. 자식인 트리거가 감지/판별 한 값들을 받아와서 행동을 수행한다.

// 터렛은 공격, 피해 가능한 객체이다.
public class Turret : MonoBehaviour, IDamageable, IAttackable
{
    // 터렛의 고유 능력치들 ------------------------------------------------------
    // 터렛의 머리
    [field : SerializeField] public GameObject _turretHead { get; private set; }
    // 터렛의 몸통
    [field : SerializeField] public GameObject _turretBody { get; private set; }
    // 터렛의 총구 위치
    [field : SerializeField] public Transform _muzzlePoint { get; private set; }
    // 터렛의 회전 속도
    [field : SerializeField] public float _headRotateSpeed { get; private set; }
    // 터렛이 탐지할 레이어 마스크
    [field : SerializeField] public LayerMask _targetLayer { get; private set; }
    // 터렛이 쏠 총알 오브젝트
    [field : SerializeField] public GameObject _bullet { get; private set; }
    // -------------------------------------------------------------------------
    
    // 다른 컴포넌트를 가져올 변수들 --------------------------------------------------
    private Status _turretInfo; // 스탯을 받아올 변수
    private TurretTrigger _turretTrigger; // 자식 컴포넌트를 받아줄 임시 변수
    private SphereCollider _triggerCollider;
    private float _turretCoolTime;
    private bool _canAttack
    {
        get { return _turretCoolTime >= _turretInfo._fireSpeed; }
    }

    public Player _player;
    // -----------------------------------------------------------------------------

    // 인터페이스 필드 구현 -----------------------------------------------------------------
    public GameObject Damageables { get; }
    public GameObject Attackable { get; }
    // -------------------------------------------------------------------------------
    
    private void Awake()
    {
        CacheComponents();
    }
    
    private void Update()
    {
        RotateHead();
        UpdateTurretCoolTime();
        RayToPlayer();
    }

    private void CacheComponents()
    {
        // 자식인 트리거의 컴포넌트를 참조함
        _turretTrigger = transform.GetComponentInChildren<TurretTrigger>();
        _triggerCollider = transform.GetComponentInChildren<SphereCollider>();
        _turretInfo = transform.GetComponent<Status>();
    }
    
    // 대가리 돌아가는 기능
    private void RotateHead()
    {
        // 1. 플레이어 탐지 전까지는 그냥 빙빙 돈다
        if(_turretTrigger._isPlayerInRange == false)
        {
            _turretHead.transform.Rotate(Vector3.up, _headRotateSpeed * Time.deltaTime);
        }
        
        // 2. 플레이어 탐지하면 플레이어를 처다본다
        else if (_turretTrigger._isPlayerInRange == true)
        {
            // 플레이어 자체의 위치는 좌표상 땅바닥에 있어서 어느 정도 띄워주어야한다.
            _turretHead.transform.LookAt(new Vector3(
                _player.transform.position.x,
                _player.transform.position.y + _muzzlePoint.position.y,
                _player.transform.position.z));
        }
    }

    private void RayToPlayer()
    {
        if (_turretTrigger._isPlayerInRange == true && _player.GetComponent<Player>())
        {
            Ray ray = new Ray(_muzzlePoint.position, _muzzlePoint.transform.forward);
            
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _triggerCollider.radius, _targetLayer))
            {
                if (!_canAttack)
                {
                    return;
                }
                AttackTarget(_turretInfo._damage);
                _turretCoolTime = 0f;
            }
        }
    }
    
    public void AttackTarget(int damage)
    {
        _player.TakeDamage(damage);
        Debug.Log($"{_player.name}에게 {damage}만큼 피해");
    }

    private void UpdateTurretCoolTime()
    {
        _turretCoolTime +=  Time.deltaTime;
    }
    
    // 데미지처리 구현
    public void TakeDamage(int damage)
    {
        /*
        turretHp -= damage;
        Debug.Log(turretHp);
        if (turretHp <= 0)
        {
            Destroy(gameObject);
        }
        */
    }

}

