using UnityEngine;



public static class LayerMaskExtensions
{
    public static bool Contains(this LayerMask mask, int layer)
    {
        return (mask.value & (1 << layer)) != 0;
    }
        
    public static bool Contains(this LayerMask mask, GameObject go)
    {
        return (mask.value & (1 << go.layer)) != 0;
    }
        
    public static bool Contains(this LayerMask mask, Component component)
    {
        return (mask.value & (1 << component.gameObject.layer)) != 0;
    }

    public static LayerMask Add(this LayerMask mask, int layer)
    {
        return (mask.value | (1 << layer));
    }

    
    public static LayerMask Remove(this LayerMask mask, int layer)
    {
        return (mask.value & ~(1 << layer));
    }
    
    // 0000 0000 0000 0000 0000 000'1' 1100 0000 에서 9번 레이어를 뺴려면
    // 0000 0000 0000 0000 0000 000'0' 1100 0000 으로 만들려면
    
    // not
    // 0000 0000 0000 0000 0000 000'1' 1100 0000 에서
    // 1111 1111 1111 1111 1111 111'0' 1111 1111  일단 다 뒤집고
    
    // and 연산자
    // 0000 0000 0000 0000 0000 000'0' 1100 0000

    // 1111 1111 1111 1111 1111 1111 1111 1111 찾기
    public static LayerMask Everything(this LayerMask mask)
    {
        return ~0;
    }

    public static LayerMask Nothing(this LayerMask mask)
    {
        return 0;
    }
}
