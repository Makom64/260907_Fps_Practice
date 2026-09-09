using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTest : MonoBehaviour
{
    // 인스펙터에서 레이어마스크 고르기
    public LayerMask TargetLayer;
    // Raycast 거리는 인스펙터에서 조절 가능
    [SerializeField] private float _castRange;

    private void Start()
    {
        TargetLayer = TargetLayer.Remove(9);
    }
    
    private void Update()
    {
        NameDebugger();
    }

    private void NameDebugger()
    {
        // 이 오브젝트의 위치에서 정면으로 발사되는 Ray 생성
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Raycast 사용해서 감지된 게임 오브젝트의 이름 출력
        // Raycast의 4번째 매개변수로 찾을 레이어를 담을 수 있다
        if (Physics.Raycast(ray, out hit, _castRange, TargetLayer))
        {
            Debug.Log(hit.transform.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = 1 << other.gameObject.layer;

        // 찾은 값이 니가 찾으려던 레이어 비트값이랑 같냐
        if (TargetLayer.Contains(other))
        {
            // 찾을 레이어를 여러 개 선택하면
            // 0000 0000 0000 0000 0000 0000 0000 1000 이랑
            // 0000 0000 0000 0000 0000 0000 0000 0100 이랑 합쳐져서
            // 0000 0000 0000 0000 0000 0000 0000 1100 이런식이 됨
            // 여기서
            // 0000 0000 0000 0000 0000 0000 0000 0100 만 찾고 싶으면
            // &&를 씀
            Debug.Log("찾음");
        }
    }

    
    private bool FindPlayer(LayerMask mask, Collider layer)
    {
        return 0 != (mask.value & (1 << layer.gameObject.layer));
    }
}
