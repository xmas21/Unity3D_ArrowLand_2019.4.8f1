using UnityEngine;

public class Camera_Inifinite : MonoBehaviour
{
    private Transform player;

    [Header("跟蹤速度"), Range(0, 10)]
    public float speed = 1.5f;
    [Header("上方限制")]
    public float top;
    [Header("下方限制")]
    public float bottom;
    [Header("左方限制")]
    public float left;
    [Header("右方限制")]
    public float right;

    private void Start()
    {
        player = GameObject.Find("玩家_IFI").transform;
    }

    private void LateUpdate()
    {
        Track();
    }

    private void Track()
    {
        Vector3 posplayer = player.position;
        Vector3 posCamera = transform.position;

        posplayer.x = Mathf.Clamp(posplayer.x, left, right);
        posplayer.z = Mathf.Clamp(posplayer.z, top, bottom);

        transform.position = Vector3.Lerp(posCamera, posplayer, 0.5f);   // camera 往 player 前進 0.5f
    }
}
