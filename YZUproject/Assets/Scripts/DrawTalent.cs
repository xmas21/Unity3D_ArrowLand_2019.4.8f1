using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrawTalent : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("所有天賦的按鈕")]
    public Image[] talentBtn;

    private Button drawBtn;    // 抽天賦按鈕

    private void Start()
    {
        drawBtn = GameObject.Find("抽天賦按鈕").GetComponent<Button>();
        drawBtn.onClick.AddListener(Drawtalent);
        //StartCoroutine(drawBtn.onClick.AddListener(Drawtalent()));
    }

    /// <summary>
    /// 抽天賦
    /// </summary>
    private void Drawtalent()
    {
        /*
        for (int j = 0; j < 3; j++)
        {
            talentBtn[0].color += new Color(0, 0, 0, 0.1f);
            talentBtn[1].color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.001f);

        }

        talentBtn[0].color = new Color(0, 0, 0, 0);
        talentBtn[1].color = new Color(0, 0, 0, 0);
        */

        int index;
        index = Random.Range(0, talentBtn.Length);
        //talentBtn[index].color += new Color(255, 255, 0, 255);
        talentBtn[index].transform.localScale = Vector3.one * 2;
        data.talents[index].level++;
        // talentBtn[index].color -= new Color(0, 0, 0, 0);
        talentBtn[index].transform.localScale = Vector3.one;

        Levelup(index);
    }

    private void Levelup(int index)
    {
        if (index == 0)
        {
            data.hp += 180;
        }
        else if (index == 1)
        {
            data.attack += 20;
        }
        else if (index == 2)
        {
            data.speed += 10;
        }
    }
}
