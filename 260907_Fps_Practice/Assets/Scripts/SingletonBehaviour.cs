using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤 이용할 애들은 얘를 상속받게 할거임
public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // private ????? Instance;
    // 무슨 타입을 넣을 것인지가 중요한데 제네릭으로 해결
    private static T _instance;
    // 인스턴스는 static으로 제공되어야 할 것이다

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>(); 
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = GetComponent<T>(); // 자기 자신을 T로 변환하면서 넣어준다
            DontDestroyOnLoad(gameObject);
        }
    }
    
    // 게임 실행할 때 플레이어 초기화가 먼저 실행될지
    // 아니면 이런 메니저 초기화가 먼저 실행될지는 유니티 마음대로라 
    // 초기화가 완료되었으면 인스턴스를 넘겨주는 방식으로 해결가능
}
