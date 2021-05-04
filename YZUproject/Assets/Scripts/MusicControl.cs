using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    [Header("圖片庫")]
    public Sprite[] muteimg;

    [Header("背景音樂靜音按鈕")]
    public Button[] bgMuteBtn;
    [Header("背景音樂靜音按鈕的圖片")]
    public Image[] bgMuteImg;

    private AudioSource aud;
    private bool isMute; // 是否靜音
    private float preVolume;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0.02f;
        isMute = false;
        preVolume = aud.volume;
        for (int i = 0; i < bgMuteBtn.Length; i++)
        {
            int index = i;
            bgMuteBtn[index].onClick.AddListener(MuteClick);
        }
    }

    private void Update()
    {
        if (isMute || aud.volume == 0)
        {
            for (int i = 0; i < bgMuteImg.Length; i++)
            {
                int index = i;
                bgMuteImg[index].sprite = muteimg[0];
            }
        }
        else
        {
            for (int i = 0; i < bgMuteImg.Length; i++)
            {
                int index = i;
                bgMuteImg[index].sprite = muteimg[1];
            }
        }
    }

    /// <summary>
    /// 調整音量
    /// </summary>
    /// <param name="newVolume">音量數值</param>
    public void VolumeChanged(float newVolume)
    {
        aud.volume = newVolume;
        isMute = false;
    }

    public void MuteClick()  // 靜音按鈕
    {
        isMute = !isMute;
        if (isMute)
        {
            preVolume = aud.volume;
            aud.volume = 0;
        }
        else
        {
            aud.volume = preVolume;
        }
    }
}

