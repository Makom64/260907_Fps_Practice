using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀의 기능
// 1. 총알 같이 쓸 애들 미리 만들어놓고 필요할 때 마다 꺼내서 쓰고 돌려놓는 공간
// 2. 터렛 총알, 플레이어 총알, 수류탄 등 조금씩 다르기 떄문에 오브젝트 풀 안에 종류별로 배열 만들기

public class ObjectPool : MonoBehaviour
{
    // 터렛총알 프리팹 참조
    [SerializeField] private TurretBullet _turretBulletPrefabs;
    
    [field: SerializeField] public int Size { get; private set; } // 오브젝트 풀 배열 크기
    
    private IPoolable[] _turretBullets; // 프리팹들로 채워질 배열
    
    public int Count { get; private set; }  // 실제로 들어가있는 거
    public bool IsEmpty => Count == 0; // 생성된게 0인지를 읽기 쉽게 할 수 도 있다
    
    
    private void Awake()
    {
       TurretBulletInit();
    }
    
    // 터렛 총알 배열 채우기
    private void TurretBulletInit()
    {
        // 인스펙터에서 설정한 값만큼 배열의 크기가 정해진다
        for (int i = 0; i < Size; i++)
        {
            // 터렛 총알 프리팹을 생성하고 변수에 담아서 다룬다
            TurretBullet turretbullet = Instantiate(_turretBulletPrefabs);
            turretbullet.gameObject.SetActive(false); // 비활성화 해주어야 함

            // TurretBullet 컴포넌트는 IPoolable을 상속받고 있기 떄문에 캐스팅 가능
            _turretBullets[i] = turretbullet;
        }
        
        // 현재 들어가있는 갯수를 새어줄 Count에도 Size를 대입해준다
        Count = Size;
        Debug.Log($"현재 터렛총알이 {Count}개 생성되어있는 상태입니다.");
    }
    
    public IPoolable TakeBullet() // 배열에서 가져가는 함수이기에 반환형은 인터페이스명으로
    {
        if (IsEmpty) // 풀에 아무것도 없으면 못 받으니까 조건을 넣어야됨
        {
            return null;
        }

        // 배열 사이즈 - 1을 가져오고 Count도 하나 뺴주어야한다
        IPoolable bullet = _turretBullets[Count - 1];
        Count--;

        return bullet;
    }

    public void Return(IPoolable poolable)
    {
        if (Size <= Count) // 꽉찼을때에는 아무것도 하지 않도록
        {
            return;
        }
        
       // _pool[Count] = poolable; // 
       // poolable.tr.gameObject.SetActive(false); // 생성할 때 비활성화
        Count++;
    }
    
    
}
