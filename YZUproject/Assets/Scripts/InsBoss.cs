using UnityEngine;

public class InsBoss : MonoBehaviour
{
    [Header("魔王")]
    public GameObject boss;

    private BoxCollider box;
    private LevelManager level;

    private void Start()
    {
        level = FindObjectOfType<LevelManager>();
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "玩家")
        {
            level.insBoss = false;
            boss.SetActive(true);
            box.enabled = false;
        }
    }
}
