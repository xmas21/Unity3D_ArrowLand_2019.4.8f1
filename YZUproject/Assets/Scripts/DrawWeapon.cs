using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrawWeapon : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("沒錢畫面")]
    public GameObject noMoney;
    [Header("關閉沒錢按鈕1")]
    public Button close1;
    [Header("關閉沒錢按鈕2")]
    public Button close2;
    [Header("武器寶箱")]
    public GameObject chestWeapon;
    [Header("抽寶箱畫面")]
    public GameObject chestPanel;
    [Header("寶箱按鈕_內")]
    public Button chestBtn_in;
    [Header("抽寶箱提示文字")]
    public Text chestText;
    [Header("寶箱圖片")]
    public GameObject chestImg;
    [Header("獎賞畫面")]
    public GameObject reward;
    [Header("獎賞畫面圖片")]
    private Image rewardSP;
    [Header("抽寶箱花費文字")]
    public Text costText;
    [Header("抽寶箱花費")]
    public int cost;

    [Header("寶箱階段")]
    public bool[] count;
    [Header("武器圖片庫")]
    public Sprite[] allImg;

    private Button chestBtn_out;       // 商城寶箱按鈕
    private Button lowButton1; // 下方選單按鈕
    private Button lowButton2;
    private Button lowButton3;
    private Button lowButton4;
    private Animator ani;              // 寶箱的動畫
    private MenuManager menu;
    private int index;                 // 武器抽前編號
    private int iindex;                // 武器抽後編號

    private void Start()
    {
        //************************************************//
        data.ownWeapons[0].owned = true;
        data.ownWeapons[1].owned = false;
        data.ownWeapons[2].owned = false;
        data.ownWeapons[3].owned = false;
        data.ownWeapons[4].owned = false;
        data.ownWeapons[5].owned = false;
        data.ownWeapons[6].owned = false;
        data.ownWeapons[7].owned = false;
        data.ownWeapons[8].owned = false;

        data.weaponChips[0].count = 0;
        data.weaponChips[1].count = 0;
        data.weaponChips[2].count = 0;
        data.weaponChips[3].count = 0;
        data.weaponChips[4].count = 0;
        data.weaponChips[5].count = 0;
        data.weaponChips[6].count = 0;
        data.weaponChips[7].count = 0;
        data.weaponChips[8].count = 0;

        menu = FindObjectOfType<MenuManager>();
        menu.weaponBtn[0].interactable = true;
        menu.weaponBtn[1].interactable = false;
        menu.weaponBtn[2].interactable = false;
        menu.weaponBtn[3].interactable = false;
        menu.weaponBtn[4].interactable = false;
        menu.weaponBtn[5].interactable = false;
        menu.weaponBtn[6].interactable = false;
        menu.weaponBtn[7].interactable = false;
        menu.weaponBtn[8].interactable = false;
        //************************************************//
        lowButton1 = GameObject.Find("商城下方按鈕").GetComponent<Button>();
        lowButton2 = GameObject.Find("裝備下方按鈕").GetComponent<Button>();
        lowButton3 = GameObject.Find("關卡下方按鈕").GetComponent<Button>();
        lowButton4 = GameObject.Find("天賦下方按鈕").GetComponent<Button>();
        chestBtn_out = GameObject.Find("抽寶箱按鈕外").GetComponent<Button>();
        ani = GameObject.Find("武器寶箱").GetComponent<Animator>();
        rewardSP = reward.GetComponent<Image>();

        costText.text = "$" + cost.ToString("F0");

        ClickBtn();
    }

    private void ClickBtn()            // 按按鈕
    {
        chestBtn_out.onClick.AddListener(ShowChestPanel);
        chestBtn_in.onClick.AddListener(() => { StartCoroutine(ClickBtn_in()); });

        close1.onClick.AddListener(NoShowNoMoney);
        close2.onClick.AddListener(NoShowNoMoney);
    }

    private IEnumerator ClickBtn_in()  // 點擊寶箱
    {
            RewardID();
            lowButton1.interactable = false;
            lowButton2.interactable = false;
            lowButton3.interactable = false;
            lowButton4.interactable = false;
            if (data.ownWeapons[iindex].owned == false)  // 假如 還沒有那一把武器 抽到武器
            {
                if (!count[0] && !count[1])    // 第一階段 寶箱開始晃動
                {
                    chestText.text = "";
                    ani.SetTrigger("點寶箱晃動");
                    count[0] = true;
                    chestBtn_in.interactable = false;
                    yield return new WaitForSeconds(0.6f);
                    chestText.text = "再次點擊寶箱";
                    chestBtn_in.interactable = true;
                }
                else if (count[0] && !count[1]) // 第二階段 抽東西
                {
                    chestText.text = "";
                    ani.SetTrigger("點寶箱開啟");
                    count[1] = true;
                    chestBtn_in.interactable = false;
                    yield return new WaitForSeconds(0.6f);
                    chestImg.SetActive(false);
                    rewardSP.sprite = allImg[iindex];
                    yield return new WaitForSeconds(0.4f);
                    reward.SetActive(true);
                    chestText.text = "恭喜你抽到   " + data.ownWeapons[iindex].name;
                    data.ownWeapons[iindex].owned = true;
                    menu.weaponBtn[iindex].interactable = true;
                    yield return new WaitForSeconds(1.5f);
                    chestText.text = "點擊任意處離開";
                    chestBtn_in.interactable = true;
                }
                else if (count[0] && count[1])  // 第三階段 關閉畫面
                {
                    ani.SetTrigger("寶箱重製");
                    data.PlayerCoin -= cost;
                    cost += 100;
                    menu.Updatedata();
                    chestImg.SetActive(true);
                    reward.SetActive(false);
                    count[0] = false;
                    count[1] = false;
                    NoShowChestPanel();
                    chestText.text = "點擊寶箱開啟";
                }
            }
            else if (data.ownWeapons[iindex].owned)  // 假如 已經有同一把武器 抽碎片
            {
                data.weaponChips[iindex].count += 10;
                if (!count[0] && !count[1])    // 第一階段 寶箱晃動動畫 
                {
                    chestText.text = "";
                    ani.SetTrigger("點寶箱晃動");
                    count[0] = true;
                    chestBtn_in.interactable = false;
                    yield return new WaitForSeconds(0.6f);
                    chestText.text = "再次點擊寶箱";
                    chestBtn_in.interactable = true;
                }
                else if (count[0] && !count[1]) // 第二階段 寶箱開啟動畫
                {
                    chestText.text = "";
                    ani.SetTrigger("點寶箱開啟");
                    count[1] = true;
                    chestBtn_in.interactable = false;
                    yield return new WaitForSeconds(0.6f);
                    chestImg.SetActive(false);
                    rewardSP.sprite = allImg[iindex];
                    yield return new WaitForSeconds(0.4f);
                    reward.SetActive(true);
                    chestText.text = "恭喜你抽到   " + data.ownWeapons[iindex].name;
                    yield return new WaitForSeconds(1.0f);
                    chestText.text = "由於您已擁有同樣武器";
                    yield return new WaitForSeconds(1.0f);
                    chestText.text = "已經自動把武器換成 10個碎片";
                    rewardSP.sprite = allImg[iindex + 9];
                    yield return new WaitForSeconds(1.5f);
                    chestText.text = "點擊任意處離開";
                    chestBtn_in.interactable = true;
                }
                else if (count[0] && count[1])  // 第三階段 關閉畫面
                {
                    ani.SetTrigger("寶箱重製");
                    menu.Updatedata();
                    data.PlayerCoin -= cost;
                    cost += 100;
                    chestImg.SetActive(true);
                    reward.SetActive(false);
                    count[0] = false;
                    count[1] = false;
                    NoShowChestPanel();
                    chestText.text = "點擊寶箱開啟";
                }
            }
            costText.text = "$" + cost.ToString("F0");
            lowButton1.interactable = true;
            lowButton2.interactable = true;
            lowButton3.interactable = true;
            lowButton4.interactable = true;
    }

    private void ShowChestPanel()      // 顯示寶箱
    {
        if (data.PlayerCoin >= cost)
        {

            chestPanel.SetActive(true);
        }
        else if (data.PlayerCoin < cost)
        {
            ShowNoMoney();
        }
    }

    private void NoShowChestPanel()    // 關閉寶箱
    {
        chestPanel.SetActive(false);
    }

    private void ShowNoMoney()         // 顯示沒錢畫面
    {
        noMoney.SetActive(true);
    }

    private void NoShowNoMoney()       // 關閉沒錢畫面
    {
        noMoney.SetActive(false);
    }

    public void RewardID()             // 決定編號
    {
        index = Random.Range(0, 100);
        if (index <= 1 && index >= 0)
        {
            index = 8; // 武器 ID 008
        }
        else if (index <= 5 && index >= 2)
        {
            index = 7; // 武器 ID 007
        }
        else if (index <= 9 && index >= 6)
        {
            index = 6;
        }
        else if (index <= 19 && index >= 10)
        {
            index = 5;
        }
        else if (index <= 29 && index >= 20)
        {
            index = 4;
        }
        else if (index <= 39 && index >= 30)
        {
            index = 3;
        }
        else if (index <= 57 && index >= 40)
        {
            index = 2;
        }
        else if (index <= 75 && index >= 58)
        {
            index = 1;
        }
        else if (index <= 100 && index >= 76)
        {
            index = 0;
        }
        iindex = index;
    }
}

