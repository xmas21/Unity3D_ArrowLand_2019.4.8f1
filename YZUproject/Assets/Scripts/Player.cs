using UnityEngine;

public class Player : MonoBehaviour
{
    private Joystick joystick; // 虛擬搖桿
    private Rigidbody rig;     // 鋼體
    private Animator ani;      // 動畫控制器
    private Transform target;  // 目標
    private LevelManager levelManager;
    private HpMpManager hpMpManager;
    private float timer;

    public float speed = 10;
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("武器")]
    public GameObject bullet;



    private void Start()
    {

        rig = GetComponent<Rigidbody>();                               // 取得元件 (rigidbody) 存入 rig (相同屬性面板)
        ani = GetComponent<Animator>();
        joystick = GameObject.Find("固態搖桿").GetComponent<Joystick>(); // 取得指定元件 (Joystick中的固態搖桿)
        target = GameObject.Find("目標").transform;                      // 短版的指定元件
        levelManager = FindObjectOfType<LevelManager>();
        hpMpManager = GetComponentInChildren<HpMpManager>();

        data.hp = data.hpMax;

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "傳送區域")
        {
            levelManager.StartCoroutine("NextLevel");
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float v = joystick.Vertical;              // Z軸 (左右) 垂直
        float h = joystick.Horizontal;            // X軸 (前後) 水平

        rig.AddForce(-h * speed, 0, -v * speed);  // 推力 (水平,0,垂直)

        ani.SetBool("跑步觸發", v != 0 || h != 0);

        Vector3 posPlayer = transform.position;   // 玩家.座標
        Vector3 posTarget = new Vector3(posPlayer.x - h, 0.28f, posPlayer.z - v);  //設定目標跟玩家的相對位置

        target.position = posTarget;

        posTarget.y = posPlayer.y;

        transform.LookAt(posTarget);              // 視野跟蹤的 API

        if (v == 0 && h == 0) Attack();
    }

    public void Hit(float damage)
    {
        data.hp -= damage;
        hpMpManager.UpdateHpBar(data.hp, data.hpMax);

        StartCoroutine(hpMpManager.ShowValue(damage, "-", Vector3.one, Color.white));

        if (data.hp <= 0) Dead();
    }

    private void Dead()
    {
        ani.SetBool("死亡觸發", true);
        enabled = false;

        StartCoroutine(levelManager.ShowRevival());
    }

    public void Revival()
    {
        enabled = true;
        ani.SetBool("死亡觸發", false);
        data.hp = data.hpMax;
        hpMpManager.UpdateHpBar(data.hp, data.hpMax);
        levelManager.CloseRevival();
    }

    private void Attack()
    {
        if (timer < data.cd)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;

            Vector3 pos = transform.position + transform.up * 1 + transform.forward * 1.5f;

            Quaternion qua = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

            GameObject temp = Instantiate(bullet, pos, qua);    
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.power);

        }

    }

}
