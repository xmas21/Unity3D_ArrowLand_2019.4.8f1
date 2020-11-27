using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region 欄位
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("購買攻擊力畫面")]
    public GameObject BuyPanelAttack;
    [Header("購買生命力畫面")]
    public GameObject BuyPanelHp;
    [Header("沒錢畫面")]
    public GameObject PanelNoMoney;
    [Header("選擇關卡畫面")]
    public GameObject chooseLevel;
    [Header("生命等級")]
    public Text hpLevel;
    [Header("攻擊力等級")]
    public Text attackLevel;
    [Header("關卡6按鈕")]
    public Button btn6 ;
    [Header("關卡11按鈕")]
    public Button btn11 ;
    [Header("關卡16按鈕")]
    public Button btn16 ;


    public Text coin1;
    public Text coin2;
    public Text coin3;
    public Text coin4;
    public Text jewel1;
    public Text jewel2;
    public Text jewel3;
    public Text jewel4;

    int a = 1;
    int b = 1;
    #endregion


    private void Start()
    {
        data.hp = data.hpMax;
        Updatedata();
    }


    /// <summary>
    /// 切換場景
    /// </summary>
    public void LoadLevel()
    {
        data.hp = data.hpMax;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 鑽石買生命
    /// </summary>
    public void BuyHp()
    {
        data.hpMax += 500;
        data.hp = data.hpMax;
    }

    /// <summary>
    /// 鑽石買攻擊力
    /// </summary>
    public void BuyAtk()
    {
        data.attack += 50;
    }

    /// <summary>
    /// 金幣買生命
    /// </summary>
    public void BuyHP()
    {
        data.PlayerCoin -= 100;
        a++;
        data.hpMax += 50;
        hpLevel.text = "lv. " + a;
        data.hp = data.hpMax;
        NoShowPanelHP();
        Updatedata();
    }

    /// <summary>
    /// 金幣買攻擊力
    /// </summary>
    public void BuyATK()
    {
        data.PlayerCoin -= 100;
        b++;
        attackLevel.text = "lv. " + b;
        data.attack += 10;
        NoShowPanelATK();
        Updatedata();
    }

    /// <summary>
    /// 金幣買生命畫面
    /// </summary>
    public void ShowPanelHP()
    {

        if (data.PlayerCoin > 100)
        {
            BuyPanelHp.SetActive(true);
        }
        else
        {
            ShowPanelNoMoney();
        }

    }

    /// <summary>
    /// 金幣購買攻擊力畫面
    /// </summary>
    public void ShowPanelATK()
    {

        if (data.PlayerCoin > 100)
        {

            BuyPanelAttack.SetActive(true);
        }
        else
        {
            ShowPanelNoMoney();
        }

    }

    /// <summary>
    /// 沒錢畫面
    /// </summary>
    public void ShowPanelNoMoney()
    {
        PanelNoMoney.SetActive(true);
    }

    /// <summary>
    /// 關閉HP畫面
    /// </summary>
    public void NoShowPanelHP()
    {
        BuyPanelHp.SetActive(false);
    }

    /// <summary>
    /// 關閉攻擊力畫面
    /// </summary>
    public void NoShowPanelATK()
    {
        BuyPanelAttack.SetActive(false);
    }

    /// <summary>
    /// 關閉沒錢畫面
    /// </summary>
    public void NoShowPanelNoMoney()
    {
        PanelNoMoney.SetActive(false);
    }

    /// <summary>
    /// 更新資訊
    /// </summary>
    private void Updatedata()
    {
        coin1.text = data.PlayerCoin.ToString();
        coin2.text = data.PlayerCoin.ToString();
        coin3.text = data.PlayerCoin.ToString();
        coin4.text = data.PlayerCoin.ToString();
        jewel1.text = data.PlayerJewel.ToString();
        jewel2.text = data.PlayerJewel.ToString();
        jewel3.text = data.PlayerJewel.ToString();
        jewel4.text = data.PlayerJewel.ToString();
    }

    /// <summary>
    /// 選擇關卡
    /// </summary>
    public void LevelChoose()
    {
        chooseLevel.SetActive(true);
    }

    /// <summary>
    /// 關閉選擇關卡畫面
    /// </summary>
    public void NoShowLevelChoose()
    {
        chooseLevel.SetActive(false);
    }

    /// <summary>
    /// 關卡1按鈕
    /// </summary>
    public void Level1() 
    {
        ButtonOfLevel(1);
    }  

    /// <summary>
    /// 關卡7按鈕
    /// </summary>
    public void Level6() 
    {
        ButtonOfLevel(7);
    }  

    /// <summary>
    /// 關卡13按鈕
    /// </summary>
    public void Level11() 
    {
        ButtonOfLevel(13);
    }  

    /// <summary>
    /// 關卡19按鈕
    /// </summary>
    public void Level16() 
    {
        ButtonOfLevel(19);
    }

    /// <summary>
    /// 關卡按鈕
    /// </summary>
    /// <param name="i">關卡號碼</param>
    private void ButtonOfLevel(int i)
    {
        AsyncOperation async;
        async = SceneManager.LoadSceneAsync(i);
        chooseLevel.SetActive(false);
    }
}
