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
    [Header("抽武器寶箱畫面")]
    public GameObject weaponChest_Panel;
    [Header("寶箱按鈕_內")]
    public Button weaponChest_in;
    [Header("抽寶箱提示文字")]
    public Text chestText;
    [Header("寶箱圖片")]
    public GameObject weaponChest_Img;
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

    private Button weaponChest_Btn_out;       // 商城寶箱按鈕
    private Button lowButton1;                // 下方選單按鈕
    private Button lowButton2;
    private Button lowButton3;
    private Button lowButton4;
    private Animator ani;                      // 寶箱的動畫
    private MenuManager menu;
    private int index;                         // 武器抽前編號
    private int iindex;                        // 武器抽後編號

    private void Start()
    {
        menu = FindObjectOfType<MenuManager>();
        lowButton1 = GameObject.Find("商城下方按鈕").GetComponent<Button>();
        lowButton2 = GameObject.Find("裝備下方按鈕").GetComponent<Button>();
        lowButton3 = GameObject.Find("關卡下方按鈕").GetComponent<Button>();
        lowButton4 = GameObject.Find("天賦下方按鈕").GetComponent<Button>();
        weaponChest_Btn_out = GameObject.Find("抽寶箱按鈕外").GetComponent<Button>();
        ani = GameObject.Find("武器寶箱").GetComponent<Animator>();
        rewardSP = reward.GetComponent<Image>();

        costText.text = "$" + cost.ToString("F0");

        ClickBtn();
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
                weaponChest_in.interactable = false;
                yield return new WaitForSeconds(0.8f);
                chestText.text = "再次點擊寶箱";
                weaponChest_in.interactable = true;
            }
            else if (count[0] && !count[1]) // 第二階段 抽東西
            {
                chestText.text = "";
                ani.SetTrigger("點寶箱開啟");
                count[1] = true;
                weaponChest_in.interactable = false;
                yield return new WaitForSeconds(2f);
                weaponChest_Img.SetActive(false);
                rewardSP.sprite = allImg[iindex];
                yield return new WaitForSeconds(0.4f);
                reward.SetActive(true);
                chestText.text = "恭喜你抽到   " + data.ownWeapons[iindex].name;
                data.ownWeapons[iindex].owned = true;
                menu.weaponBtn[iindex].interactable = true;
                menu.weaponUse_Img[iindex].sprite = menu.weaponBtn_allimg[iindex];
                data.weapon_Count++;
                yield return new WaitForSeconds(1.5f);
                chestText.text = "點擊任意處離開";
                weaponChest_in.interactable = true;
            }
            else if (count[0] && count[1])  // 第三階段 關閉畫面
            {
                ani.SetTrigger("寶箱重製");
                data.PlayerCoin -= cost;
                menu.Updatedata();
                weaponChest_Img.SetActive(true);
                reward.SetActive(false);
                count[0] = false;
                count[1] = false;
                NoShowChestPanel();
                chestText.text = "點擊寶箱開啟";
            }
        }
        else if (data.ownWeapons[iindex].owned)  // 假如 已經有同一把武器 抽碎片
        {

            if (!count[0] && !count[1])    // 第一階段 寶箱晃動動畫 
            {
                chestText.text = "";
                ani.SetTrigger("點寶箱晃動");
                count[0] = true;
                weaponChest_in.interactable = false;
                yield return new WaitForSeconds(0.8f);
                chestText.text = "再次點擊寶箱";
                weaponChest_in.interactable = true;
            }
            else if (count[0] && !count[1]) // 第二階段 寶箱開啟動畫
            {
                chestText.text = "";
                ani.SetTrigger("點寶箱開啟");
                count[1] = true;
                weaponChest_in.interactable = false;
                yield return new WaitForSeconds(1.0f);
                weaponChest_Img.SetActive(false);
                rewardSP.sprite = allImg[iindex];
                yield return new WaitForSeconds(0.4f);
                reward.SetActive(true);
                chestText.text = "恭喜你抽到   " + data.ownWeapons[iindex].name;
                yield return new WaitForSeconds(1.0f);
                chestText.text = "由於您已擁有同樣武器";
                yield return new WaitForSeconds(1.0f);
                chestText.text = "已經自動把武器換成 10個碎片";
                data.weaponChips[iindex].count += 10;
                rewardSP.sprite = allImg[iindex + 9];
                yield return new WaitForSeconds(1.5f);
                chestText.text = "點擊任意處離開";
                weaponChest_in.interactable = true;
            }
            else if (count[0] && count[1])  // 第三階段 關閉畫面
            {
                ani.SetTrigger("寶箱重製");
                menu.Updatedata();
                data.PlayerCoin -= cost;
                weaponChest_Img.SetActive(true);
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

    private void ClickBtn()            // 按按鈕
    {
        weaponChest_Btn_out.onClick.AddListener(ShowChestPanel);
        weaponChest_in.onClick.AddListener(() => { StartCoroutine(ClickBtn_in()); });

        close1.onClick.AddListener(NoShowNoMoney);
        close2.onClick.AddListener(NoShowNoMoney);
    }

    private void ShowChestPanel()      // 顯示寶箱
    {
        if (data.PlayerCoin >= cost)
        {

            weaponChest_Panel.SetActive(true);
        }
        else if (data.PlayerCoin < cost)
        {
            ShowNoMoney();
        }
    }

    private void NoShowChestPanel()    // 關閉寶箱
    {
        weaponChest_Panel.SetActive(false);
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

