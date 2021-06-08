using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("金幣音效")]
    public AudioClip sound;

    [HideInInspector]
    public bool pass;

    private Transform player;
    private AudioSource aud;

    private void Start()
    {
        Physics.IgnoreLayerCollision(10, 10, false);
        aud = GetComponent<AudioSource>();
        player = GameObject.Find("玩家").transform;
        HandleCollision();
    }

    private void Update()
    {
        GoToPlayer();
    }

    /// <summary>
    /// 避免碰撞
    /// </summary>
    private void HandleCollision()
    {
        Physics.IgnoreLayerCollision(10, 8);
        Physics.IgnoreLayerCollision(10, 9);
    }

    /// <summary>
    /// 跑向玩家
    /// </summary>
    private void GoToPlayer()
    {
        if (pass)
        {
            Physics.IgnoreLayerCollision(10, 10);
            transform.position = Vector3.Lerp(transform.position, player.position, 0.5f * Time.deltaTime * 30);

            if (Vector3.Distance(transform.position, player.position) < 1 && !aud.isPlaying)
            {
                Destroy(gameObject, 0.2f);
                aud.PlayOneShot(sound, 0.05f);
            }
        }
    }
}
