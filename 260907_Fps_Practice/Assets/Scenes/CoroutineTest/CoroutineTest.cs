using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    // 지금까지 시간 쟀던 방식
    /*private float _elapsedTime;
    private float _time = 2f;
    
    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _time)
        {
            _elapsedTime = 0;
            Debug.Log("지정 시간 경과");
        }
    }*/

    [SerializeField] private float _time;
    private WaitForSeconds _wait;
    private bool _isBool;
    private Coroutine _coroutine;

    private void Awake()
    {
        // 반복적으로 yield return의 조건으로 쓸거기 때문에 미리 캐싱해두기
        _wait = new WaitForSeconds(_time);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Run();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Stop();
        
    }

    private void Run()
    {
        if (_coroutine != null) return;
        _coroutine = StartCoroutine(MyRoutine());
    }

    private void Stop()
    {
        if (_coroutine == null) return;
        
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
    
    // 함수의 반환형은 IEnumerator, 반환식은 yield return
    private IEnumerator MyRoutine()
    {
        while (true)
        {
            // new WaitUntil() = 괄호안의 조건이 참이 될 때까지 기다린다, 괄호 안에는 delegate가 들어간다
            // new WaitWhile() = 괄호안의 조건이 false일때까지
            yield return _wait;
            Debug.Log("Coroutine");
        }
        /*Debug.Log("Coroutine 1");
        // yield return은 yield return 000 : 000 이 충족되는 상황까지 함수를 일시정지하고 대기함 
        yield return _wait;
        
        Debug.Log("Coroutine 2");

        // stopcoroutine 말고 함수 안에서 자체적으로 종료시킬 필요가 있을때
        yield break;*/
    }
}
