using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    private Joystick joystick; // 虛擬搖桿
    private Rigidbody rig;     // 鋼體
    private Animator ani;      // 動畫控制器
    private Transform target;  // 目標

    public float speed = 10;

    #endregion

    #region 方法 遊戲內的操作

    private void Start()
    {
        rig = GetComponent <Rigidbody> ();                               // 取得元件 (rigidbody) 存入 rig (相同屬性面板)
        ani = GetComponent <Animator> ();
        joystick = GameObject.Find("固態搖桿").GetComponent<Joystick>(); // 取得指定元件 (Joystick中的固態搖桿)
        target = GameObject.Find("目標").transform;                      // 短版的指定元件
    }

    private void FixedUpdate()
    {
        Move();
    }


    #endregion


    #region 事件 遊戲操作的事先定義

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float v = joystick.Vertical;              // Z軸 (左右) 垂直
        float h = joystick.Horizontal;            // X軸 (前後) 水平

        rig.AddForce(-h * speed, 0, -v * speed);  // 推力 (水平,0,垂直)

        ani.SetBool("跑步觸發",v !=0 || h != 0);

        Vector3 posPlayer = transform.position;   // 玩家.座標
        Vector3 posTarget = new Vector3 (posPlayer.x - h ,0.28f, posPlayer.z - v);  //設定目標跟玩家的相對位置

        target.position = posTarget;

        posTarget.y = posPlayer.y;

        transform.LookAt(posTarget);              // 視野跟蹤的 API
    }

   
    #endregion
}
