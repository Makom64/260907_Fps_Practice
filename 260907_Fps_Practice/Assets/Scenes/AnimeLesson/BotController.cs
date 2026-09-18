using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _playerCam;
    [SerializeField] private float _mouseSensivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private Transform _mainCam;
    private float _pitch;
    
    private Vector2 _prevMovement;
    private Rigidbody _rb;

    public event Action<Vector2> OnMove;

    private void Awake()
    {
        CacheComponents();
    }
    
    private void Update()
    {
        SetMove();
        MovePlayer();
    }

    private void LateUpdate()
    {
        PlayerCamMove();
    }

    private void SetMove()
    {
        // 이전 프레임의 Movement와 같으면 return;
        // 다르다면 OnMove 실행 + _prevMovemnt 갱신

        Vector2 movement = GetMovement();
        if (_prevMovement == movement) return;
        
        OnMove?.Invoke(movement);
        _prevMovement = movement;

    }

    private Vector2 GetMovement()
    {
        // 입력 받아서 Vector2 반환 GetAxisRaw
        // 단위벡터로 만들지 마세요

        return new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
    }
    
    private Vector3 ReadMouseInput()
    {
        float LR = Input.GetAxis("Mouse X");
        float UD = Input.GetAxis("Mouse Y");

        return new Vector3(LR, -UD, 0);
    }

    private void PlayerCamMove()
    {
        Vector3 _mouseMove =  ReadMouseInput() *  _mouseSensivity;
        
        transform.Rotate(0, _mouseMove.x, 0, Space.Self);
        _pitch = Mathf.Clamp(_pitch +  _mouseMove.y, -_minPitch, _maxPitch);
        _playerCam.localRotation = Quaternion.Euler(_pitch, 0, 0);
        _mainCam.LookAt(new Vector3(_playerCam.position.x,
            _playerCam.position.y,
            _playerCam.position.z));
    }
    
    
    private Vector3 ReadMoveInput()
    {
        float z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        
        return new Vector3(z, 0, z).normalized;
    }

    private void MovePlayer()
    {
        Vector3 _move = ReadMoveInput();

        Vector3 direction = transform.right * _move.x + transform.forward * _move.z;

        Vector3 playerVelo = new Vector3(
            direction.x * _moveSpeed,
            _rb.velocity.y,
            direction.z * _moveSpeed);
        
        _rb.velocity = playerVelo;
    }

    private void CacheComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCam = Camera.main.transform;
    }
}
