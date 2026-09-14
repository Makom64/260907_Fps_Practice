using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObservableProperty<T>
{
    private Action<T> _onValueChanged;
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Notify();
        }
    }
    
    // 생성 단계
    public ObservableProperty(T initialValue)
    {
        _value = initialValue;
    }

    public void AddListner(Action<T> onValueChanged)
    {
        // 매개변수가 int인 함수같은걸 담을 수 있다
        _onValueChanged += onValueChanged;
    }

    public void RemoveListner(Action<T> onValueChanged)
    {
        _onValueChanged -= onValueChanged;
    }
    
    // 만약에 플레이어가 파괴되거나 하는 경우
    // 델리게이트에 null을 담아준다
    public void RemoveAllListners()
    {
        _onValueChanged = null;
    }

    // 값이 바뀌었을 때 델리게이트를 실행해준다
    public void Notify()
    {
        _onValueChanged?.Invoke(_value);
    }
}
