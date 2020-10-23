using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public bool playerBullet;

    private void OnTriggerEnter(Collider other)
    {
        if (!playerBullet)
        {
            if (other.name == "玩家")
            {
                other.GetComponent<Player>().Hit(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.GetComponent<Enemy>() && other.tag == "敵人")
            {
                other.GetComponent<Enemy>().Hit(damage);
                Destroy(gameObject);
            }
        }
    }
}
