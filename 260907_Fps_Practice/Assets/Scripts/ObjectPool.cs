using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [field: SerializeField] public int Size { get; private set; } // 크기

    private IPoolable[] _pool;
    public int Count { get; private set; }  // 실제로 들어가있는 거
    public bool IsEmpty => Count == 0; // 생성된게 0인지를 읽기 쉽게 할 수 도 있다
    
    // 쓸만큼만 생성
    public void Awake()
    {
        Init();
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
    
    // 생성하는거 함수로 뺴기
    private void Init()
    {
        _pool = new IPoolable[Size]; // 입력받은 사이즈로 배열 생성

        for (int i = 0; i < _pool.Length; i++)
        {
            GameObject go = Instantiate(_prefab);
            _pool[i] = go.GetComponent<IPoolable>();
            _pool[i] = this;
            go.SetActive(false);
            
        }
        
        Count = Size; // 마지막에 갯수 새주기
    }
}
