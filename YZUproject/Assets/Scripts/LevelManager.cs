using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    public GameObject objLight;
    public GameObject ramdomSkill;
    public Enemy[] enemys;

    [Header("是否顯示隨機技能")]
    public bool autoShowSkill;
    [Header("是否自動開門")]
    public bool autoOpenDoor;
    [Header("復活介面")]
    public GameObject panelRevival;
    [Header("主選單")]
    public GameObject mainMenu;

    [Header("古堡魔王一是否通關")]
    public static bool lv_9;
    [Header("古堡魔王二是否通關")]
    public static bool lv_15;
    [Header("希臘魔王一是否通關")]
    public static bool lv_21;
    [Header("希臘魔王二是否通關")]
    public static bool lv_27;

    public Player player;
    public Animator door;
    public Image imgCross;

    protected virtual void Start()
    {
        door = GameObject.Find("木頭門").GetComponent<Animator>();
        imgCross = GameObject.Find("轉場效果").GetComponent<Image>();

        player = FindObjectOfType<Player>();

        lv_9 = false;
        lv_15 = false;
        lv_21 = false;
        lv_27 = false;

        if (autoShowSkill) ShowSkill();
        if (autoOpenDoor) Invoke("Opendoor", 6);
    }

    protected virtual void IsPass() // 是否通關
    {
        enemys = FindObjectsOfType<Enemy>();

        if (enemys.Length == 0)
        {
            Pass();
            return;
        }
    }

    private void Update()
    {
        IsPass();
    }

    public IEnumerator NextLevel() // 下一關
    {
        AsyncOperation async;

        if (SceneManager.GetActiveScene().name.Contains("古堡魔王一"))
        {
            async = SceneManager.LoadSceneAsync(2);               // 切換場景到 主選單(關卡編號0)
            lv_9 = true;
            data.areas[0].stage = 5;
        }
        else if (SceneManager.GetActiveScene().name.Contains("古堡魔王二"))
        {
            async = SceneManager.LoadSceneAsync(2);
            lv_15= true;
            data.areas[0].stage = 10;
        }
        else if (SceneManager.GetActiveScene().name.Contains("希臘魔王一"))
        {
            async = SceneManager.LoadSceneAsync(2);
            lv_21 = true;
            data.areas[1].stage = 5;
        }
        else if (SceneManager.GetActiveScene().name.Contains("希臘魔王二"))
        {
            async = SceneManager.LoadSceneAsync(2);
            lv_27 = true;
            data.areas[1].stage = 10;
        }
        else
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            async = SceneManager.LoadSceneAsync(index + 1);        // 切換場景到下一關(關卡編號+1)
        }

        async.allowSceneActivation = false;

        for (int i = 0; i < 100; i++)                              // 過場動畫
        {
            imgCross.color += new Color(1, 1, 1, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }

        async.allowSceneActivation = true;
    }

    public IEnumerator ShowRevival() // 顯示復活
    {
        panelRevival.SetActive(true);

        for (int i = 5; i > 0; i--)
        {
            panelRevival.transform.GetChild(1).GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    public void CloseRevival() // 關閉復活
    {
        StopCoroutine(ShowRevival());
        panelRevival.SetActive(false);
    }
 
    public void Pass()// 過關
    {
        Opendoor();

        Item[] coins = FindObjectsOfType<Item>();

        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].pass = true;
        }
    }

    public void ShowMenu()// 選單按鈕
    {
        Time.timeScale = 0;
        mainMenu.SetActive(true);
    }

    public void MainMenu()// 回主選單
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void ResumeGame() // 繼續遊戲
    {
        mainMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowSkill() // 顯示抽技能
    {
        ramdomSkill.SetActive(true);
    }

    private void Opendoor() //開門
    {
        objLight.SetActive(true);
        door.SetTrigger("開門觸發");
    }

}
