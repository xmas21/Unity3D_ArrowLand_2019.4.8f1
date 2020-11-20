using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    #region 欄位
    private Joystick joystick; // 虛擬搖桿
    private Rigidbody rig;     // 鋼體
    private Animator ani;      // 動畫控制器
    private Transform target;  // 目標
    private LevelManager levelManager;
    private HpMpManager hpMpManager;
    private float timer;
    private Enemy[] enemys;
    private float[] enemyDistanse;
    public RandomSkill randomSkill;
    private SkillData skillData;

    public float speed = 10;
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("武器")]
    public GameObject bullet;
    #endregion 

    #region 方法
    private void Start()
    {
        rig = GetComponent<Rigidbody>();                                 // 取得元件 (rigidbody) 存入 rig (相同屬性面板)
        ani = GetComponent<Animator>();
        joystick = GameObject.Find("固態搖桿").GetComponent<Joystick>(); // 取得指定元件 (Joystick中的固態搖桿)
        target = GameObject.Find("目標").transform;                      // 短版的指定元件
        levelManager = FindObjectOfType<LevelManager>();
        hpMpManager = GetComponentInChildren<HpMpManager>();
        skillData = FindObjectOfType<SkillData>();
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
    #endregion 

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

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(float damage)
    {
        data.hp -= damage;
        hpMpManager.UpdateHpBar(data.hp, data.hpMax);

        StartCoroutine(hpMpManager.ShowValue(damage, "-", Vector3.one, Color.white));

        if (data.hp <= 0) Dead();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        ani.SetBool("死亡觸發", true);
        enabled = false;

        StartCoroutine(levelManager.ShowRevival());
    }

    /// <summary>
    /// 復活
    /// </summary>
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
        if (timer < data.cd) timer += Time.deltaTime;
        else
        {
            enemys = FindObjectsOfType<Enemy>();
            enemyDistanse = new float[enemys.Length];

            if (enemys.Length == 0)
            {
                levelManager.Pass();
                return;
            }

            timer = 0;

            ani.SetTrigger("攻擊觸發");

            for (int i = 0; i < enemys.Length; i++)
            {
                enemyDistanse[i] = Vector3.Distance(transform.position, enemys[i].transform.position);
            }

            float min = enemyDistanse.Min();

            int index = enemyDistanse.ToList().IndexOf(min);

            Vector3 posEnemy = enemys[index].transform.position;
            posEnemy.y = transform.position.y;
            transform.LookAt(posEnemy);

            Vector3 pos = transform.position + transform.up * 1 + transform.forward * 1.5f;

            Quaternion qua = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

            GameObject temp = Instantiate(bullet, pos, qua);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.power);
            temp.AddComponent<Bullet>();
            temp.GetComponent<Bullet>().damage = data.attack + data.CriticalAttack;
            temp.GetComponent<Bullet>().playerBullet = true;
        }
    }

    public void AttackAbility()
    {
        // 連續射擊
        if (RandomSkill.nameskill.Equals(skillData.Skill1))
        {
            print("連續射擊");
        }
        // 正向劍
        else if (RandomSkill.nameskill.Equals(skillData.Skill2))
        {
            print("正向箭");
        }
        // 背向劍
        else if (RandomSkill.nameskill.Equals(skillData.Skill3))
        {
            print("背向箭");
        }
        // 側向劍
        else if (RandomSkill.nameskill.Equals(skillData.Skill4))
        {
            print("側向箭");
        }
        // 血量增加
        else if (RandomSkill.nameskill.Equals(skillData.Skill5))
        {
            data.hp += 200;
            data.hpMax = data.hp;
        }
        // 攻擊增加
        else if (RandomSkill.nameskill.Equals(skillData.Skill6))
        {
            data.attack += 30;
        }
        // 公訴增加
        else if (RandomSkill.nameskill.Equals(skillData.Skill7))
        {
            data.cd -= 0.3f;
        }
        // 爆擊增加
        else if (RandomSkill.nameskill.Equals(skillData.Skill8))
        {
            data.CriticalAttack += 50;
        }
    }
}
