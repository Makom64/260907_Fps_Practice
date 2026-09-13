using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 게임 내 플레이어나 적들이 가지는 능력치는 여기서 통일 시킨다
public class Status : MonoBehaviour
{
    // 1인칭 Fps에서 보통 쓰이는 능력치들
    
    // 이름
    [field : SerializeField] public string _name { get; private set; }
    // 체력
    [field : SerializeField] public int _hp { get; private set; }
    // 공격력
    [field : SerializeField] public int _damage { get; private set; }
    // 이동 속도 (움직이지 않는 터렛은 이동속도가 0인 적이다)
    [field : SerializeField] public float _moveSpeed { get; private set; }
    // 연사력
    [field : SerializeField] public float _fireSpeed { get; private set; }
}