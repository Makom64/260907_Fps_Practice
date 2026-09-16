using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLight : MonoBehaviour
{
    [SerializeField] private float _time;
    private Renderer _CubeRenderer;
    private readonly WaitForSeconds _waitOneSeconds = new WaitForSeconds(1f);
    private Coroutine _signalRoutine;
    private bool _isCrossRequested;

    private void Awake()
    {
        CacheComponents();
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        ReadKey();
        ReadKeyKey();
    }

    private Coroutine SignalRoutine()
    {
        _signalRoutine = StartCoroutine(RunSignalRoutine());

        return _signalRoutine;
    }
    
    private void ReadKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _signalRoutine == null)
        {
            SignalRoutine();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _signalRoutine != null)
        {
            StopCoroutine(_signalRoutine);
            _signalRoutine = null;
            // 여기서 null로 바꿔주지 않으면 시작하는 조건문을 통과하지 못 하니까!
        }
    }

    private void ReadKeyKey()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _isCrossRequested = true;
        }
    }
    
    private IEnumerator RunSignalRoutine()
    {
        while (true)
        {
            _CubeRenderer.material.color = Color.red;
            yield return _waitOneSeconds;
            _CubeRenderer.material.color = Color.yellow;
            yield return new WaitUntil(() => _isCrossRequested);
            _isCrossRequested = false;
            _CubeRenderer.material.color = Color.green;
            yield return _waitOneSeconds;
        }
    }

    private void CacheComponents()
    {
        _CubeRenderer = GetComponent<Renderer>();
    }
}
