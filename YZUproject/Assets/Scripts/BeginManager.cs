using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class BeginManager : MonoBehaviour
{
    [Header("選擇畫面")]
    public GameObject choose_Panel;
    [Header("創建角色按鈕")]
    public Button build_player;
    [Header("載入角色按鈕")]
    public Button load_player;

    private string fileName = "Save.txt";

    private Button begin_btn;         // 最一開始的按鈕
    private DataSave ds;

    private void Start()
    {
        begin_btn = GameObject.Find("進入遊戲按鈕").GetComponent<Button>();

        Invoke("ActivateBtn", 4f);

        ClickBtn();
    }

    private void ClickBtn()           // 點擊按鈕
    {
        begin_btn.onClick.AddListener(ShowChoosePanel);
        build_player.onClick.AddListener(()=> { StartCoroutine(BuildPlayer()); });
        load_player.onClick.AddListener(() => { StartCoroutine(LoadPlayer()); });
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
        if (File.Exists(fileName))
        {
            print("1");
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        yield return null;
    }

    private IEnumerator LoadPlayer()  // 載入角色
    {
        print("Load");
        ds.LoadData();
        yield return null;
    }
}
