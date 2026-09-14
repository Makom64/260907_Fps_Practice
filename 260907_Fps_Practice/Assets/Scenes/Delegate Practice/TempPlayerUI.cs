using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempPlayerUI : MonoBehaviour
{
    
    
    public TempPlayer Player;
    [SerializeField] private TextMeshProUGUI _playerHealthText;

    /*private void OnEnable()
    {
        Player.OnHealthChange += RefreshHealthUI;
    }

    private void OnDisable()
    {
        Player.OnHealthChange -= RefreshHealthUI;
    }*/
    // 만일 위 OnEnable에서 식이 델리게이트 체인이였으면
    // 저 메서드 대입이 오브젝트가 켜졌다 꺼졌다 할 때마다 배로 늘어난다
    
    // 근데 그런 문제는 반대로 OnDisable에서 뺴주는 방식으로 방지 가능함!
    
    public void RefreshHealthUI(int health)
    {
        _playerHealthText.text = health.ToString();
    }
}
