using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    [SerializeField] private TempPlayer _player;
    [SerializeField] private TempPlayerUI _playerUI;
    [SerializeField] private ExpGauge1 _expGauge1;

    private void Awake()
    {
        CacheComponents();
    }

    private void OnEnable()
    {
        BindPlayerStatChangeEvents();
    }

    private void OnDisable()
    {
        UnBindPlayerStatChangeEvents();
    }

    private void CacheComponents()
    {
        _player = GetComponent<TempPlayer>();
    }

    private void BindPlayerStatChangeEvents()
    {
        _player.OnHealthChange += _playerUI.RefreshHealthUI;
        _player.Exp.AddListner(_expGauge1.RefreshGauge);
    }

    private void UnBindPlayerStatChangeEvents()
    {
        _player.Exp.RemoveListner(_expGauge1.RefreshGauge);
    }
}
