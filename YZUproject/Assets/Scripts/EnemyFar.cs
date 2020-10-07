using UnityEngine;
using System.Collections;

public class EnemyFar : Enemy
{
    [Header("子彈")]
    public GameObject bullet;


    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateBullet());
    }

    private IEnumerator CreateBullet()
    {
        yield return new WaitForSeconds(data.NearAttackDelay);

        Vector3 pos = transform.position + new Vector3(0, data.NearAttackPos.y, 0);

        pos += transform.forward * data.NearAttackPos.z;

        Instantiate(bullet, pos, transform.rotation);
    }
}
