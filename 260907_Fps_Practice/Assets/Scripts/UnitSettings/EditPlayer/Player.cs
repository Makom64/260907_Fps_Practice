using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable, IDamageable
{
    [field : SerializeField] public LayerMask _PlayerEnemyMask { get; private set; } // Enemy로 설정 7번

    private Status _playerInfo; // 플레이어의 스탯

    public GameObject Attackable { get; }
    public GameObject Damageables { get; }

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        
    }
    
    private void CacheComponents()
    {
        _playerInfo = gameObject.GetComponent<Status>();
    }

    public void AttackTarget(int damage)
    {
        
    }

    public void TakeDamage(int damage)
    {
        int playerHp = _playerInfo._hp;
        playerHp -= damage;
        Debug.Log(playerHp);

        if (playerHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
