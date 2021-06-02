using UnityEngine;
using UnityEngine.AI;

public class Enemy_IFI : MonoBehaviour
{
    [Header("輸入資料")]
    public EnemyData data;

    private Animator ani;
    private NavMeshAgent agent;
    private Transform target;
    private HpMpManager hpMpManager;
    private LevelManager_Ifinite LM_I;
    private float hp;
    private float timer;


    private void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hpMpManager = GetComponentInChildren<HpMpManager>();
        LM_I = FindObjectOfType<LevelManager_Ifinite>();

        agent.speed = data.speed;
        agent.stoppingDistance = data.StopDistanse;
        hp = data.hp;

        target = GameObject.Find("玩家_IFI").transform;
    }

    private void Update()
    {
        Move();
    }

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

    private void Wait()
    {
        ani.SetBool("跑步觸發", false);
        timer += Time.deltaTime;

        if (timer >= data.cd)
        {
            Attack();
        }
    }

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

    private void Dead()
    {
        LM_I.kill_count++;
        ani.SetBool("死亡觸發", true);
        agent.isStopped = true;
        Destroy(this);
        Destroy(gameObject, 1.2f);
        DropProp();
    }

    private void DropProp()
    {
        int r = (int)Random.Range(data.coinRandom.x, data.coinRandom.y);

        target.GetComponent<Player_IFI>().data.PlayerCoin += r;

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
