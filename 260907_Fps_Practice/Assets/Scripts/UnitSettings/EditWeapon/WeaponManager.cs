using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [field : SerializeField] public PlayerWeapon[] _playerWeapons { get; private set; }
    
    private PlayerController _playerController; // 컨트롤러 컴포넌트 담을 변수
    private ThrowableMovement _throwable; // 던져서 쓰는 무기들
    
    // 1번 무기를 가지고 있을 땐 1번 작동 안 함
    // 2번 무기를 가지고 있을 땐 2번 작동 안 함

    private bool _hasWeapon1;
    private bool _hasWeapon2;

    

    private void Awake()
    {
        CacheComponents();
        _hasWeapon1 = true;
        _playerWeapons[0].gameObject.SetActive(true);
        _playerWeapons[1].gameObject.SetActive(false);
        _playerWeapons[2].gameObject.SetActive(false);
    }

    private void Update()
    {
        ReadWeaponChangeKey();
    }
    
    private void ReadWeaponChangeKey()
    {
        // 무기 1번 변경 버튼을 눌렀을 때
        if (Input.GetKeyDown(_playerController._changeWeapon1))
        {
            // 무기 1번 가지고 있으면 작동 시키면 안 됨
            if (_hasWeapon1)
            {
                return;
            }
            else
            {
                _hasWeapon1 = true;
                _hasWeapon2 = false;
                _playerWeapons[1].gameObject.SetActive(false);
                _playerWeapons[0].gameObject.SetActive(true);
                _playerController._playerWeapon = _playerWeapons[0].gameObject.GetComponent<PlayerWeapon>();
            }
        }
        // 무기 2번으로 바꾸려고 했을 때
        else if (Input.GetKeyDown(_playerController._changeWeapon2))
        {
            // 무기 2번 가지고 있으면
            if (_hasWeapon2)
            {
                return;
            }
            else
            {
                _hasWeapon2 = true;
                _hasWeapon1 = false;
                _playerWeapons[0].gameObject.SetActive(false);
                _playerWeapons[1].gameObject.SetActive(true);
                _playerController._playerWeapon = _playerWeapons[1].gameObject.GetComponent<PlayerWeapon>();
            }
        }
        else
        {
            return;
        }
    }
    
    private void CacheComponents()
    {
        _playerController = transform.parent.parent.GetComponent<PlayerController>();
        _throwable = GetComponent<ThrowableMovement>();
    }
}
