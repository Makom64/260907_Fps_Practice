using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimeHandler : MonoBehaviour
{
    [SerializeField] private string _moveXParam;
    [SerializeField] private string _moveYParam;

    private int _moveX;
    private int _moveY;
    private Animator _animator;
    private BotController _botController;

    private void OnEnable() => BindBotEvents();
    private void OnDisable() => UnBindBotEvents();
    
    private void BindBotEvents()
    {
        _botController.OnMove += SetMoveAnim;
    }

    private void UnBindBotEvents()
    {
        _botController.OnMove -= SetMoveAnim;
    }

    private void SetMoveAnim(Vector2 movement)
    {
        _animator.SetFloat(_moveXParam, movement.x);
        _animator.SetFloat(_moveYParam, movement.y);
    }

    private void Init()
    {
        _moveX = Animator.StringToHash(_moveXParam);
        _moveY = Animator.StringToHash(_moveYParam);
    }
    
    private void CacheComponents()
    {
        _botController = GetComponent<BotController>();
        _animator = GetComponent<Animator>();
    }
}
