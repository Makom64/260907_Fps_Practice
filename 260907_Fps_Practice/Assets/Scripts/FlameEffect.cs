using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    // 발사버튼을 누르면 = 정해진 시간 만큼 애니메이션 재생됨
    // 발사속도가 0.2초면 딜레이는 0.3초가 적당함
    // 0.3가 되면 애니메이션을 멈춤
    // 발사시간이 되면 딜레이를 0으로 초기화 시킴
    
    [SerializeField] private float _delayTime;
    private float _elapsedTime;
    [SerializeField] private bool _isDestroy;
    [SerializeField] private bool _playInStart;

    private void OnEnable()
    {
        ResetElapsedTime();
    }

    private void Start()
    {
        gameObject.SetActive(_playInStart);
    }

    private void Update()
    {
        UpdateElapsedTime();
        Deactivate();
    }

    public void Play()
    {
        ResetElapsedTime();
    }

    private void ResetElapsedTime()
    {
        _elapsedTime = 0;
    }

    private void UpdateElapsedTime()
    {
        _elapsedTime += Time.deltaTime;
    }

    private void Deactivate()
    {
        if (_elapsedTime < _delayTime)
        {
            return;
        }
        
        gameObject.SetActive(false);
        if(_isDestroy) Destroy(gameObject);
    }
}
