using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> // 싱글톤 매니저에서 자기자신을 넣고 상속받음
// 왜냐면 상속받은 싱글톤이 monobehaviour를 상속받고 있으니까 같이 딸려와서 그래
{
    public bool IsGameRunning { get; private set; }
    public static GameManager Instance;
    // 자기 자신의 인스턴스
    // 다른 클래스 안에서 GameManager.Instance.~ 으로 불러와서 쓸 수 있다

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        
        
        // 2. 게임 내에 '단 하나'만 존재해야 함
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        // = 인스턴스에 무엇인가 들어있고, 그게 자신이 아니라면 파괴
        else
        {
            // 1. 전역적인 접근 지원
            Instance = this;
            
            // 3. Scene 전환 시에도 유지
            DontDestroyOnLoad(gameObject);
        }
        
        
    }
    
    private void Start() => Run();

    public void Run()
    {
        LockCursor();
        Time.timeScale = 1;
        IsGameRunning = true;
    }

    public void Pause()
    {
        UnlockCursor();
        Time.timeScale = 0;
        IsGameRunning = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
