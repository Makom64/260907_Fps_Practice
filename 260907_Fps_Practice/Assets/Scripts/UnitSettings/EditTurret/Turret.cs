using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject turretHead; // 터렛의 대가리 설정
    [SerializeField] private GameObject turretBody; // 터렛의 몸통 설정
    [SerializeField] private float _headRotateSpeed; // 대가리 돌아가는 속도
    [SerializeField] private float _turretRange; // 터렛 범위 설정
    [SerializeField] private SphereCollider _turretTrigger; // 자식의 트리거를 담음
    [field : SerializeField] public LayerMask _targetLayer { get; private set; }
    // 프로퍼티로 열어서 밖에서 읽을 수 있고 값은 인스펙터에서만 넣을 수 있음
    
    private GiveStatus _turretInfo; // 스탯을 받아올 변수
    public GameObject Damageables { get; }
    
    // 씬뷰에서 터렛을 좀 더 편하게 관리하려고 찾은 함수
    private void OnValidate() // 인스펙터를 갱신해준다
    {
        if (_turretTrigger != null) // 인스펙터에서 지정했는지
        {
            _turretTrigger.radius = _turretRange; // 지정했다면 내가 설정한 범위만큼 반경을 갱신해줌
        }
    }

    private void Awake()
    {
        CacheComponents();
    }
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        RotateHead();
    }

    private void CacheComponents()
    {
        _turretInfo = gameObject.GetComponent<GiveStatus>();
        // 자기 자신의 컴포넌트 값을 가져옴
    }
    
    // 대가리 돌아가는 기능
    private void RotateHead()
    {turretHead.transform.Rotate(Vector3.up, _headRotateSpeed * Time.deltaTime);}
    
    // 데미지처리 구현
    public void TakeDamage(int damage)
    {
        int turretHP = _turretInfo._hp; // 값을 변경할 수 없어서 다른 변수에 담아줌
        turretHP -= damage;

        if (turretHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
