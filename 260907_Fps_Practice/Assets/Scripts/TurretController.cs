using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private const string TAG_DETECTED = "Player";
    private Transform _playerTransform;
    private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInSight = false;
    [SerializeField] private Transform _turret;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _muzzlePoint;
    private SphereCollider _sphereCollider;
    [SerializeField] private float _cooldown;
    private float _coolTimer;
    private bool _isReadyToFire
    {
        get { return  _coolTimer >= _cooldown; } // false
    }
    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int  _bulletDamage;
    [SerializeField] private float _bulletExistTime;
    

    private void Awake() => CacheComponents();

    private void CacheComponents()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }
     
    private void Update()
    {
        CountCoolTimer();
        DetectPlayer();
        TurretRotate();
        Fire();
    }

    private void OnTriggerEnter(Collider detected)
    {
        if (!detected.CompareTag(TAG_DETECTED))
        {
            return;
        }
        _playerTransform = detected.transform;
        Debug.Log($"TurretController: {detected.tag} 감지");
    }

    private void OnTriggerExit(Collider detected)
    {
        if (!detected.CompareTag(TAG_DETECTED))
        {
            return;
        }
        _playerTransform = null;
        Debug.Log($"TurretController: {detected.tag} 감지 해제");
    }

    private void CountCoolTimer()
    {
        if (_isReadyToFire)
        {
            return;
        }
        _coolTimer += Time.deltaTime;
    }

    private void SpawnBullet()
    {
        BulletController bullet = Instantiate(_bulletPrefab, _muzzlePoint.position, _muzzlePoint.rotation);
        
        bullet.SetData(_bulletDamage, _bulletSpeed, _bulletExistTime);
    }
    
    private void TurretRotate()
    {
        if (_isPlayerInSight)
        {
            return;
        }
        
        _turret.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void Fire()
    {
        if (!_isPlayerInSight || !_isPlayerInTrigger)
        {
            return;
        }
        
        Vector3 look = new Vector3(
            _playerTransform.position.x,
            _turret.position.y,
            _playerTransform.position.z);
        
        _turret.LookAt(look);

        if (!_isReadyToFire)
        {
            return;
        }
        Debug.Log("발사 발사");
        SpawnBullet();
        _coolTimer = 0f;
    }
    
    private void DetectPlayer()
    {
        _isPlayerInSight = false;
        if (!_isPlayerInTrigger)
        {
            return;
        }

        Vector3 from = new Vector3(
            transform.position.x,
            transform.position.y +  _muzzlePoint.position.y / 2,
            transform.position.z);
        Vector3 to = new Vector3(
            _playerTransform.position.x,
            _playerTransform.position.y +  _muzzlePoint.position.y / 2,
            _playerTransform.position.z);
        
        Ray ray = new Ray(from, (to - from).normalized);

        if (Physics.Raycast(ray, out RaycastHit hit,  _sphereCollider.radius))
        {
            if (hit.transform.CompareTag(TAG_DETECTED))
            {
                _isPlayerInSight = true;
                Debug.Log("DetectPlayer: 플레이어 발견!!!! ");
            }
        }
    }
}
