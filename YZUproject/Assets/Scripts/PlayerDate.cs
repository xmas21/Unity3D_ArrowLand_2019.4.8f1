using UnityEngine;

[CreateAssetMenu(fileName ="玩家血量",menuName ="Lobo/玩家資料")]
public class PlayerDate : ScriptableObject
{
    [Header("玩家名稱")]
    public string name;

    [Header("生命值"), Range(100, 30000)]
    public float hp = 300;
    [Header("最大血量"), Range(100, 30000)]
    public float hpMax;

    [Header("冷卻時間"), Range(0.01f, 2)]
    public float cd = 1f;
    [Header("武器速度"), Range(1000, 5000)]
    public float power = 1000;

    [Header("攻擊傷害"), Range(1, 5000)]
    public float attack = 50;
    [Header("爆擊傷害"), Range(0, 100)]
    public float CriticalAttack = 0;
    [Header("武器傷害"), Range(0, 2000)]
    public float WeaponAttack;

    [Header("金幣數量")]
    public float PlayerCoin = 0;
    [Header("鑽石數量")]
    public float PlayerJewel = 0;

}
