using UnityEngine;

[CreateAssetMenu(fileName = "怪物資料", menuName = "HWC/怪物")]
public class EnemyData : ScriptableObject
{
    [Header("怪物屬性")]
    public Attributes attributes;
    [Header("血量"), Range(30, 10000)]
    public float hp;
    [Header("血量最大值"), Range(30, 10000)]
    public float hpMax;
    [Header("攻擊力"), Range(0, 2000)]
    public float attack;
    [Header("冷卻"), Range(0, 5)]
    public float cd;
    [Header("速度"), Range(0, 1000)]
    public float speed;
    [Header("停止距離"), Range(0, 1000)]
    public float StopDistanse = 2;

    [Header("攻擊長度"), Range(0, 1000)]
    public float NearAttackLength;
    [Header("攻擊位置")]
    public Vector3 NearAttackPos;
    [Header("攻擊延遲"), Range(0, 3)]
    public float NearAttackDelay;

    [Header("子彈發射速度"), Range(0, 3000)]
    public float farPower = 1500;
    [Header("掉落金幣數量")]
    public Vector2 coinRandom;
    [Header("金幣")]
    public GameObject coin;

}

public enum Attributes
{
    fire, water, wood, light, dark
}
