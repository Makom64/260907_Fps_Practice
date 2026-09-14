using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Action과 Func을 쓸 때는 얘를 항상 불러와야한다는 걸 잊지말거라
using UnityEngine.Events;

public class TempPlayer : MonoBehaviour
{
    public UnityEvent TempEvent;
    public delegate void IntChange(int value);
    // 이 선언의 의미
    // 반환형 : 없음, 매개 변수 : int 1개 의 함수를 담는 타입을 선언한 것이다

    // public event IntChange OnHealthChange;
    // 델리게이트 자체를 밖에서 실행시켜버리면 문제점으로 다시 복귀하기 때문에
    // event라는 키워드를 추가하여 사건이 발생했을 때만 작동하도록 한다
    // 근데 반환형마다 새로 작성해야되는 문제점을 다시한번 고친게 아래 형태
    public event Action<int> OnHealthChange;
    // public event Action<int, float, string> 저걸 반환하는 델리게이트들

    public ObservableProperty<float> Exp = new(0);
    
    private int _health;
    
    public int Health 
    { 
        get => _health;
        private set { _health = value; OnHealthChange?.Invoke(_health); }
        // 델리게이트가 참조를 안 하고 있어서 null일수도 있기 때문에 ?로 간단하게 검사식을 추가한다
    }
    
    public TempPlayerUI UI;


    // 데이터를 불러오는 기능이 성공도 있고 실패도 있는데
    // 성공했을 때와 실패했을 때를 함수로 호출해버릴수도있다
    private void LoadData(Action s, Action f)
    {
        
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) TakeDamage(5);
        if(Input.GetKeyDown(KeyCode.Alpha2)) Heal(10);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Exp.Value += 20.5f;
        if (Input.GetKeyDown(KeyCode.Alpha4)) TempEvent?.Invoke();
    }

    private void OnDestroy()
    {
        Exp.RemoveAllListners();
    }
    
    public void TakeDamage(int damage)
    {
        Debug.Log("아파요");
        Health -= damage;
        UI.RefreshHealthUI(Health);
    }

    public void Heal(int heal)
    {
        Debug.Log("조아요");
        Health += heal;
        UI.RefreshHealthUI(Health);
    }
}
