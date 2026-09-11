using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public ObjectPool Pool { get; set; } // 자신이 속한 Pool에 대한 프로퍼티
    
    // 어차피 생성할 위치등을 설정할거라면 인터페이스에서 Transform을 제공해주기
    public Transform tr { get; }
    
    public void ReturnToPool();
}


// 강사님이 터렛에 적용한 방법

/*
 * [serializeField] private ObjectPool _pool; 형태로 담아주고
 *
 * SpawnBullet() 형태의 총알 생성하는 메서드에 포함시키는 형태로 진행
 *
 * IPoolable bullet = _pool.Take(); 형태로 얻어오고
 *
 * bullet.transform.position = muzzlePoint.position;
 * bullet.transform.rotation = muzzlePoint.rotaition; // 머즐 포인트 기준으로 위치,회전 설정
 *
 * 그리고 비활성화 되어있으니 활성화시키기
 * bullet.transform.gameObject.SetActive(true);
 *
 * 총알의 기능부여를 위해 bulletController에서 IPoolable 동일하게 적용
 * 
 * 오브젝트 풀로 돌아가게 되는 경우 일정시간 뒤 Destroy가 필요하지 않다
 * _elapsedTime과 _returnDelay 등의 쿨타임 변수로 조건식 뒤에 오브젝트 풀로 돌아가도록
 *
 * IPoolable을 상속받은 클래스가 오브젝트풀 복귀를 다루는 방식으로
 *
 * 상속받은 ReturnToPool에 들어가는 요소
 * 1. 제한 시간의 경과 (앞에서 언급) / 총알이 맞았을 때 = OnTriggerEnter
 * 2. 자신이 속한 풀에 대한 참조
 * 
 * 3. 풀 내부적으로 다시 오브젝트를 넣기
 * Pool.Return(this;) ....this = 오브젝트 풀에서 대입
 * _elpasedTime 초기화
 *
 * 인터페이스 IPoolable이 생성 당시에 = 프로퍼티에 set 필요
 *
 * 풀에 담겨 있던 오브젝트를 지금 형태는 맨 뒤에서부터 가져왔는데 => 자료구조 맛보기
 * ++ 되돌려줄 때 (비활성화하고 배열에 다시 추가할 때)에도 맨 뒤에서 부터
 * 데이터를 관리하는 방법의 일종

 *
 * 풀을 만들어서 넣어놓고 쓰지 않아도 되는 것을 풀을 쓰기 위해 억지로 만드는 것은 주의할 점
 * 풀을 미리 만들어 놓는 것 또한 메모리에 할당이 되기 때문
 */