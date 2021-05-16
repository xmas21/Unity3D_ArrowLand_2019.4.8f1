using UnityEngine;
using System.Collections;

public class EnemyFar : Enemy
{
    [Header("子彈")]
    public GameObject bullet;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + data.NearAttackPos, transform.forward * data.NearAttackLength);
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateBullet());
    }

    /// <summary>
    /// 射子彈
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateBullet()
    {
        yield return new WaitForSeconds(data.NearAttackDelay);

        Vector3 pos = transform.position + new Vector3(data.NearAttackPos.x, data.NearAttackPos.y, 0);

        GameObject temp = Instantiate(bullet, pos, transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.farPower);
        temp.AddComponent<Bullet>();
        temp.GetComponent<Bullet>().damage = data.attack;
        temp.GetComponent<Bullet>().playerBullet = false;

        Destroy(temp, 3.5f);
    }
}
