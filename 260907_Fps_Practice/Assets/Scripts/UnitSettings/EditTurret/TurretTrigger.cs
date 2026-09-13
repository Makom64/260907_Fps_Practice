using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 터렛의 트리거에게 줄 역할
// 1. 트리거는 감지하고 대상을 구별한다.
// 2. 감지하고 구별해낸 대상의 값을 터렛에게 넘겨준다.

public class TurretTrigger : MonoBehaviour
{
    // ----------------------------------------------------------
    private Turret _turret; // 부모 컴포넌트를 참조
    
    public bool _isPlayerInRange; // 플레이어 감지했는지를 조건으로, 기본 false
    public float _range;

    private void Awake()
    {
        CacheTurret();
    }
    
    private void CacheTurret()
    {
        _turret = transform.parent.GetComponent<Turret>();
    }
    
    private void OnTriggerEnter(Collider targetCol)
    { 
        // 충돌체의 레이어 마스크값으로 임시 마스크 변수를 선언
        int mask = 1 << targetCol.gameObject.layer;
        // 비트값을 감지 된 레이어마스크 번호만큼 민다

        // 감지된 놈과 임시 변수의 레이어 마스크를 정수로 변환한 값이 같으면 플레이어다
        if (mask != _turret._targetLayer.value)
        {
            // 아닌 경우 반환하도록
            return;
        }
        // 일치하는 경우 (플레이어 이름)을 감지했다는 내용을 출력
        Status _detected;
        _detected = targetCol.transform.GetComponent<Status>(); // _detected는 플레이어의 Status로 캐스팅
        _isPlayerInRange = true;
        _turret._player = targetCol.transform.GetComponent<Player>();
        Debug.Log($"근처 터렛이 {_detected._name} 감지함");
    }

    private void OnTriggerExit(Collider targetCol) // 플레이어가 탐지 범위 밖으로 나갔을 때의 행동
    {
        int mask = 1 << targetCol.gameObject.layer;
        
        if (mask != _turret._targetLayer.value)
        {
            return;
        }
        Status _detected;
        _isPlayerInRange = false;
        _detected = targetCol.transform.GetComponent<Status>();
        Debug.Log($"근처 터렛이 {_detected._name} 를 놓침");
    }
}
