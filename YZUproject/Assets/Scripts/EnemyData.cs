﻿using UnityEngine;

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
    [Header("停止距離"),Range(0,1000)]
    public float StopDistanse = 2;
    
    [Header("攻擊長度"),Range(0,1000)]
    public float NearAttackLength;
    [Header("攻擊位置")]
    public Vector3 NearAttackPos;
    [Header("攻擊延遲"),Range(0,3)]
    public float NearAttackDelay;


}
