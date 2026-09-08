using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ItemBox : MonoBehaviour, IInteractable
{
    
    public GameObject GameObject
    {
        get => gameObject;
    }
    
    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        Init();
    }

    private Outline _outline;
    
    public void Interact(IInteractor interactor)
    {
        if (!(interactor is PlayerController))
        {
            return;
        }
        PlayerController player =  (PlayerController)interactor;
        Destroy(gameObject);
    }

    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void Untargeting()
    {
        _outline.enabled = false;
    }
    
    private void Init()
    {
        _outline.enabled = false;
    }

    public void CacheComponents()
    {
        _outline = gameObject.GetComponent<Outline>();
    }

}
