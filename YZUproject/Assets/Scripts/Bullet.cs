using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public bool playerBullet;  // 是玩家的子彈

    private void OnTriggerEnter(Collider other)
    {
        if (!playerBullet)     // 怪物射得子彈
        {
            if (other.name == "玩家")
            {
                other.GetComponent<Player>().Hit(damage);
                Destroy(gameObject);
            }
            else if (other.tag == "牆壁")
            {
                Destroy(gameObject);
            }
        }
        else                  // 玩家射的子彈
        {
            if (other.GetComponent<Enemy>() && other.tag == "敵人")
            {
                other.GetComponent<Enemy>().Hit(damage);
                Destroy(gameObject);
            }
            else if (other.tag == "牆壁")
            {
                Destroy(gameObject);
            }
        }
    }
}
