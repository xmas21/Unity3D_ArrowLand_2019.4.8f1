using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrawPet : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("沒錢畫面")]
    public GameObject noMoney;
    [Header("關閉沒錢按鈕1")]
    public Button close1;
    [Header("關閉沒錢按鈕2")]
    public Button close2;
    [Header("抽寵物寶箱畫面")]
    public GameObject chest_Panel;
    [Header("寶箱按鈕_內")]
    public Button chest_Btn_in;
    [Header("抽寶箱提示文字")]
    public Text chestText;
    [Header("寶箱圖片")]
    public GameObject chest_Img;
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

    private Button chest_Btn_Out;             // 商城寶箱按鈕
    private Button lowButton1;                // 下方選單按鈕
    private Button lowButton2;
    private Button lowButton3;
    private Button lowButton4;
    private Animator ani;                      // 寶箱的動畫
    private MenuManager menu;
    private int index;                         // 寵物抽前編號
    private int iindex;                        // 寵物抽後編號

    private void Start()
    {
        data.ownPets[0].owned = false;
        data.ownPets[1].owned = false;

        menu = FindObjectOfType<MenuManager>();
        lowButton1 = GameObject.Find("商城下方按鈕").GetComponent<Button>();
        lowButton2 = GameObject.Find("裝備下方按鈕").GetComponent<Button>();
        lowButton3 = GameObject.Find("關卡下方按鈕").GetComponent<Button>();
        lowButton4 = GameObject.Find("天賦下方按鈕").GetComponent<Button>();
        chest_Btn_Out = GameObject.Find("抽寵物寶箱按鈕外").GetComponent<Button>();
        ani = GameObject.Find("寵物寶箱").GetComponent<Animator>();
        rewardSP = reward.GetComponent<Image>();

        costText.text = "$" + cost.ToString("F0");

        ClickBtn();
    }

    private void ClickBtn()            // 按按鈕
    {
        chest_Btn_Out.onClick.AddListener(ShowChestPanel);
        chest_Btn_in.onClick.AddListener(() => { StartCoroutine(ClickBtn_in()); });

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
        if (data.ownPets[iindex].owned == false)  // 假如 還沒有那一把武器 抽到武器
        {
            if (!count[0] && !count[1])    // 第一階段 寶箱開始晃動
            {
                chestText.text = "";
                ani.SetTrigger("點寶箱晃動");
                count[0] = true;
                chest_Btn_in.interactable = false;
                yield return new WaitForSeconds(0.8f);
                chestText.text = "再次點擊寶箱";
                chest_Btn_in.interactable = true;
            }
            else if (count[0] && !count[1]) // 第二階段 抽東西
            {
                chestText.text = "";
                ani.SetTrigger("點寶箱開啟");
                count[1] = true;
                chest_Btn_in.interactable = false;
                yield return new WaitForSeconds(2f);
                chest_Img.SetActive(false);
                rewardSP.sprite = allImg[iindex];
                yield return new WaitForSeconds(0.4f);
                reward.SetActive(true);
                chestText.text = "恭喜你抽到   " + data.ownPets[iindex].name;
                data.ownPets[iindex].owned = true;
                menu.petBtn[iindex].interactable = true;
                menu.petUse_img[iindex].sprite = menu.petBtn_allimg[iindex];
                yield return new WaitForSeconds(1.5f);
                chestText.text = "點擊任意處離開";
                chest_Btn_in.interactable = true;
            }
            else if (count[0] && count[1])  // 第三階段 關閉畫面
            {
                ani.SetTrigger("寶箱重製");
                data.PlayerCoin -= cost;
                menu.Updatedata();
                chest_Img.SetActive(true);
                reward.SetActive(false);
                count[0] = false;
                count[1] = false;
                NoShowChestPanel();
                chestText.text = "點擊寶箱開啟";
            }
        }
        else if (data.ownPets[iindex].owned)  // 假如 已經有同一把武器 抽碎片
        {
            if (!count[0] && !count[1])    // 第一階段 寶箱晃動動畫 
            {
                chestText.text = "";
                ani.SetTrigger("點寶箱晃動");
                count[0] = true;
                chest_Btn_in.interactable = false;
                yield return new WaitForSeconds(0.8f);
                chestText.text = "再次點擊寶箱";
                chest_Btn_in.interactable = true;
            }
            else if (count[0] && !count[1]) // 第二階段 寶箱開啟動畫
            {
                chestText.text = "";
                ani.SetTrigger("點寶箱開啟");
                count[1] = true;
                chest_Btn_in.interactable = false;
                yield return new WaitForSeconds(1.0f);
                chest_Img.SetActive(false);
                rewardSP.sprite = allImg[iindex];
                yield return new WaitForSeconds(0.4f);
                reward.SetActive(true);
                chestText.text = "恭喜你抽到   " + data.ownPets[iindex].name;
                yield return new WaitForSeconds(1.0f);
                chestText.text = "由於您已擁有同樣寵物";
                yield return new WaitForSeconds(1.0f);
                chestText.text = "已經自動把寵物換成 5個碎片";
                data.petChips[iindex].count += 5;
                rewardSP.sprite = allImg[iindex + 2];
                yield return new WaitForSeconds(1.5f);
                chestText.text = "點擊任意處離開";
                chest_Btn_in.interactable = true;
            }
            else if (count[0] && count[1])  // 第三階段 關閉畫面
            {
                ani.SetTrigger("寶箱重製");
                menu.Updatedata();
                data.PlayerCoin -= cost;
                chest_Img.SetActive(true);
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
            chest_Panel.SetActive(true);
        }
        else if (data.PlayerCoin < cost)
        {
            ShowNoMoney();
        }
    }

    private void NoShowChestPanel()    // 關閉寶箱
    {
        chest_Panel.SetActive(false);
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

        if (index <= 30 && index >= 0)
        {
            index = 1; // 
        }
        else if (index <= 100 && index >= 31)
        {
            index = 0; // 
        }

        iindex = index;
    }
}

