using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 인스펙터에서 메인카메라가 될 오브젝트를 담는다
    [SerializeField] private Transform _playerCam;
    [SerializeField] private float _mouseSensitivity; // 마우스 민감도
    [SerializeField] private float _minPitch; // 카메라 아래로 최대 각도
    [SerializeField] private float _maxPitch; // 카메라 위로 최대 각도
    
    private Transform _playerCamTransform; // 메인카메라를 담아줄 변수
    private float _pitch; // 인스펙터에서 설정한 피치값을 담아줄 변수

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        MovePlayerCam();
    }
    
    private void LateUpdate()
    {
        SetPlayerCamera();
    }
    
    // 메인카메라의 위치를 바꿔주는 메서드
    private void SetPlayerCamera()
    {
        _playerCamTransform.SetPositionAndRotation(
            _playerCam.position,
            _playerCam.rotation);
    }
    
    // 마우스 움직임을 Vector3로 반환해줄 메서드
    private Vector3 ReadMouseInput()
    {
        float leftRight = Input.GetAxis("Mouse X"); // 마우스 좌우 이동량
        float upDown = Input.GetAxis("Mouse Y"); // 마우스 상하 이동량
        
        // Y축 기준으로 좌우, X축 기준으로 상하만큼 움직여야한다
        // 그리고 X축으로 양수만큼 앞으로 기울어지니까 음수로 바꿔서 상하반전
        return new Vector3(leftRight, -upDown, 0);
    }

    // 이동 키 입력으로 Vector3를 반환해줄 메서드
    private Vector3 ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 좌우
        float z = Input.GetAxisRaw("Vertical"); // 앞뒤
        return new Vector3(x, 0, z);
    }
    
    // 마우스 움직임 Vector3로 카메라를 회전시킨다
    private void MovePlayerCam()
    {
        // GetAxis에 마우스 민감도를 곱한다
        Vector3 _mouseInput = ReadMouseInput() * _mouseSensitivity;
        // 스크립트가 붙은 Player가 Y축을 기준으로 제자리 회전한다
        transform.Rotate(0,_mouseInput.x,0,Space.Self);
        _pitch = Mathf.Clamp(_pitch + _mouseInput.y, -_minPitch, _maxPitch);
        _playerCam.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    // 임시 저장 메서드
    private void CacheComponents()
    {
        _playerCamTransform = Camera.main.transform; // 메인 카메라 위치를 갖는다
    }
}
