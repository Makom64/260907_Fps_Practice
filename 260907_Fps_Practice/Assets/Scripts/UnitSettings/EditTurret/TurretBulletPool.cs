using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretBulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _turretBulletPrefabs; // 터렛총알 프리팹을 담을 변수
    [field: SerializeField] public int TRBulletSize { get; private set; } // 오브젝트 풀 크기

    private PoolableTurretBullet[] _turretBullet; // 터렛총알 배열

    public int TRBulletCount { get; private set; } // 밖에서 갯수를 조건으로 자동하는게 있어서 프로퍼티 열어둠
    public bool IsEmpty => TRBulletCount == 0; // 갯수가 0이면 배열이 비어있는 것

    private void _turretBulletInit()
    {
        // 배열의 크기는 생성할 크기만큼
        _turretBullet = new PoolableTurretBullet[TRBulletSize];

        for (int i = 0; i < TRBulletSize; i++)
        {
            // newBullet은 생성한 프리팹의 값을 가진다
            GameObject newBullet = Instantiate(_turretBulletPrefabs);
            _turretBullet[i] = newBullet.GetComponent<PoolableTurretBullet>();
        }
    }
}
