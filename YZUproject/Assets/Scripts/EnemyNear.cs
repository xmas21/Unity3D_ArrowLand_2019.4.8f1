using UnityEngine;
using System.Collections;

public class EnemyNear : Enemy
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + data.NearAttackPos, transform.forward*data.NearAttackLength);
    }

    /// <summary>
    /// 怪物近距離的攻擊
    /// </summary>
    protected override void Attack()
    {
        base.Attack();

        StartCoroutine(DelayAttack());
    }

    /// <summary>
    /// 利用射線攻擊玩家 + 傷害延遲產生
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(data.NearAttackDelay);

        RaycastHit hits; // 區域變數 接收射線資訊

        // out 是把方法內的資料保存在區域變數內

        if (Physics.Raycast(transform.position + data.NearAttackPos, transform.forward, out hits, data.NearAttackLength))
        {

        }       
    }
}
