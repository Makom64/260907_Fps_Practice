using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    private PlayerMovement _playerMovement;
    private Transform _cameraTransform;
    [SerializeField] private PlayerWeapon _weapon;
    
    private void Awake() => CacheComponents();
    
    private void Start() => LockCursor();

    private void FixedUpdate() => _playerMovement.Move();

    private void Update()
    {
        _playerMovement.CameraRotate();
        _weapon.Fire();
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
}
