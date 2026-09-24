using UnityEngine;

//ハンマーの種類を定義
public enum Hammer_Kind
{ 
    WOOD_HAMMER,
    IRON_HAMMER,
}

//シーン間受け渡しをするためにScriptableObjectに
[CreateAssetMenu]
public class HammerKindData : ScriptableObject
{
    public Hammer_Kind kind = Hammer_Kind.WOOD_HAMMER;
}

