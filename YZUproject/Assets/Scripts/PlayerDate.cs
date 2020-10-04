using UnityEngine;

[CreateAssetMenu(fileName ="玩家血量",menuName ="HWC/玩家資料")]
public class PlayerDate : ScriptableObject
{
    [Header("血量與最大血量"), Range(200, 3000)]
    public float hp = 200;

    public float hpMax;

}
