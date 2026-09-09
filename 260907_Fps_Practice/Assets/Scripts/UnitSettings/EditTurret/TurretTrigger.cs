using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    private Turret _turret; // 변수를 선언

    private void Awake()
    {
        CacheTurret();
    }
    
    private void OnTriggerEnter(Collider targetCol)
    { 
        int mask = 1 << targetCol.gameObject.layer;

        if (mask == _turret._targetLayer.value)
        {
            Debug.Log($"근처 터렛이 {targetCol.gameObject.name} 감지함");
        }
    }
    
    private void CacheTurret()
    { 
        _turret = transform.parent.GetComponent<Turret>(); // 부모한테 붙어있는걸 받아옴
    }
}
