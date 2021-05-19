using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextAnimation : MonoBehaviour
{
    private TextWriter tw;
    private Text story_text;

    private void Awake()
    {
        story_text = GameObject.Find("開始文字_1").GetComponent<Text>();
        tw = FindObjectOfType<TextWriter>();
    }

    private void Start()
    {
        StartCoroutine(ShowStory());
    }

    private IEnumerator ShowStory()  // 文字內容，文字時間，文字是否透明
    {
        tw.AddWriter(story_text, 
            "西元4682年，你誕生於一個單親家庭中。" +
            "在你出生以前星球已經因為經歷了大大小小生化武器的戰爭。" +
            "導致星球上的生物多數已經突變成怪物了。" +
            "在你18歲的那年，你父親在前往山上劈柴的途中被火龍襲擊而身亡，為此你悲痛欲絕。" +
            "在經過了2天的意志消沉之後你決定拿起父親遺留在現場的武器，誓言要打倒世上所有的怪物。" +
            "為了不希望再有家庭體驗到這種切身之痛，於是你踏上了旅程。", 0.1f, false);
        yield return new WaitForSeconds(0.1f);
    }
}
