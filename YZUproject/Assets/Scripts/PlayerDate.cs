using UnityEngine;

[CreateAssetMenu(fileName ="玩家血量",menuName ="HWC/玩家資料")]
public class PlayerDate : ScriptableObject
{
    [Header("血量與最大血量"), Range(200, 3000)]
    public float hp = 200;

    public float hpMax;

    [Header("冷卻時間"), Range(0.01f, 2)]
    public float cd = 0.8f;
    [Header("武器速度"), Range(1000, 5000)]
    public float power = 1000;
    [Header("攻擊傷害"), Range(1, 5000)]
    public float attack = 50;
    [Header("爆擊傷害"), Range(1, 100)]
    public float CriticalAttack;
    [Header("金幣數量")]
    public float PlayerCoin = 0;
    [Header("鑽石數量")]
    public float PlayerJewel = 0;


}
