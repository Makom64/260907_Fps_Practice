using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    
    private Rigidbody _rigidbody;
    private float _pitch;

    private void Awake() => CacheComponents();
    
    public void Move()
    {
        // 입력받아서 방향구하기
        Vector3 movement = ReadMoveInput();
        
        // 새로운 벨로시티 값 설정
        Vector3 direction = transform.right * movement.x + transform.forward * movement.z;
            
        Vector3 newVelocity = new Vector3(
            direction.x * _moveSpeed,
            _rigidbody.velocity.y,
            direction.z * _moveSpeed);
        
        // _rigidbody.velocity에 적용
        _rigidbody.velocity = newVelocity;

    }

    public void CameraRotate()
    {
        Vector3 input = ReadRotateInput() * _mouseSensitivity;
        // 좌우 -> 360도 회전
        transform.Rotate(0, input.y, 0, Space.Self);
        // 상하 -> 고정값 안에서
        _pitch = Mathf.Clamp(_pitch + input.x, -_maxPitch, _maxPitch);
        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    private Vector3 ReadRotateInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y =  Input.GetAxis("Mouse Y");
        
        return new Vector3(-y,x,0);
    }

    private Vector3 ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        return new Vector3(x,0,z).normalized;
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
