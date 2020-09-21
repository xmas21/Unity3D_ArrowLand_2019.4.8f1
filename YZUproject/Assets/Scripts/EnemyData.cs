using UnityEngine;

[CreateAssetMenu(fileName = "怪物資料", menuName = "HWC/怪物")]
public class EnemyData : ScriptableObject
{
    [Header("血量"),Range(30,3000)]
    public float hp;
    [Header("攻擊力"),Range(0,1000)]
    public float attack;
    [Header("冷卻"),Range(0,5)]
    public float cd;
    [Header("速度"),Range(0,1000)]
    public float speed;

}
