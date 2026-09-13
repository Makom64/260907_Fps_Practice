using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀의 기능
// 1. 총알 같이 쓸 애들 미리 만들어놓고 필요할 때 마다 꺼내서 쓰고 돌려놓는 공간
// 2. 터렛 총알, 플레이어 총알, 수류탄 등 조금씩 다르기 떄문에 오브젝트 풀 안에 종류별로 배열 만들기

public class ObjectPool : MonoBehaviour
{
    
    [field: SerializeField] public int Size { get; private set; } // 오브젝트 풀 배열 크기
    
    private IPoolable[] _turretBulletPool; // 프리팹들로 채워질 배열
    
    
    public int Count { get; private set; }  // 실제로 들어가있는 거
    public bool IsEmpty => Count == 0; // 생성된게 0인지를 읽기 쉽게 할 수 도 있다
    
    
    private void Awake()
    {
        Init();
    }
    
    // 
    private void _turretBulletInit()
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            GameObject go = Instantiate(_prefab);
            _pool[i] = go.GetComponent<IPoolable>();
            // _pool[i] = this;
            go.SetActive(false);
            
        }
        
        Count = Size; // 마지막에 갯수 새주기
    }

    
    public IPoolable Take() // 생성해둔걸 받아가는 함수
    {
        // 그래서 반환형은 IPoolable

        if (IsEmpty) // 풀에 아무것도 없으면 못 받으니까 조건을 넣어야됨
        {
            return null;
        }
        
        IPoolable poolable = _pool[Count - 1]; // 하나 꺼내면 갯수가 줄어드니까
        
        /* 이거랑 동일함
        Count--;
        IPoolable poolable = _pool[Count];
        */

        return poolable; // 받은걸 반환
    }

    public void Return(IPoolable poolable)
    {
        if (Size <= Count) // 꽉찼을때에는 아무것도 하지 않도록
        {
            return;
        }
        
        _pool[Count] = poolable; // 
        poolable.tr.gameObject.SetActive(false); // 생성할 때 비활성화
        Count++;
    }
    
    
}
