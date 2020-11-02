using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    #region 
    [Header("金幣音效")]
    public AudioClip sound;

    [HideInInspector]
    public bool pass;

    private Transform player;
    private AudioSource aud;

    #endregion

    #region
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
    #endregion

    #region
    private void HandleCollision()
    {
        Physics.IgnoreLayerCollision(10, 8);
        Physics.IgnoreLayerCollision(10, 9);
    }

    private void GoToPlayer()
    {
        if (pass)
        {
            Physics.IgnoreLayerCollision(10, 10);
            transform.position = Vector3.Lerp(transform.position, player.position, 0.5f * Time.deltaTime * 30);

            if (Vector3.Distance(transform.position, player.position) < 1 && !aud.isPlaying)
            {
                Destroy(gameObject, 0.2f);
                aud.PlayOneShot(sound, 0.3f);
            }
        }
    }

    #endregion
}
