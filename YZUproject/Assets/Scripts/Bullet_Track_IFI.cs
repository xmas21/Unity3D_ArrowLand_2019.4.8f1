using UnityEngine;

public class Bullet_Track_IFI : MonoBehaviour
{
    [Header("追蹤速度")]
    public float trackSpeed = 0.1f;

    private Transform player;

    private void Start()
    {
        player = GameObject.Find("玩家_IFI").transform;
    }

    private void Update()
    {
        TrackBullet();
    }

    private void TrackBullet()
    {
        Vector3 posplayer = player.position;
        Vector3 posBullet = transform.position;

        transform.position = Vector3.Lerp(posBullet, posplayer, trackSpeed);
    }
}
