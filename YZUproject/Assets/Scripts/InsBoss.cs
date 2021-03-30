using UnityEngine;

public class InsBoss : MonoBehaviour
{
    [Header("魔王")]
    public GameObject boss;

    private BoxCollider box;

    private void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "玩家")
        {
            boss.SetActive(true);
            box.enabled = false;
        }
    }
}
