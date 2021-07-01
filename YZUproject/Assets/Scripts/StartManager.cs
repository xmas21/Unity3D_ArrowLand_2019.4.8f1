using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate date;
    [Header("開始文字_1")]
    public GameObject start_1_Text;
    [Header("開始文字_2")]
    public GameObject start_2_Text;
    [Header("開始文字_3")]
    public GameObject start_3_Text;
    [Header("開始按鈕_2物件")]
    public GameObject start_2_OBject;
    [Header("開始按鈕_3物件")]
    public GameObject start_3_Object;
    [Header("提示文字")]
    public GameObject tip_Text;
    [Header("名字輸入欄")]
    public GameObject name_input;
    [Header("字數錯誤")]
    public GameObject name_error;
    [Header("玩家名稱")]
    public Text player_name;
    [Header("開始按鈕_2按鈕")]
    public Button start_2_Btn;
    [Header("開始按鈕_3按鈕")]
    public Button start_3_Btn;

    private DataSave ds;
    private Button start_1_Btn;

    private void Start()
    {
        ds = FindObjectOfType<DataSave>();
        start_1_Btn = GameObject.Find("開始按鈕_1").GetComponent<Button>();
        Invoke("ShowTip", 4f);

        ClickBtn();
    }

    private void ClickBtn()                 // 點擊按鈕
    {
        start_1_Btn.onClick.AddListener(() => { StartCoroutine(CloseStory()); });
        start_2_Btn.onClick.AddListener(ChooseReady);
        start_3_Btn.onClick.AddListener(() => { StartCoroutine(LoadScene()); });
    }

    private void ChooseReady()              // 第二階段
    {
        start_2_Text.SetActive(false);
        start_2_OBject.SetActive(false);
        start_3_Text.SetActive(true);
        start_3_Object.SetActive(true);
        name_input.SetActive(true);
    }

    private void ShowTip()                  // 顯示提示文字
    {
        tip_Text.SetActive(true);
        start_1_Btn.interactable = true;
    }

    private IEnumerator CloseStory()        // 第一階段
    {
        tip_Text.SetActive(false);
        start_1_Text.SetActive(false);
        start_2_Text.SetActive(true);
        start_1_Btn.interactable = false;
        yield return new WaitForSeconds(0.5f);
        start_2_OBject.SetActive(true);
    }

    private IEnumerator LoadScene()         // 切換場景
    {
        if (player_name.text.Length < 7 && player_name.text.Length > 0)
        {
            date.player_name = player_name.text;
            ds.SaveData();
            SceneManager.LoadScene("2遊戲主選單");
            yield return null;
        }
        else
        {
            name_error.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            name_error.SetActive(false);
        }
    }
}
