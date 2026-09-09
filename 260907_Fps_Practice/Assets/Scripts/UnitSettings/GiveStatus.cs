using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveStatus : MonoBehaviour
{
    [field : SerializeField] public string _name { get; private set; }
    [field : SerializeField] public int _hp { get; private set; }
    [field : SerializeField] public int _damage { get; private set; }
    [field : SerializeField] public float _moveSpeed { get; private set; }
    [field : SerializeField] public float _fireSpeed { get; private set; }
}
