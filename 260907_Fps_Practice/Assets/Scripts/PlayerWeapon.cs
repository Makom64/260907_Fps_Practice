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
    
    // 한 탄알집에 30발, 다 쏘면 발사불가, R키로 30발 반환
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private int _maxMagazine;
    private int magazine;
    private bool _enoughMagazine
    {
        get { return magazine > 0; }
    }
    
    [SerializeField] private FlameEffect _flameEffect;
    [SerializeField] private FlameEffect _bulletEffectPrefab;
    
    private void Update()
    {
        FireCoolTime();
        WeaponReload();
        CacheComponents();
    }

    private void Start()
    {
        magazine = _maxMagazine;
    }

    private void PlayBulletEffect(RaycastHit hit)
    {
            Transform effectTransform = Instantiate(_bulletEffectPrefab).transform;
            effectTransform.position = hit.point;
            effectTransform.forward = hit.normal;
        
    }

    private void PlayFlameEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.Play();
    }

    private void FireCoolTime()
    {
        _fireSpeed += Time.deltaTime;
    }
    
    public void Fire()
    {
        if (_isPressedFire && _isFireCoolDone && _enoughMagazine)
        {
            PlayFlameEffect();
            magazine--;
            _fireSpeed = 0f;
            Debug.Log($"남은 장탄수 : {magazine}");
            
            IDamageable damageable = GetDamageable();
            
            if (damageable == null)
            {
                return;
            }
            
            damageable.TakeDamage(_damage);
            Debug.Log($"Player: {damageable.Damageables.name} 에게 발사");
        }
        else if (_isPressedFire && _isFireCoolDone && !_enoughMagazine)
        {
            Debug.Log("재장전을 하세연!");
            return;
        }
    }

    private void WeaponReload()
    {
        if (magazine < _maxMagazine && Input.GetKeyDown(_reloadKey))
        {
            magazine = _maxMagazine;
            Debug.Log($"재장전 햇시유!");
        }
    }

    private IDamageable GetDamageable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        
        IDamageable damageable = null;

        if (Physics.Raycast(ray, out RaycastHit hit, _weaponRange))
        {
            PlayBulletEffect(hit);
            damageable = hit.collider.GetComponent<IDamageable>();
        }

        return damageable;
    }

    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }
}
