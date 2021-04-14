using UnityEngine;

public class InsBoss : MonoBehaviour
{
    [Header("魔王")]
    public GameObject boss;

    private BoxCollider box;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "玩家")
        {
            player.insBoss = true;
            boss.SetActive(true);
            box.enabled = false;
        }
    }
}
