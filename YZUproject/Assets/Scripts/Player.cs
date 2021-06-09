using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Player : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("預設武器")]
    public GameObject test_bullet;
    [Header("預設寵物")]
    public GameObject test_pet;

    [Header("生命"), Range(0, 1000)]
    public static float hp;
    [Header("最大生命"), Range(0, 1000)]
    public static float hpMax;
    [Header("角色攻擊力"), Range(0, 1000)]
    public static float attack;
    [Header("武器攻擊力"), Range(0, 1000)]
    public static float attack_WP;
    [Header("角色爆擊傷害"), Range(0, 1000)]
    public static float criticalAttack;
    [Header("攻擊冷卻"), Range(0, 1000)]
    public static float cd;
    [Header("移動速度"), Range(0, 1000)]
    public static float speed;
    [Header("每秒回血"), Range(0, 1000)]
    public static float rehp;

    public static GameObject bullet;
    public static GameObject pet1;

    private float timer;
    private float[] enemyDistanse;

    private Text hpText;
    private Joystick joystick; // 虛擬搖桿
    private Rigidbody rig;     // 鋼體
    private Animator ani;      // 動畫控制器
    private Transform target;  // 目標
    private LevelManager levelManager;
    private HpMpManager hpMpManager;
    private Enemy[] enemys;
    private SkillData skillData;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();                                 // 取得元件 (rigidbody) 存入 rig (相同屬性面板)
        ani = GetComponent<Animator>();
        hpMpManager = GetComponentInChildren<HpMpManager>();
        hpText = transform.GetChild(3).GetChild(3).GetComponent<Text>();
        joystick = GameObject.Find("固態搖桿").GetComponent<Joystick>(); // 取得指定元件 (Joystick中的固態搖桿)
        target = GameObject.Find("目標").transform;                      // 短版的指定元件
        levelManager = FindObjectOfType<LevelManager>();
        skillData = FindObjectOfType<SkillData>();

        hp = data.hp;
        hpMax = data.hpMax;
        attack = data.attack;
        attack_WP = data.WeaponAttack;
        criticalAttack = data.CriticalAttack;
        cd = data.cd;
        speed = data.speed;
        rehp = data.rehp;

        bullet = test_bullet; // 設定預設子彈
        pet1 = test_pet;      // 設定預設寵物
        data.hp = data.hpMax; // 設定生命力

        Instantiate(pet1);
    }

    private void FixedUpdate()
    {
        UpdateValue();
        Move();
        RestoreHp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "傳送區域")
        {
            levelManager.StartCoroutine("NextLevel");
        }
        else if (other.tag == "敵人")
        {
            Hit(30);
        }
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(float damage)
    {
        float dmg = damage * (1 - data.armor); // 減傷後的傷害

        int dmgValue = (int)dmg;  // 把浮點樹轉整數(無條件捨去)

        hp -= dmgValue;

        StartCoroutine(hpMpManager.ShowValue(dmgValue, "-", Vector3.one, Color.white));

        if (hp <= 0) Dead();
    }

    public void Revival() // 復活
    {
        enabled = true;
        ani.SetBool("死亡觸發", false);
        hp = data.hp;
        hpMpManager.UpdateHpBar(hp, hpMax);
        levelManager.CloseRevival();
    }

    public void AttackAbility() // 技能效果
    {
        // 連續射擊 
        if (RandomSkill.nameskill.Equals(skillData.Skill1))
        {
            Bullet(-1.5f, transform.forward, transform.eulerAngles.x + 180, transform.eulerAngles.y, transform.eulerAngles.z, Vector3.zero, 0);
        }

        // 正向劍 
        else if (RandomSkill.nameskill.Equals(skillData.Skill2))
        {
            Bullet(1.5f, transform.forward, transform.eulerAngles.x + 180, transform.eulerAngles.y, transform.eulerAngles.z, transform.right, 0.25f);
        }

        // 背向劍 
        else if (RandomSkill.nameskill.Equals(skillData.Skill3))
        {
            Bullet(-1.5f, -transform.forward, transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z, Vector3.zero, 0);
        }

        // 側向劍 
        else if (RandomSkill.nameskill.Equals(skillData.Skill4))
        {
            Bullet(0.5f, transform.right, transform.eulerAngles.x + 180, transform.eulerAngles.y + 90, transform.eulerAngles.z, transform.right, 1);

            Bullet(0.5f, -transform.right, transform.eulerAngles.x + 180, transform.eulerAngles.y - 90, transform.eulerAngles.z, -transform.right, 1);
        }
    }

    public void BuffAbility() // 技能Buff
    {
        // 血量增加
        if (RandomSkill.nameskill.Equals(skillData.Skill5))
        {
            hp += 200;
            hpMax += 200;
        }

        // 攻擊增加
        else if (RandomSkill.nameskill.Equals(skillData.Skill6))
        {
            attack += 30;
        }

        // 攻速增加
        else if (RandomSkill.nameskill.Equals(skillData.Skill7))
        {
            cd -= 0.2f;
        }

        // 爆擊增加
        else if (RandomSkill.nameskill.Equals(skillData.Skill8))
        {
            criticalAttack += 30;
        }
    }

    private void Move() // 移動
    {
        float h = joystick.Horizontal;                // X軸 (左右) 水平
        float v = joystick.Vertical;                  // Z軸 (前後) 垂直

        rig.AddForce(-h * speed, 0, -v * speed);      // 推力 (水平,0,垂直)

        ani.SetBool("跑步觸發", v != 0 || h != 0);

        Vector3 posPlayer = transform.position;       // 玩家.座標
        Vector3 posTarget = new Vector3(posPlayer.x - h, 0.28f, posPlayer.z - v);  //設定目標跟玩家的相對位置

        target.position = posTarget;

        posTarget.y = posPlayer.y;

        transform.LookAt(posTarget);                   // 視野跟蹤的 API

        if (v == 0 && h == 0) Attack();
    }

    private void Dead() // 死亡
    {
        hp = 0;
        ani.SetBool("死亡觸發", true);
        enabled = false;

        StartCoroutine(levelManager.ShowRevival());
    }

    private void Attack() // 攻擊
    {
        if (timer < cd) timer += Time.deltaTime;
        else
        {
            timer = 0;

            enemys = FindObjectsOfType<Enemy>();        // 找尋腳本物件<敵人>
            enemyDistanse = new float[enemys.Length];

            for (int i = 0; i < enemys.Length; i++)
            {
                enemyDistanse[i] = Vector3.Distance(transform.position, enemys[i].transform.position);  // 計算每個敵人與玩家之間的距離
            }

            if (enemys.Length == 0)
            {
                return;
            }
            else
            {
                float min = enemyDistanse.Min();                      // 取得距離最近的敵人

                int index = enemyDistanse.ToList().IndexOf(min);

                Vector3 posEnemy = enemys[index].transform.position;
                posEnemy.y = transform.position.y;
                transform.LookAt(posEnemy);                           // 鎖定敵人

                ani.SetTrigger("攻擊觸發");                           // 動畫觸發器

                Vector3 pos = transform.position + transform.up * 1 + transform.forward * 1.5f;   // 武器生成位置

                Quaternion qua = Quaternion.Euler(transform.eulerAngles.x + 180, transform.eulerAngles.y, transform.eulerAngles.z); // 武器生成角度

                GameObject temp = Instantiate(bullet, pos, qua);       // 生成(物件,位置,角度)
                temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.power);
                temp.AddComponent<Bullet>();
                temp.GetComponent<Bullet>().damage = attack + criticalAttack + attack_WP;
                temp.GetComponent<Bullet>().playerBullet = true;

                AttackAbility();

                Destroy(temp, 5f);                                     // 刪除沒有攻擊到敵人殘留的武器 => 省效能
            }
        }
    }

    /// <summary>
    /// 技能
    /// </summary>
    /// <param name="forward1">劍生成的位置(前後)</param>
    /// <param name="ways">劍飛的方向(前後左右)</param>
    /// <param name="x1">劍的轉向(x)</param>
    /// <param name="y1">劍的轉向(y)</param>
    /// <param name="z1">劍的轉向(z)</param>
    /// <param name="way2">劍生成的位置(左右)</param>
    /// <param name="a1">劍的位置遠近</param>
    private void Bullet(float forward1, Vector3 ways, float x1, float y1, float z1, Vector3 way2, float a1)
    {
        Vector3 pos = transform.position + transform.up * 1 + transform.forward * forward1 + way2 * a1;
        Quaternion qua = Quaternion.Euler(x1, y1, z1);
        GameObject temp = Instantiate(bullet, pos, qua);
        temp.GetComponent<Rigidbody>().AddForce(ways * data.power);
        temp.AddComponent<Bullet>();
        temp.GetComponent<Bullet>().damage = attack + criticalAttack + attack_WP;
        temp.GetComponent<Bullet>().playerBullet = true;
    }

    private void UpdateValue()// 生命值更新
    {
        hpText.text = hp.ToString("f0");
    }

    private void RestoreHp() // 每秒回復生命
    {
        hp = Mathf.Clamp(hp, 0, hpMax);
        hp += rehp * Time.deltaTime;
        hpMpManager.UpdateHpBar(hp, hpMax);
    }

}