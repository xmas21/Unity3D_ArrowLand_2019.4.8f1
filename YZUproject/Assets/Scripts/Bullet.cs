using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "玩家")
        {
            other.GetComponent<Player>().Hit(damage);
        }
    }
}
