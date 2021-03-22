using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class PetFar : MonoBehaviour
{
    [Header("子彈")]
    public GameObject bullet;
    [Header("追蹤速度"), Range(0, 100)]
    public float trackSpeed = 0.1f;
    [Header("寵物資料")]
    public PetData data;

    private Enemy[] enemys;
    private Animator ani;
    private NavMeshAgent agent;
    private Transform target;       //  玩家位置

    private float[] enemyDistanse;
    private float PetDistanse;      // 寵物跟玩家的距離
    private float timer;

    private void Start()
    {
        IgnoreCollision();
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = data.stopDistanse;
        target = GameObject.Find("玩家").transform;

        enemys = FindObjectsOfType<Enemy>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        if (enemys.Length == 0)
        {
            Idle();
        }

        Vector3 targetPos = target.position;    // 玩家位置
        targetPos.y = transform.position.y;

        Vector3 PetPos = transform.position;    // 寵物位置

        PetDistanse = Vector3.Distance(PetPos, targetPos);

        if (PetDistanse > data.stopDistanse)
        {
            transform.LookAt(targetPos);

            ani.SetBool("跑步觸發", true);

            PetPos = Vector3.Lerp(PetPos, targetPos, trackSpeed * Time.deltaTime); // 讓怪物跟著玩家走

            transform.position = PetPos;
        }

        else if (agent.remainingDistance < agent.stoppingDistance)
        {
            Wait();
        }
    }

    /// <summary>
    /// 等待
    /// </summary>
    private void Wait()
    {
        ani.SetBool("跑步觸發", false);

        LookEnemy();

        timer += Time.deltaTime;

        if (timer >= data.cd)
        {
            Attack();
        }
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        LookEnemy();

        if (enemys.Length == 0)
        {
            Idle();
        }

        timer = 0;
        ani.SetTrigger("攻擊觸發");

        Vector3 pos = transform.position + transform.up * 1 + transform.forward * 1f; // 生成位置

        Quaternion qua = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z); // 生成角度

        GameObject temp = Instantiate(bullet, pos, qua);
        temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.power);
        temp.AddComponent<Bullet>();
        temp.GetComponent<Bullet>().damage = data.attack;
        temp.GetComponent<Bullet>().playerBullet = true;
    }

    /// <summary>
    /// 看敵人
    /// </summary>
    private void LookEnemy()
    {
        enemyDistanse = new float[enemys.Length];

        if (enemys.Length == 0)
        {
            Idle();
        }

        for (int i = 0; i < enemys.Length; i++)
        {
            enemyDistanse[i] = Vector3.Distance(transform.position, enemys[i].transform.position);
        }

        float min = enemyDistanse.Min();
        int index = enemyDistanse.ToList().IndexOf(min);

        Vector3 posEnemy = enemys[index].transform.position;
        posEnemy.y = transform.position.y;
        transform.LookAt(posEnemy);
    }

    /// <summary>
    /// 沒怪時的動作
    /// </summary>
    private void Idle()
    {
        ani.SetBool("跑步觸發", false);
    }

    /// <summary>
    /// 無視碰撞
    /// </summary>
    private void IgnoreCollision()
    {
        Physics.IgnoreLayerCollision(12, 10);
        Physics.IgnoreLayerCollision(12, 9);
        Physics.IgnoreLayerCollision(12, 8);
    }
}
