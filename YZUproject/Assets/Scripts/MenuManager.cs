using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
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

    [Header("空的寵物")]
    public GameObject pets_Empty;
    [Header("玩家寵物1"), Tooltip("寵物1")]
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

    // ************************************************ //
    [Header("武器區塊")]
    public GameObject weaponArea;
    [Header("寵物區塊")]
    public GameObject petArea;
    [Header("是否點了設定按鈕")]
    public bool isSet = false;

    [Header("設定畫面退出按鈕")]
    public Button[] setExit;
    [Header("設定畫面")]
    public GameObject[] setPanel;

    [Header("武器按鈕")]
    public Button[] weaponBtn;
    [Header("武器取消按鈕")]
    public Button[] weaponExit;
    [Header("武器畫面")]
    public GameObject[] weaponPanel;

    [Header("人員素材按鈕")]
    public Button[] soueceBtn;
    [Header("人員素材退出按鈕")]
    public Button[] soueceExit;
    [Header("人員素材畫面")]
    public GameObject[] soursePanel;

    [Header("天賦按鈕")]
    public Button[] talentBtn;
    [Header("天賦畫面")]
    public GameObject[] talentPanel;
    [Header("天賦數值")]
    public Text[] talentValue;

    private GameObject ps;     // 粒子

    private Button setButton1; // 設定按鈕
    private Button setButton2;
    private Button setButton3;
    private Button setButton4;
    private Button lowButton1; // 下方選單按鈕
    private Button lowButton2;
    private Button lowButton3;
    private Button lowButton4;
    private Button atkButton;  // 選擇武器的按鈕
    private Button petButton;  // 選擇寵物的按鈕

    private Player player;
    private DrawTalent draw;

    private Text atkValue;     // 裝備 攻擊力數值
    private Text hpValue;      // 裝備 生命數值
    private Text name;         // 玩家名稱

    private void Start()
    {
        setButton1 = GameObject.Find("設定按鈕0").GetComponent<Button>();
        setButton2 = GameObject.Find("設定按鈕1").GetComponent<Button>();
        setButton3 = GameObject.Find("設定按鈕2").GetComponent<Button>();
        setButton4 = GameObject.Find("設定按鈕3").GetComponent<Button>();
        lowButton1 = GameObject.Find("商城下方按鈕").GetComponent<Button>();
        lowButton2 = GameObject.Find("裝備下方按鈕").GetComponent<Button>();
        lowButton3 = GameObject.Find("關卡下方按鈕").GetComponent<Button>();
        lowButton4 = GameObject.Find("天賦下方按鈕").GetComponent<Button>();
        atkButton = GameObject.Find("武器按鈕").GetComponent<Button>();
        petButton = GameObject.Find("寵物按鈕").GetComponent<Button>();
        hpValue = GameObject.Find("生命值").GetComponent<Text>();
        atkValue = GameObject.Find("攻擊力").GetComponent<Text>();
        name = GameObject.Find("玩家名稱").GetComponent<Text>();
        ps = GameObject.Find("選單粒子效果");

        player = FindObjectOfType<Player>();
        draw = FindObjectOfType<DrawTalent>();

        Player.bullet = Weapon1;
        Player.pet1 = pets_Empty;

        isSet = false;

        Updatedata();
        Allowbtn();
        ClickButton();
    }

    private void Update()
    {
        if (isSet)
        {
            lowButton1.interactable = false;
            lowButton2.interactable = false;
            lowButton3.interactable = false;
            lowButton4.interactable = false;
        }
        else if (isSet == false)
        {
            lowButton1.interactable = true;
            lowButton2.interactable = true;
            lowButton3.interactable = true;
            lowButton4.interactable = true;
        }
    }


    /// <summary>
    /// 更新數值
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

        talentValue[0].text = "天賦生命值增加 + " + draw.hpValue;
        talentValue[1].text = "天賦攻擊力增加 + " + draw.atkValue;
        talentValue[2].text = "天賦爆擊增加 + " + draw.criticalValue;
        talentValue[3].text = "天賦跑速增加 + " + draw.speedValue;
        talentValue[4].text = "天賦減傷增加 + " + draw.armorValue;
        talentValue[5].text = "天賦回復力增加 + " + draw.rehpValue;

        float atk = data.attack + data.WeaponAttack + data.CriticalAttack;
        atkValue.text = atk.ToString();
        hpValue.text = data.hp.ToString("F0");
        name.text = data.name + "の裝備";

        Player.hp = player.data.hp;
        Player.hpMax = player.data.hpMax;
        Player.attack = player.data.attack;
        Player.attack_WP = player.data.WeaponAttack;
        Player.cd = player.data.cd;
        Player.criticalAttack = player.data.CriticalAttack;
        Player.speed = player.data.speed;
        Player.rehp = player.data.rehp;
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
    /// 點擊按鈕
    /// </summary>
    private void ClickButton()
    {
        lowButton1.onClick.AddListener(NoShowParticle);
        lowButton2.onClick.AddListener(NoShowParticle);
        lowButton4.onClick.AddListener(NoShowParticle);
        lowButton3.onClick.AddListener(ShowParticle);

        atkButton.onClick.AddListener(ShowWeaponArea);
        petButton.onClick.AddListener(ShowPetArea);

        setButton1.onClick.AddListener(() => { ShowSetPanel(0); });
        setButton2.onClick.AddListener(() => { ShowSetPanel(1); });
        setButton3.onClick.AddListener(() => { ShowSetPanel(2); });
        setButton4.onClick.AddListener(() => { ShowSetPanel(3); });

        for (int i = 0; i < setExit.Length; i++)
        {
            int index = i;
            setExit[index].onClick.AddListener(() => { NoShowSetPanel(index); });
        }

        for (int i = 0; i < weaponBtn.Length; i++)
        {
            int index = i;
            weaponBtn[index].onClick.AddListener(() => { ShowWeaponPanel(index); });
            weaponExit[index].onClick.AddListener(() => { NoShowWeaponPanel(index); });
        }

        for (int i = 0; i < soueceBtn.Length; i++)
        {
            int index = i;
            soueceBtn[index].onClick.AddListener(() => { ShowSourcePanel(index); });
            soueceExit[index].onClick.AddListener(() => { NoShowSourcePanel(index); });
        }

        for (int i = 0; i < talentBtn.Length; i++)
        {
            for (int j = 0; j < talentBtn.Length; j++)
            {
                int iindex = j;
                talentBtn[iindex].onClick.AddListener(() => { NoShowTalentPanel(0); });
                talentBtn[iindex].onClick.AddListener(() => { NoShowTalentPanel(1); });
                talentBtn[iindex].onClick.AddListener(() => { NoShowTalentPanel(2); });
                talentBtn[iindex].onClick.AddListener(() => { NoShowTalentPanel(3); });
                talentBtn[iindex].onClick.AddListener(() => { NoShowTalentPanel(4); });
                talentBtn[iindex].onClick.AddListener(() => { NoShowTalentPanel(5); });
            }
            int index = i;
            talentBtn[index].onClick.AddListener(() => { ShowTalentPanel(index); });
        }
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
    public void Level7()
    {
        ButtonOfLevel(7);
    }

    /// <summary>
    /// 關卡13按鈕
    /// </summary>
    public void Level13()
    {
        ButtonOfLevel(13);
    }

    /// <summary>
    /// 關卡19按鈕
    /// </summary>
    public void Level19()
    {
        ButtonOfLevel(19);
    }

    /// <summary>
    /// 關卡按鈕
    /// </summary>
    /// <param name="i">關卡號碼</param>
    private void ButtonOfLevel(int i)
    {
        SceneManager.LoadSceneAsync(i);
        chooseLevel.SetActive(false);
    }

    /*
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
    */

    /// <summary>
    /// 使用武器1
    /// </summary>
    public void UseWeapon1()
    {
        Player.bullet = Weapon1;
        data.WeaponAttack = 30;
    }

    /// <summary>
    /// 使用武器2
    /// </summary>
    public void UseWeapon2()
    {
        Player.bullet = Weapon2;
        data.WeaponAttack = 80;
    }

    /// <summary>
    /// 使用武器3
    /// </summary>
    public void UseWeapon3()
    {
        Player.bullet = Weapon3;
        data.WeaponAttack = 150;
    }

    /// <summary>
    /// 使用武器4
    /// </summary>
    public void UseWeapon4()
    {
        Player.bullet = Weapon4;
        data.WeaponAttack = 300;
    }

    /// <summary>
    /// 使用武器5
    /// </summary>
    public void UseWeapon5()
    {
        Player.bullet = Weapon5;
        data.WeaponAttack = 500;
    }

    /// <summary>
    /// 使用武器6
    /// </summary>
    public void UseWeapon6()
    {
        Player.bullet = Weapon6;
        data.WeaponAttack = 1000;
    }

    /// <summary>
    /// 使用寵物1
    /// </summary>
    public void UsePet1()
    {
        Player.pet1 = pets_1;
    }

    /// <summary>
    /// 使用寵物1
    /// </summary>
    public void UsePet2()
    {
        Player.pet1 = pets_2;
    }

    // ******************************************************************************************* //

    private void ShowWeaponArea() // 開啟武器區域畫面
    {
        petArea.SetActive(false);
        weaponArea.SetActive(true);
    }

    private void ShowPetArea() // 開啟寵物區域畫面
    {
        weaponArea.SetActive(false);
        petArea.SetActive(true);
    }

    private void ShowParticle() // 開啟粒子系統
    {
        ps.SetActive(true);
    }

    private void NoShowParticle() // 關閉粒子系統
    {
        ps.SetActive(false);
    }

    private void ShowSetPanel(int i) // 顯示設定畫面
    {
        isSet = true;
        setPanel[i].SetActive(true);
    }

    private void NoShowSetPanel(int i) // 關閉設定畫面
    {
        isSet = false;
        setPanel[i].SetActive(false);
    }

    private void ShowSourcePanel(int i) // 開啟人素材員畫面
    {
        soursePanel[i].SetActive(true);
    }

    private void NoShowSourcePanel(int i) // 關閉人員素材畫面
    {
        soursePanel[i].SetActive(false);
    }

    private void ShowWeaponPanel(int i) // 顯示武器畫面
    {
        weaponPanel[i].SetActive(true);
    }

    private void NoShowWeaponPanel(int i) // 不顯示武器畫面
    {
        weaponPanel[i].SetActive(false);
    }

    private void ShowTalentPanel(int i)  // 顯示天賦畫面
    {
        talentPanel[i].SetActive(true);
    }

    private void NoShowTalentPanel(int i) // 不顯示天賦畫面
    {
        talentPanel[i].SetActive(false);
    }
}
