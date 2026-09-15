using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour, IPoolable
{
    public ObjectPool Pool { get; set; } // 이 총알이 돌아갈 오브젝트 풀
    public Transform poolableTransform { get; }

    private void Update()
    {
        
    }

    public void ReturnToPool()
    {
        
    }
}
