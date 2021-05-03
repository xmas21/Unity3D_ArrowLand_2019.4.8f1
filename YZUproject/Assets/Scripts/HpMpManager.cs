using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpMpManager : MonoBehaviour
{
    private Image hpBar;
    private RectTransform rtValue;
    private Text textValue;
    private Text texthp;

    private void Start()
    {
        hpBar = transform.GetChild(1).GetComponent<Image>();
        rtValue = transform.GetChild(2).GetComponent<RectTransform>();
        textValue = transform.GetChild(2).GetComponent<Text>();
        texthp = transform.GetChild(3).GetComponent<Text>();
    }

    private void Update()
    {
        FixAngle();
    }

    /// <summary>
    /// 固定血條螢幕角度
    /// </summary>
    private void FixAngle()
    {
        transform.eulerAngles = new Vector3(50, -180, 0);
    }

    /// <summary>
    /// 更新生命條
    /// </summary>
    /// <param name="hpcurrent">當前生命</param>
    /// <param name="hpMax">最大生命</param>
    public void UpdateHpBar(float hpcurrent, float hpMax)
    {
        hpBar.fillAmount = hpcurrent / hpMax;
    }

    /// <summary>
    /// 顯示傷害
    /// </summary>
    /// <param name="value">傷害</param>
    /// <param name="mark">正負</param>
    /// <param name="size">大小</param>
    /// <param name="valueColor">顏色</param>
    /// <returns></returns>
    public IEnumerator ShowValue(int value, string mark, Vector3 size, Color valueColor)
    {
        textValue.text = mark + value;  // 內容為 : 符號 + 數值 -90 , +50
        valueColor.a = 0;               // 顏色.透明度 = 0
        textValue.color = valueColor;   // 更新文字.顏色
        rtValue.localScale = size;      // 更新文字.尺寸

        for (int i = 0; i < 15; i++)
        {
            textValue.color += new Color(0, 0, 0, 0.4f);
            rtValue.anchoredPosition += Vector2.up * 4;
            yield return new WaitForSeconds(0.01f);
        }

        rtValue.anchoredPosition = new Vector2(0, 60);
        textValue.color = new Color(0, 0, 0, 0);
    }
}
