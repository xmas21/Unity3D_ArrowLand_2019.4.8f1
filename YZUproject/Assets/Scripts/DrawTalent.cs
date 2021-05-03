using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrawTalent : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("所有天賦的按鈕圖案")]
    public Image[] talentBtn;
    [Header("循環次數")]
    public int loop = 3;
    [Header("每次抽獎花費")]
    public int cost = 100;
    [Header("總共抽獎次數")]
    public int count = 0;

    public float hpValue = 0;
    public float atkValue = 0;
    public float criticalValue = 0;
    public float speedValue = 0;
    public float armorValue = 0;
    public float rehpValue = 0;

    private Text costText;     // 花費金錢
    private Text countText;    // 總共抽獎次數
    private Text hpTL;         // TL 代表天賦等級
    private Text atkTL;        // 攻擊天賦等級
    private Text criticalTL;   // 爆擊
    private Text speedTL;      // 跑速
    private Text armorTL;      // 減傷
    private Text rehpTL;       // 回復生命

    private Button drawBtn;    // 抽天賦按鈕

    private void Start()
    {
        cost = 100;
        data.talents[0].level = 0;
        data.talents[1].level = 0;
        data.talents[2].level = 0;
        data.talents[3].level = 0;
        data.talents[4].level = 0;
        data.talents[5].level = 0;

        costText = GameObject.Find("升級花費").GetComponent<Text>();
        countText = GameObject.Find("次數").GetComponent<Text>();
        hpTL = GameObject.Find("生命等級").GetComponent<Text>();
        atkTL = GameObject.Find("力量等級").GetComponent<Text>();
        criticalTL = GameObject.Find("爆擊等級").GetComponent<Text>();
        speedTL = GameObject.Find("跑速等級").GetComponent<Text>();
        armorTL = GameObject.Find("減傷等級").GetComponent<Text>();
        rehpTL = GameObject.Find("回血等級").GetComponent<Text>();
        drawBtn = GameObject.Find("抽天賦按鈕").GetComponent<Button>();

        costText.text = "$" + cost.ToString("F0");
        countText.text = count.ToString("F0") + "次";
        drawBtn.onClick.AddListener(() => { StartCoroutine(Drawtalent()); });
    }

    /// <summary>
    /// 抽天賦
    /// </summary>
    private IEnumerator Drawtalent()
    {
        if (data.PlayerCoin >= cost)
        {
            data.PlayerCoin -= cost;

            for (int j = 0; j < loop; j++)
            {
                for (int i = 0; i < talentBtn.Length; i++)
                {
                    talentBtn[i].color += new Color(0, 0, 0, 0.7f);
                    yield return new WaitForSeconds(0.2f);
                    talentBtn[i].color = new Color(1, 1, 0, 0);
                }
            }

            int index;
            index = Random.Range(0, talentBtn.Length);
            talentBtn[index].transform.localScale = Vector3.one * 1.5f;
            talentBtn[index].color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.3f);
            talentBtn[index].color = new Color(1, 0, 0, 1);
            yield return new WaitForSeconds(0.3f);
            talentBtn[index].color = new Color(1, 1, 0, 1);
            yield return new WaitForSeconds(0.3f);
            talentBtn[index].color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(1f);
            data.talents[index].level++;
            talentBtn[index].color = new Color(1, 1, 0, 0);
            talentBtn[index].transform.localScale = Vector3.one;

            cost += 100;
            count++;

            Levelup(index);
            UpdateValue();
        }
        else
        {

        }
    }

    /// <summary>
    /// 天賦升級
    /// </summary>
    /// <param name="index">天賦編號</param>
    private void Levelup(int index)
    {
        if (index == 0)
        {
            hpValue += 150;
            data.hp += hpValue;
        }
        else if (index == 1)
        {
            atkValue += 20;
            data.attack += atkValue;
        }
        else if (index == 2)
        {
            criticalValue += 20;
            data.CriticalAttack += criticalValue;
        }
        else if (index == 3)
        {
            speedValue += 5;
            data.speed += speedValue;
        }
        else if (index == 4)
        {
            armorValue += 0.02f;
            data.armor += armorValue;
        }
        else if (index == 5)
        {
            rehpValue += 2;
            data.rehp += rehpValue;
        }
    }

    /// <summary>
    /// 更新數值
    /// </summary>
    private void UpdateValue()
    {
        countText.text = count.ToString("F0") + "次";
        costText.text = "$" + cost.ToString("F0");
        hpTL.text = data.talents[0].level.ToString("F0");
        atkTL.text = data.talents[1].level.ToString("F0");
        criticalTL.text = data.talents[2].level.ToString("F0");
        speedTL.text = data.talents[3].level.ToString("F0");
        armorTL.text = data.talents[4].level.ToString("F0");
        rehpTL.text = data.talents[5].level.ToString("F0");
    }
}
