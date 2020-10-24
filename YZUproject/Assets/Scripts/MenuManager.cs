using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("購買攻擊力畫面")]
    public GameObject BuyPanelAttack;
    [Header("購買生命力畫面")]
    public GameObject BuyPanelHp;
    #endregion

    #region 
    public void LoadLevel()
    {
        data.hp = data.hpMax;
        SceneManager.LoadScene("關卡1");
    }

    public void BuyHp()
    {
        data.hpMax += 500;
        data.hp = data.hpMax;
    }

    public void BuyHP()
    {
        data.hpMax += 50;
        data.hp = data.hpMax;
        NoShowPanelHP();
    }

    public void BuyAtk()
    {
        data.attack += 50;
    }

    public void BuyATK()
    {
        data.attack += 10;
        NoShowPanelATK();
    }

    public void ShowPanelHP()
    {
        BuyPanelHp.SetActive(true);
    }

    public void ShowPanelATK()
    {
        BuyPanelAttack.SetActive(true);
    }

    public void NoShowPanelHP()
    {
        BuyPanelHp.SetActive(false);
    }

    public void NoShowPanelATK()
    {
        BuyPanelAttack.SetActive(false);
    }
    #endregion

}
