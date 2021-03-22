using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Linq;

public class PetNear : MonoBehaviour
{
    [Header("寵物資料")]
    public PetData data;

    private Enemy[] enemys;
    private Animator ani;
    private NavMeshAgent agent;

    private float[] enemyDistanse;
    private float timer;   // 攻擊計時器

    private void Start()
    {
        IgnoreCollision();

        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemys = FindObjectsOfType<Enemy>();

        agent.stoppingDistance = data.stopDistanse;
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// 沒怪時等待
    /// </summary>
    private void Idle()
    {
        ani.SetBool("跑步觸發", false);
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private  void Attack()
    {
        timer = 0;
        ani.SetTrigger("攻擊觸發");
    }

    /// <summary>
    /// 停下等待
    /// </summary>
    private void Wait()
    {
        ani.SetBool("移動開關", false);
        timer += Time.deltaTime;

        if (timer >= data.cd)
        {
            Attack();
        }
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

        enemyDistanse = new float[enemys.Length];

        for (int i = 0; i < enemys.Length; i++)
        {
            enemyDistanse[i] = Vector3.Distance(transform.position, enemys[i].transform.position);
        }

        float min = enemyDistanse.Min();
        int index = enemyDistanse.ToList().IndexOf(min);

        Vector3 posEnemy = enemys[index].transform.position;
        posEnemy.y = transform.position.y;
        transform.LookAt(posEnemy);

        agent.SetDestination(posEnemy);

        if (agent.remainingDistance < data.stopDistanse)
        {
            Wait();
        }
        else
        {
            ani.SetBool("移動開關", true);
        }
    }

    /// <summary>
    /// 忽略碰撞(寵物與敵人 &玩家)
    /// </summary>
    private void IgnoreCollision()
    {
        Physics.IgnoreLayerCollision(12, 10);
        Physics.IgnoreLayerCollision(12, 9);
        Physics.IgnoreLayerCollision(12, 8);
    }
}
