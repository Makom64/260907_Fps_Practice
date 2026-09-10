using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 카메라 관련 ----------------------------------------------------
    // 인스펙터에서 메인카메라가 될 오브젝트를 담는다
    [SerializeField] private Transform _playerCam;
    [SerializeField] private float _mouseSensitivity; // 마우스 민감도
    [SerializeField] private float _minPitch; // 카메라 아래로 최대 각도
    [SerializeField] private float _maxPitch; // 카메라 위로 최대 각도

    private Transform _playerCamTransform; // 메인카메라를 담아줄 변수
    private GiveStatus _playerStatus; // 플레이어 스탯을 담아줄 변수
    private float _pitch; // 인스펙터에서 설정한 피치값을 담아줄 변수

    // 공격 관련 ------------------------------------------------------
    [SerializeField] private KeyCode _useWeapon = KeyCode.Mouse0; // 좌클릭으로 공격
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [field : SerializeField] public KeyCode _changeWeapon1 = KeyCode.Alpha1;// 무기 1로 변경
    [field : SerializeField] public KeyCode _changeWeapon2 =  KeyCode.Alpha2; // 무기 2로 변경
    
    private Player _player; // 플레이어를 담을 변수
    private LayerMask _playerEnemy; // 플레이어의 레이어 마스크를 가져올 변수
    private IDamageable _damageables; // 공격대상을 담을 변수

    public PlayerWeapon _playerWeapon;
    
    // 공격키를 눌렀는지를 반환해줄 메서드
    public void ReadWeaponKeyInput()
    {
        if (!Input.GetKeyDown(_useWeapon))
        {
            return;
        }

        Ray ray = new Ray(_playerCam.position, _playerCam.forward);
        RaycastHit hit;
        _playerEnemy = _player._PlayerEnemyMask;
        
        if (Physics.Raycast(ray, out hit, _playerWeapon._range, _playerEnemy))
        {
            // 맞은 애를 임시 변수에 담아주고, IDamageable 컴포넌트도 가져온다
            _damageables = hit.transform.GetComponentInParent<IDamageable>();
            _damageables.TakeDamage(_playerStatus._damage * _playerWeapon._damage);
            Debug.Log(hit.transform.name);
        }
        
    }
    
    private Rigidbody _playerRB; // 여기다가 rigidbody를 달아줄거임

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        MovePlayerCam();
        MovePlayerPosition();
        ReadWeaponKeyInput();
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
    
    // 이동 키 입력으로 Vector3를 반환해줄 메서드
    private Vector3 ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 좌우
        float z = Input.GetAxisRaw("Vertical"); // 앞뒤
        return new Vector3(x, 0, z).normalized; // 단위벡터로 받아야한다
    }

    // 스크립트가 붙어있는 부모랑 자식이 같이 움직여야됨
    private void MovePlayerPosition()
    {
        // _playerRB.MovePosition(transform.position + ReadMoveInput());
        // 이러면 프레임마다 존나 빠르게 절대좌표계로 움직임
        
        // 키입력으로 받은 벡터를 변수에 담고 쓴다
        Vector3 _moveInput = ReadMoveInput();
        // 방향을 담아줄 Vector3
        Vector3 direction = transform.right * _moveInput.x + transform.forward * _moveInput.z;
        // 스크립트가 붙은 애가 단위벡터로 입력값만큼 간다
        
        // 초당 속도로 움직이려면 Velocity가 필요함
        Vector3 playerVelocity = new Vector3(
            direction.x * _playerStatus._moveSpeed,
            _playerRB.velocity.y,
            direction.z * _playerStatus._moveSpeed);
        _playerRB.velocity = playerVelocity;
    }

    
    // 임시 저장 메서드
    private void CacheComponents()
    {
        _playerCamTransform = Camera.main.transform; // 메인 카메라 위치를 갖는다
        // 이 스크립트가 붙은 플레이어의 스탯을 가져온다
        _playerStatus = transform.GetComponent<GiveStatus>();
        // 플레이어의 rigidbody를 가져옴
        _playerRB = transform.GetComponent<Rigidbody>();
        _playerWeapon = transform.GetComponent<PlayerWeapon>();
        _player = transform.GetComponent<Player>();
    }
}
