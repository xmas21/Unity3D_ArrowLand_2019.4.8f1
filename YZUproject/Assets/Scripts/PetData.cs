using UnityEngine;

[CreateAssetMenu(fileName = "寵物資訊", menuName = "HWC/寵物資料")]
public class PetData : ScriptableObject
{
    [Header("冷卻時間"), Range(0.01f, 20)]
    public float cd = 3f;
    [Header("武器速度"), Range(1000, 5000)]
    public float power = 1000;
    [Header("攻擊傷害"), Range(1, 5000)]
    public float attack = 30;
    [Header("速度"), Range(0, 1000)]
    public float speed;
    [Header("停止距離"), Range(0, 1000)]
    public float stopDistanse = 5;
    [Header("子彈發射速度"), Range(0, 3000)]
    public float farPower = 1500;

}
