using UnityEngine;

[CreateAssetMenu(fileName = "玩家血量", menuName = "Lobo/玩家資料")]
public class PlayerDate : ScriptableObject
{
    [Header("玩家名稱")]
    public string name;

    [Header("生命值"), Range(100, 30000)]
    public float hp = 300;
    [Header("攻擊傷害"), Range(1, 5000)]
    public float attack = 50;
    [Header("爆擊傷害"), Range(0, 100)]
    public float CriticalAttack = 0;
    [Header("冷卻時間"), Range(0.01f, 2)]
    public float cd = 1f;
    [Header("跑速"), Range(0.01f, 2000)]
    public float speed = 260;
    [Header("減傷"), Range(0.01f, 2000)]
    public float armor = 0;
    [Header("回血"), Range(0.01f, 2000)]
    public float rehp;
    [Header("最大血量"), Range(100, 30000)]
    public float hpMax;
    [Header("武器速度"), Range(1000, 5000)]
    public float power = 1000;
    [Header("武器傷害"), Range(0, 2000)]
    public float WeaponAttack;
    [Header("金幣數量")]
    public float PlayerCoin = 0;
    [Header("鑽石數量")]
    public float PlayerJewel = 0;

    [Header("武器庫")]
    public OwnWeapon[] ownWeapons;
    [Header("武器庫")]
    public WeaponChip[] weaponChips;
    [Header("寵物庫")]
    public OwnPet[] ownPets;
    [Header("角色天賦")]
    public Talent[] talents;
}

[System.Serializable]
public class OwnWeapon
{
    [Header("武器名稱")]
    public string name;
    [Header("是否擁有武器")]
    public bool owned;
    [Header("武器等級")]
    public int level;
}

[System.Serializable]
public class WeaponChip
{
    [Header("武器碎片名稱")]
    public string name;
    [Header("武器碎片數量")]
    public int count;
}

[System.Serializable]
public class OwnPet
{
    [Header("寵物名稱")]
    public string name;
    [Header("是否擁有寵物")]
    public bool owned;
    [Header("寵物等級")]
    public int level;
}

[System.Serializable]
public class Talent
{
    [Header("天賦名稱")]
    public string name;
    [Header("天賦等級")]
    public int level;
}




