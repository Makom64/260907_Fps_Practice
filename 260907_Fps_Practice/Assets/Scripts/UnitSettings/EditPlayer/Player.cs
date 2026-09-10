using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable
{
    [field : SerializeField] public LayerMask _PlayerEnemyMask { get; private set; } // Enemy로 설정 7번

    private GiveStatus _playerInfo; // 플레이어의 스탯

    public GameObject Attackable { get; }

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        
    }
    
    private void CacheComponents()
    {
        _playerInfo = gameObject.GetComponent<GiveStatus>();
    }

    public void AttackTarget(int damage)
    {
        
    }
}
