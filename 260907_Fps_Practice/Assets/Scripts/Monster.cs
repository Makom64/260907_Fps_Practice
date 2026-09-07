using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp;
    
    public GameObject GameObject {get => gameObject;}
    
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            DeathMessage();
            Destroy(this.gameObject);
        }
        Debug.Log($"{gameObject.name} : 데미지를 {damage}만큼 입엇당!");
    }

    public void DeathMessage()
    {
        Debug.Log($"{GameObject.name}이 파괴되었습니다");
    }
}
