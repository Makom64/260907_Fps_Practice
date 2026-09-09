using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable
{
    [SerializeField] private LayerMask _playerEnemy;
    
    [SerializeField] private KeyCode _attackKey =  KeyCode.A;
    
    private GiveStatus _playerInfo;
    private IDamageable damageables;
    
    public GameObject Attackable { get; }
    

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        Attack();
    }

    
    
    private void CacheComponents()
    {
        _playerInfo = gameObject.GetComponent<GiveStatus>();
    }


    private void Attack()
    {
        if (!Input.GetKeyDown(_attackKey))
        {
            return;
        }
        AttackTarget(_playerInfo._damage);
    }
    
    // 공격하는 메서드
    public void AttackTarget(int damage)
    {
        
        // 레이를 주인 위치에서 앞으로 쏨
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // 사거리 2칸에서 맞은 애가 선택한 레이어가 맞다면
        if (Physics.Raycast(ray, out hit, 2f,_playerEnemy))
        {
            // 맞은 애의 피해입는 컴포넌트를 가져와서 피해입는 메서드를 실행함
            hit.transform.GetComponent<IDamageable>().TakeDamage(damage);
            Debug.Log("공격성공");
        }
    }
}
