using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public bool playerBullet;  // 是玩家的子彈

    private void OnTriggerEnter(Collider col)
    {
        if (!playerBullet)     // 怪物射得子彈
        {
            if (col.name == "玩家")
            {
                col.GetComponent<Player>().Hit(damage);
                Destroy(gameObject);
            }
            else if (col.name == "玩家_IFI")
            {
                col.GetComponent<Player_IFI>().Hit(damage);
                Destroy(gameObject);
            }
            else if (col.tag == "牆壁")
            {
                Destroy(gameObject);
            }
        }
        else                  // 玩家射的子彈
        {
            if (col.GetComponent<Enemy_IFI>() && col.tag == "敵人")
            {
                col.GetComponent<Enemy_IFI>().Hit(damage);
                Destroy(gameObject);
            }
            else if (col.GetComponent<Enemy>() && col.tag == "敵人")
            {
                col.GetComponent<Enemy>().Hit(damage);
                Destroy(gameObject);
            }
            else if (col.tag == "牆壁")
            {
                Destroy(gameObject);
            }
        }
    }
}
