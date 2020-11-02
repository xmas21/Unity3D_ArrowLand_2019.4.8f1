using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("購買攻擊力畫面")]
    public GameObject BuyPanelAttack;
    [Header("購買生命力畫面")]
    public GameObject BuyPanelHp;
    [Header("沒錢畫面")]
    public GameObject PanelNoMoney;
    [Header("生命等級")]
    public Text hpLevel;
    [Header("攻擊力等級")]
    public Text attackLevel;

    private PlayerDate playerDate;
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

    #region 

    private void Start()
    {
        #region 金幣鑽石
        coin1.text = data.PlayerCoin.ToString();
        coin2.text = data.PlayerCoin.ToString();
        coin3.text = data.PlayerCoin.ToString();
        coin4.text = data.PlayerCoin.ToString();
        jewel1.text = data.PlayerCoin.ToString();
        jewel2.text = data.PlayerCoin.ToString();
        jewel3.text = data.PlayerCoin.ToString();
        jewel4.text = data.PlayerCoin.ToString();
        #endregion

    }

    public void LoadLevel()
    {
        data.hp = data.hpMax;
        SceneManager.LoadScene("關卡1");
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
        a++;
        data.hpMax += 50;
        hpLevel.text = "lv. " + a;
        data.hp = data.hpMax;
        NoShowPanelHP();
    }

    /// <summary>
    /// 金幣買攻擊力
    /// </summary>
    public void BuyATK()
    {
        b++;
        attackLevel.text = "lv. " + b;
        data.attack += 10;
        NoShowPanelATK();
    }

    /// <summary>
    /// 金幣買生命畫面
    /// </summary>
    public void ShowPanelHP()
    {

        BuyPanelHp.SetActive(true);

        /*
        if (playerDate.PlayerCoin > 100)
        {
            BuyPanelHp.SetActive(true);
        }
        else
        {
            ShowPanelNoMoney();
        }
        */
    }

    /// <summary>
    /// 金幣購買攻擊力畫面
    /// </summary>
    public void ShowPanelATK()
    {

        BuyPanelAttack.SetActive(true);

        /*
        if (playerDate.PlayerCoin > 100)
        {

            BuyPanelAttack.SetActive(true);
        }
        else
        {
            ShowPanelNoMoney();
        }
        */
    }

    public void ShowPanelNoMoney()
    {
        PanelNoMoney.SetActive(true);
    }

    public void NoShowPanelHP()
    {
        BuyPanelHp.SetActive(false);
    }

    public void NoShowPanelATK()
    {
        BuyPanelAttack.SetActive(false);
    }

    public void NoShowPanelNoMoney()
    {
        PanelNoMoney.SetActive(false);
    }
    #endregion

}
