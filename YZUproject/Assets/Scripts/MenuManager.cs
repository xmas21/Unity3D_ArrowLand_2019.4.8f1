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
    public Button btn6;
    [Header("關卡11按鈕")]
    public Button btn11;
    [Header("關卡16按鈕")]
    public Button btn16;

    [Header("設定畫面1")]
    public GameObject setPanel1;
    [Header("設定畫面2")]
    public GameObject setPanel2;
    [Header("設定畫面3")]
    public GameObject setPanel3;
    [Header("設定畫面4")]
    public GameObject setPanel4;
    [Header("開發素材畫面1")]
    public GameObject soursePanel1;
    [Header("開發素材畫面2")]
    public GameObject soursePanel2;
    [Header("開發素材畫面3")]
    public GameObject soursePanel3;
    [Header("開發素材畫面4")]
    public GameObject soursePanel4;

    [Header("開發素材按鈕1")]
    public Button btnSource1;
    [Header("開發素材按鈕2")]
    public Button btnSource2;
    [Header("開發素材按鈕3")]
    public Button btnSource3;
    [Header("開發素材按鈕4")]
    public Button btnSource4;

    [Header("武器庫畫面")]
    public GameObject AllWeapon;
    [Header("武器1畫面")]
    public GameObject WeaponPanel_1;
    [Header("武器2畫面")]
    public GameObject WeaponPanel_2;
    [Header("武器3畫面")]
    public GameObject WeaponPanel_3;
    [Header("武器4畫面")]
    public GameObject WeaponPanel_4;
    [Header("武器5畫面")]
    public GameObject WeaponPanel_5;
    [Header("武器6畫面")]
    public GameObject WeaponPanel_6;
    [Header("沒錢買武器畫面")]
    public GameObject Nomoney;

    [Header("使用武器2按鈕")]
    public Button btnwpn2;
    [Header("使用武器3按鈕")]
    public Button btnwpn3;
    [Header("使用武器4按鈕")]
    public Button btnwpn4;
    [Header("使用武器5按鈕")]
    public Button btnwpn5;
    [Header("使用武器6按鈕")]
    public Button btnwpn6;

    [Header("購買武器2按鈕")]
    public Button buywpn2;
    [Header("購買武器3按鈕")]
    public Button buywpn3;
    [Header("購買武器4按鈕")]
    public Button buywpn4;
    [Header("購買武器5按鈕")]
    public Button buywpn5;
    [Header("購買武器6按鈕")]
    public Button buywpn6;

    [Header("玩家武器1")]
    public GameObject Weapon1;
    [Header("玩家武器2")]
    public GameObject Weapon2;
    [Header("玩家武器3")]
    public GameObject Weapon3;
    [Header("玩家武器4")]
    public GameObject Weapon4;
    [Header("玩家武器5")]
    public GameObject Weapon5;
    [Header("玩家武器6")]
    public GameObject Weapon6;

    [Header("寵物區塊畫面")]
    public GameObject pets;
    [Header("寵物1畫面")]
    public GameObject petPanel_1;
    [Header("寵物2畫面")]
    public GameObject petPanel_2;

    [Header("購買寵物1按鈕")]
    public Button buypet1;
    [Header("購買寵物2按鈕")]
    public Button buypet2;

    [Header("使用寵物1按鈕")]
    public Button btnpet1;
    [Header("使用寵物2按鈕")]
    public Button btnpet2;

    [Header("空的寵物")]
    public GameObject pets_Empty;
    [Header("玩家寵物1"),Tooltip("寵物1")]
    public GameObject pets_1;
    [Header("玩家寵物2"), Tooltip("寵物2")]
    public GameObject pets_2;

    public Text coin1;
    public Text coin2;
    public Text coin3;
    public Text coin4;
    public Text jewel1;
    public Text jewel2;
    public Text jewel3;
    public Text jewel4;

    private Button setButton1;
    private Button setButton2;
    private Button setButton3;
    private Button setButton4;

    int a = 1;
    int b = 1;
    #endregion

    private void Start()
    {
        setButton1 = GameObject.Find("設定按鈕1").GetComponent<Button>();
        setButton2 = GameObject.Find("設定按鈕2").GetComponent<Button>();
        setButton3 = GameObject.Find("設定按鈕3").GetComponent<Button>();
        setButton4 = GameObject.Find("設定按鈕4").GetComponent<Button>();

        data.hp = data.hpMax;
        data.WeaponAttack = 30;

        Player.bullet = Weapon1;
        Player.pet1 = pets_Empty;

        Updatedata();
        Allowbtn();
        SetButton();
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

    /// <summary>
    /// 激活按鈕
    /// </summary>
    private void Allowbtn()
    {
        if (LevelManager.bl_6 == true)
        {
            btn6.interactable = true;
        }
        if (LevelManager.bl_11 == true)
        {
            btn11.interactable = true;
        }
        if (LevelManager.bl_16 == true)
        {
            btn16.interactable = true;
        }
    }

    /// <summary>
    /// 開啟武器庫畫面
    /// </summary>
    public void ShowAllWeapon()
    {
        AllWeapon.SetActive(true);
    }

    /// <summary>
    /// 關閉武器庫畫面
    /// </summary>
    public void NoShowAllWeapon()
    {
        AllWeapon.SetActive(false);
    }

    /// <summary>
    /// 開啟武器1畫面
    /// </summary>
    public void ShowWeapon1()
    {
        WeaponPanel_1.SetActive(true);
    }

    /// <summary>
    /// 開啟武器2畫面
    /// </summary>
    public void ShowWeapon2()
    {
        WeaponPanel_2.SetActive(true);
    }

    /// <summary>
    /// 開啟武器3畫面
    /// </summary>
    public void ShowWeapon3()
    {
        WeaponPanel_3.SetActive(true);
    }

    /// <summary>
    /// 開啟武器4畫面
    /// </summary>
    public void ShowWeapon4()
    {
        WeaponPanel_4.SetActive(true);
    }

    /// <summary>
    /// 開啟武器5畫面
    /// </summary>
    public void ShowWeapon5()
    {
        WeaponPanel_5.SetActive(true);
    }

    /// <summary>
    /// 開啟武器6畫面
    /// </summary>
    public void ShowWeapon6()
    {
        WeaponPanel_6.SetActive(true);
    }

    /// <summary>
    /// 關閉武器1畫面
    /// </summary>
    public void NoShowWeapon1()
    {
        WeaponPanel_1.SetActive(false);
    }

    /// <summary>
    /// 關閉武器2畫面
    /// </summary>
    public void NoShowWeapon2()
    {
        WeaponPanel_2.SetActive(false);
    }

    /// <summary>
    /// 關閉武器3畫面
    /// </summary>
    public void NoShowWeapon3()
    {
        WeaponPanel_3.SetActive(false);
    }

    /// <summary>
    /// 關閉武器4畫面
    /// </summary>
    public void NoShowWeapon4()
    {
        WeaponPanel_4.SetActive(false);
    }

    /// <summary>
    /// 關閉武器5畫面
    /// </summary>
    public void NoShowWeapon5()
    {
        WeaponPanel_5.SetActive(false);
    }

    /// <summary>
    /// 關閉武器6畫面
    /// </summary>
    public void NoShowWeapon6()
    {
        WeaponPanel_6.SetActive(false);
    }

    /// <summary>
    /// 買武器2
    /// </summary>
    public void BuyWeapon2()
    {
        if (data.PlayerCoin > 500)
        {
            data.PlayerCoin -= 500;
            btnwpn2.interactable = true;
            buywpn2.interactable = false;
            Updatedata();
        }
        else
        {
            NoMoney();
        }
    }

    /// <summary>
    /// 買武器3
    /// </summary>
    public void BuyWeapon3()
    {
        if (data.PlayerCoin > 1000)
        {
            data.PlayerCoin -= 1000;
            btnwpn3.interactable = true;
            buywpn3.interactable = false;
            Updatedata();
        }
        else
        {
            NoMoney();
        }
    }

    /// <summary>
    /// 買武器4
    /// </summary>
    public void BuyWeapon4()
    {
        if (data.PlayerCoin > 1500)
        {
            data.PlayerCoin -= 1500;
            btnwpn4.interactable = true;
            buywpn4.interactable = false;
            Updatedata();
        }
        else
        {
            NoMoney();
        }
    }

    /// <summary>
    /// 買武器5
    /// </summary>
    public void BuyWeapon5()
    {
        if (data.PlayerCoin > 3000)
        {
            data.PlayerCoin -= 3000;
            btnwpn5.interactable = true;
            buywpn5.interactable = false;
            Updatedata();
        }
        else
        {
            NoMoney();
        }
    }

    /// <summary>
    /// 買武器6
    /// </summary>
    public void BuyWeapon6()
    {
        if (data.PlayerCoin > 6000)
        {
            data.PlayerCoin -= 5000;
            btnwpn6.interactable = true;
            buywpn6.interactable = false;
            Updatedata();
        }
        else
        {
            NoMoney();
        }
    }

    /// <summary>
    /// 沒錢買武器
    /// </summary>
    private void NoMoney()
    {
        Nomoney.SetActive(true);
    }

    /// <summary>
    /// 關閉沒錢買武器畫面
    /// </summary>
    public void NoShowNoMoney()
    {
        Nomoney.SetActive(false);
        NoShowWeapon1();
        NoShowWeapon2();
        NoShowWeapon3();
        NoShowWeapon4();
        NoShowWeapon5();
        NoShowWeapon6();
    }

    /// <summary>
    /// 使用武器1
    /// </summary>
    public void UseWeapon1()
    {
        Player.bullet = Weapon1;
        data.WeaponAttack = 30;
        WeaponPanel_1.SetActive(false);
        NoShowAllWeapon();
    }

    /// <summary>
    /// 使用武器2
    /// </summary>
    public void UseWeapon2()
    {
        Player.bullet = Weapon2;
        data.WeaponAttack = 80;
        WeaponPanel_2.SetActive(false);
        NoShowAllWeapon();
    }

    /// <summary>
    /// 使用武器3
    /// </summary>
    public void UseWeapon3()
    {
        Player.bullet = Weapon3;
        data.WeaponAttack = 150;
        WeaponPanel_3.SetActive(false);
        NoShowAllWeapon();
    }

    /// <summary>
    /// 使用武器4
    /// </summary>
    public void UseWeapon4()
    {
        Player.bullet = Weapon4;
        data.WeaponAttack = 300;
        WeaponPanel_4.SetActive(false);
        NoShowAllWeapon();
    }

    /// <summary>
    /// 使用武器5
    /// </summary>
    public void UseWeapon5()
    {
        Player.bullet = Weapon5;
        data.WeaponAttack = 500;
        WeaponPanel_5.SetActive(false);
        NoShowAllWeapon();
    }

    /// <summary>
    /// 使用武器6
    /// </summary>
    public void UseWeapon6()
    {
        Player.bullet = Weapon6;
        data.WeaponAttack = 1000;
        WeaponPanel_6.SetActive(false);
        NoShowAllWeapon();
    }

    /// <summary>
    /// 開啟全部寵物畫面
    /// </summary>
    public void ShowPets()
    {
        pets.SetActive(true);
    }

    /// <summary>
    /// 開啟寵物1資訊
    /// </summary>
    public void ShowPet1()
    {
        petPanel_1.SetActive(true);
    }

    /// <summary>
    /// 開啟寵物2資訊
    /// </summary>
    public void ShowPet2()
    {
        petPanel_2.SetActive(true);
    }

    /// <summary>
    /// 關閉全部寵物畫面
    /// </summary>
    public void NoShowPets()
    {
        pets.SetActive(false);
    }

    /// <summary>
    /// 關閉寵物1資訊
    /// </summary>
    public void NoShowPet1()
    {
        petPanel_1.SetActive(false);
    }

    /// <summary>
    /// 關閉寵物2資訊
    /// </summary>
    public void NoShowPet2()
    {
        petPanel_2.SetActive(false);
    }

    /// <summary>
    /// 買寵物1
    /// </summary>
    public void BuyPet1()
    {
        if (data.PlayerCoin > 50)
        {
            data.PlayerCoin -= 50;
            btnpet1.interactable = true;
            buypet1.interactable = false;
            Updatedata();
        }
        else
        {
            NoMoney();
        }
    }

    /// <summary>
    /// 買寵物2
    /// </summary>
    public void BuyPet2()
    {
        if (data.PlayerCoin > 100)
        {
            data.PlayerCoin -= 100;
            btnpet2.interactable = true;
            buypet2.interactable = false;
            Updatedata();
        }
        else
        {
            NoMoney();
        }
    }

    /// <summary>
    /// 使用寵物1
    /// </summary>
    public void UsePet1()
    {
        Player.pet1 = pets_1;
        NoShowPets();
    }

    /// <summary>
    /// 使用寵物1
    /// </summary>
    public void UsePet2()
    {
        Player.pet1 = pets_2;
        NoShowPets();
    }

    /// <summary>
    /// 點擊設定按鈕
    /// </summary>
    private void SetButton()
    {
        setButton1.onClick.AddListener(ShowSetPanel);
        setButton2.onClick.AddListener(ShowSetPanel);
        setButton3.onClick.AddListener(ShowSetPanel);
        setButton4.onClick.AddListener(ShowSetPanel);
    }

    /// <summary>
    /// 顯示設定畫面
    /// </summary>
    private void ShowSetPanel()
    {
        setPanel1.SetActive(true);
        setPanel2.SetActive(true);
        setPanel3.SetActive(true);
        setPanel4.SetActive(true);
    }

    public void NoShowSetPanel1()
    {
        setPanel1.SetActive(false);
        setPanel2.SetActive(false);
        setPanel3.SetActive(false);
        setPanel4.SetActive(false);
    }

    public void NoShowSetPanel2()
    {
        setPanel1.SetActive(false);
        setPanel2.SetActive(false);
        setPanel3.SetActive(false);
        setPanel4.SetActive(false);
    }

    public void NoShowSetPanel3()
    {
        setPanel1.SetActive(false);
        setPanel2.SetActive(false);
        setPanel3.SetActive(false);
        setPanel4.SetActive(false);
    }

    public void NoShowSetPanel4()
    {
        setPanel1.SetActive(false);
        setPanel2.SetActive(false);
        setPanel3.SetActive(false);
        setPanel4.SetActive(false);
    }

    public void NoShowSourcePanel1()
    {
        soursePanel1.SetActive(false);
    }

    public void NoShowSourcePanel2()
    {
        soursePanel2.SetActive(false);
    }

    public void NoShowSourcePanel3()
    {
        soursePanel3.SetActive(false);
    }

    public void NoShowSourcePanel4()
    {
        soursePanel4.SetActive(false);
    }

    public void BtnSource1()
    {
        soursePanel1.SetActive(true);
    }

    public void BtnSource2()
    {
        soursePanel2.SetActive(true);
    }

    public void BtnSource3()
    {
        soursePanel3.SetActive(true);
    }

    public void BtnSource4()
    {
        soursePanel4.SetActive(true);
    }

}
