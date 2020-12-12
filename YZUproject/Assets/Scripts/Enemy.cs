using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("輸入資料")]
    public EnemyData data;

    private Animator ani;
    private NavMeshAgent agent;
    private Transform target;
    private float timer;
    private HpMpManager hpMpManager;
    private float hp;
    private PlayerDate playerDate;


    private void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.stoppingDistance = data.StopDistanse;
        hp = data.hp;

        target = GameObject.Find("玩家").transform;
        hpMpManager = GetComponentInChildren<HpMpManager>();
    }

    private void Update()
    {
        Move();
    }


    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        if (ani.GetBool("死亡觸發")) return;

        agent.SetDestination(target.position);

        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            Wait();
        }
        else
        {
            ani.SetBool("跑步觸發", true);
        }

    }

    /// <summary>
    /// 等待
    /// </summary>
    private void Wait()
    {
        ani.SetBool("跑步觸發", false);
        timer += Time.deltaTime;

        if (timer >= data.cd)
        {
            Attack();
        }
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected virtual void Attack()
    {
        timer = 0;
        ani.SetTrigger("攻擊觸發");
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害</param>
    public void Hit(float damage)
    {
        hp -= damage;
        hpMpManager.UpdateHpBar(hp, data.hpMax);

        StartCoroutine(hpMpManager.ShowValue(damage, "-", Vector3.one, Color.white));

        if (hp <= 0) Dead();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        ani.SetBool("死亡觸發", true);
        agent.isStopped = true;
        Destroy(this);
        Destroy(gameObject, 0.6f);
        DropProp();
    }

    /// <summary>
    /// 掉金幣
    /// </summary>
    private void DropProp()
    {
        int r = (int)Random.Range(data.coinRandom.x, data.coinRandom.y);

        target.GetComponent<Player>().data.PlayerCoin += r;

        for (int i = 0; i < r; i++)
        {
            Instantiate(data.coin, transform.position + transform.up * 2, Quaternion.identity);
        }

    }
}
