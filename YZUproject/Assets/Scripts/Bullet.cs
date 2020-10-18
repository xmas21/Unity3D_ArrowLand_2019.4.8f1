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
            }
        }
        else
        {
            if (other.tag == "敵人")
            {
                other.GetComponent<Enemy>().Hit(damage);
            }
        }
    }
}
