using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform _cameraPivot;
    private PlayerMovement _playerMovement;
    private Transform _cameraTransform;
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private float _interactRange;
    private IInteractable _interactable;
    private bool _hasDetectInteractable => _interactable != null;
    public GameObject GameObject
    {
        get => gameObject;
    }

    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    private bool _isPressedInteractKey => Input.GetKeyDown(_interactKey);
    private bool _canInteract => _hasDetectInteractable && Input.GetKeyDown(_interactKey);
    
    private void Awake() => CacheComponents();
    
    private void Start() => LockCursor();

    private void FixedUpdate() => _playerMovement.Move();

    private void Update()
    {
        _playerMovement.CameraRotate();
        _weapon.Fire();
        DetectInteractable();
        TryInteract();
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
