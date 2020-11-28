using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomSkill : MonoBehaviour
{
    [Header("圖片區域")]
    public Sprite[] spritesBlur;
    public Sprite[] spritesSkill;

    public string[] nameSkill = { "連續射擊", "正向箭", "背向箭", "側向箭", "血量增加", "攻擊增加", "攻速增加", "爆擊增加" };

    [Header("捲動速度"), Range(0.001f, 0.1f)]
    public float speed = 0.1f;
    [Header("隨機圖片輪迴次數"), Range(1, 30)]
    public int count = 3;

    public AudioClip soundScroll;
    public AudioClip soundSkill;

    private AudioSource aud;
    private Image imgSkill;
    private Button btn;
    private Text textName;
    private GameObject skillPanel;
    public int index;
    private Animator ani;
    public static string nameskill;
    private Player player;


    private void Start()
    {
        aud = GetComponent<AudioSource>();
        imgSkill = GetComponent<Image>();
        btn = GetComponent<Button>();
        player = FindObjectOfType<Player>();
        textName = transform.GetChild(0).GetComponent<Text>(); // 取得子物件
        skillPanel = GameObject.Find("隨機技能");

        btn.onClick.AddListener(chooseSkill);

        StartCoroutine(RandomEffect());

    }


    /// <summary>
    /// 選取技能後的動作
    /// </summary>
    private void chooseSkill()
    {
        skillPanel.SetActive(false);
        nameskill = nameSkill[index];
        player.BuffAbility();
    }

    /// <summary>
    /// 技能動畫
    /// </summary>
    private IEnumerator RandomEffect()
    {
        btn.interactable = false;

        // 開始捲動
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < spritesBlur.Length; i++)
            {
                aud.PlayOneShot(soundScroll, 0.1f);
                imgSkill.sprite = spritesBlur[i];
                yield return new WaitForSeconds(speed);
            }
        }

        // 捲動結束後
        btn.interactable = true;
        index = Random.Range(0, spritesSkill.Length);
        imgSkill.sprite = spritesSkill[index];
        aud.PlayOneShot(soundSkill, 0.1f);
        textName.text = nameSkill[index];
    }
}
