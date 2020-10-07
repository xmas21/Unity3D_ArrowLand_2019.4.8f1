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

    private void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.stoppingDistance = data.StopDistanse;

        target = GameObject.Find("玩家").transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
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

    private void Hit(float damage)
    {

    }

    private void Dead()
    {

    }

    private void Prop()
    {

    }
}
