using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class BeginManager : MonoBehaviour
{
    [Header("選擇畫面")]
    public GameObject choose_Panel;
    [Header("創建畫面")]
    public GameObject create_Panel;
    [Header("載入畫面")]
    public GameObject load__Panel;
    [Header("創建角色按鈕")]
    public Button build_player;
    [Header("載入角色按鈕")]
    public Button load_player;
    [Header("依舊創建角色按鈕")]
    public Button still_create_Btn;
    [Header("依舊創建角色按鈕")]
    public Text still_create_Text;
    [Header("回上一步按鈕")]
    public Button back_Btn;
    [Header("創建角色2按鈕")]
    public Button create2_Btn; 
    [Header("回上一步按鈕")]
    public Button back2_Btn;

    private string filepath;

    private Button begin_btn;         // 最一開始的按鈕
    public DataSave ds;

    private void Start()
    {
        ds = FindObjectOfType<DataSave>();
        filepath = Application.dataPath + "/" + "Save.txt";
        begin_btn = GameObject.Find("進入遊戲按鈕").GetComponent<Button>();

        Invoke("ActivateBtn", 4f);

        ClickBtn();
    }

    private void ClickBtn()           // 點擊按鈕
    {
        begin_btn.onClick.AddListener(ShowChoosePanel);
        build_player.onClick.AddListener(() => { StartCoroutine(BuildPlayer()); });
        load_player.onClick.AddListener(() => { StartCoroutine(LoadPlayer()); });
        still_create_Btn.onClick.AddListener(() => { StartCoroutine(StillCreate()); });
        create2_Btn.onClick.AddListener(() => { StartCoroutine(StillCreate()); });

        back_Btn.onClick.AddListener(Back);
        back2_Btn.onClick.AddListener(Back);
    }

    private void ActivateBtn()        // 激活按鈕
    {
        begin_btn.interactable = true;
    }

    private void ShowChoosePanel()    // 顯示選擇畫面
    {
        choose_Panel.SetActive(true);
    }

    private IEnumerator BuildPlayer() // 創建角色
    {
        if (File.Exists(filepath))
        {
            create_Panel.SetActive(true);
            still_create_Btn.interactable = false;
            still_create_Text.text = "3";
            yield return new WaitForSeconds(1f);
            still_create_Text.text = "2";
            yield return new WaitForSeconds(1f);
            still_create_Text.text = "1";
            yield return new WaitForSeconds(1f);
            still_create_Text.text = "";
            still_create_Btn.interactable = true;
        }
        else
        {
            ds.SaveData();
            SceneManager.LoadScene(1);
        }
        yield return null;
    }

    private IEnumerator LoadPlayer()  // 載入角色
    {
        if (File.Exists(filepath))
        {
            ds.LoadData();
            SceneManager.LoadScene(2);
        }
        else
        {
            load__Panel.SetActive(true);
        }
        yield return null;
    }

    private IEnumerator StillCreate() // 依舊創建
    {
        ds.SaveData();
        SceneManager.LoadScene(1);
        yield return null;
    }

    private void Back() // 回上一步
    {
        create_Panel.SetActive(false);
        load__Panel.SetActive(false);
    }
}
