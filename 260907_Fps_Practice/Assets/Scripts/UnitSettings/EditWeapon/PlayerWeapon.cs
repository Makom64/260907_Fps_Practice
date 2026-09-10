using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [field : SerializeField] public int _damage { get; private set; } // 공격력
    [field : SerializeField] public float _range { get; private set; } // 사거리
    [field : SerializeField] public int _maxMagazine { get; private set; } // 최대 장탄수
    [field : SerializeField] public float _fireCoolTime { get; private set; } // 발사속도
    [field : SerializeField] public bool _canReload { get; private set; } // 일회성인지
    
    /*
    private void Fire()
    {
        
    }

    private void Reload()
    {
        
    }

    private void ChangeWeapon()
    {
        
    }
    */
    
}
