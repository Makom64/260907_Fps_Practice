using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform _cameraPivot; // 시점 카메라를 담는다
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private float _interactRange;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private KeyCode _useGrenade = KeyCode.Alpha3;
    
    private PlayerMovement _playerMovement; // 플레이어 움직임을 참조
    private Transform _cameraTransform; // 카메라 위치
    private IInteractable _interactable; // 상호가능체을 참조
    private bool _hasDetectInteractable => _interactable != null; // 상호가능체 인식 여부
    public GameObject GameObject // 플레이어 프로퍼티
    {
        get => gameObject;
    }

    
    private bool _isPressedInteractKey => Input.GetKeyDown(_interactKey);
    private bool _canInteract => _hasDetectInteractable && Input.GetKeyDown(_interactKey);
    
    private void Awake() => CacheComponents();
    
    private void Start() => LockCursor();

    private void FixedUpdate() => _playerMovement.Move();

    private void Update()
    {
        _playerMovement.CameraRotate();
        _weapon.Fire();
        UseGrenade();
        DetectInteractable();
        TryInteract();
    }

    private void UseGrenade()
    {
        if (!Input.GetKeyDown(_useGrenade))
        {
            return;
        }

        GameObject _grenade = Instantiate(_grenadePrefab, _cameraPivot.transform.position, Quaternion.identity);
        _grenade.transform.Translate(Vector3.forward * 1f);
    }
    
    private void LateUpdate()
    {
        SetCameraTransform();
        SetWeaponTransform();
    }

    private void CacheComponents()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _cameraTransform = Camera.main.transform;
        _weapon = GetComponentInChildren<PlayerWeapon>();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetCameraTransform()
    {
        _cameraTransform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation);
    }
    
    private void SetWeaponTransform()
    { 
        _weapon.transform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation);
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, _interactRange))
        {
            if (_hasDetectInteractable)
            {
                _interactable.Untargeting();
                _interactable = null;
            }
            return;
        }

        if (_hasDetectInteractable)
        {
            if (hit.collider.gameObject == _interactable.GameObject)
            {
                return;
            }
        }
        _interactable?.Untargeting();
        _interactable = hit.collider.GetComponent<IInteractable>();

        
        _interactable?.Targeting();
    }
    
    public void TryInteract()
    {
        if (!_canInteract)
        {
            return;
        }
        _interactable.Interact(this);
        _interactable = null;
    }
    
    
}
