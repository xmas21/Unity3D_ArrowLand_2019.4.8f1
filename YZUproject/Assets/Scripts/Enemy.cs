using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("輸入資料")]
    public EnemyData data;

    private Animator ani;
    private NavMeshAgent agent;
    private Transform target;
    private HpMpManager hpMpManager;
    private float hp;
    private float timer;


    private void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hpMpManager = GetComponentInChildren<HpMpManager>();

        agent.speed = data.speed;
        agent.stoppingDistance = data.StopDistanse;
        hp = data.hp;

        target = GameObject.Find("玩家").transform;
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

        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);

        agent.SetDestination(targetPos);

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
        int dmg = (int)damage;

        hp -= dmg;

        hpMpManager.UpdateHpBar(hp, data.hpMax);

        StartCoroutine(hpMpManager.ShowValue(dmg, "-", Vector3.one, Color.white));

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
        Destroy(gameObject, 1.2f);
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

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "老虎攻擊範圍")
        {
            Hit(100);
        }
    }
}
