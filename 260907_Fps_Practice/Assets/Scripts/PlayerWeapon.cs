using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Transform _cameraTransform;

    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private float _weaponRange;
    [SerializeField] private int _damage;
    private float _fireSpeed;
    [SerializeField] private float _fireCoolTime;
    private bool _isFireCoolDone
    {
        get { return _fireSpeed >= _fireCoolTime; }
    }

    private bool _isPressedFire => Input.GetKey(_fireKey);

    private void Update()
    {
        FireCoolTime();
        CacheComponents();
    }

    private void FireCoolTime()
    {
        _fireSpeed += Time.deltaTime;
    }
    
    public void Fire()
    {
        if (!_isPressedFire)
        {
            return;
        }

        if (_isPressedFire && _isFireCoolDone)
        {
            IDamageable damageable = GetDamageable();

            if (damageable == null)
            {
                return;
            }
        
            damageable.TakeDamage(_damage);
            Debug.Log($"Player: {damageable.GameObject.name} 에게 발사");
            _fireSpeed = 0f;
        }
        
    }

    private IDamageable GetDamageable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        
        IDamageable damageable = null;

        if (Physics.Raycast(ray, out RaycastHit hit, _weaponRange))
        {
            damageable = hit.collider.GetComponent<IDamageable>();
        }

        return damageable;
    }

    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }
}
