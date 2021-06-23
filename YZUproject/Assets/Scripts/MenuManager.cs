using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("選擇層數畫面")]
    public GameObject chooseLevel_Panel;
    [Header("關閉層數畫面按鈕")]
    public Button chooseLevel_Exit;
    [Header("階段一按鈕")]
    public Button stage1_Btn;
    [Header("階段二按鈕")]
    public Button stage2_Btn;
    [Header("詳細資訊畫面")]
    public GameObject valueDetail;
    [Header("武器區塊")]
    public GameObject weaponArea;
    [Header("寵物區塊")]
    public GameObject petArea;

    [Header("玩家數值")]
    public Text[] playerValue;
    [Header("玩家武器")]
    public GameObject[] weapon;
    [Header("玩家寵物")]
    public GameObject[] pet;

    [Header("金幣群組")]
    public Text[] coin;
    [Header("鑽石群組")]
    public Text[] jewel;

    [Header("設定畫面退出按鈕")]
    public Button[] setExit;
    [Header("設定畫面")]
    public GameObject[] setPanel;

    [Header("武器按鈕")]
    public Button[] weaponBtn;
    [Header("武器按鈕全部照片")]
    public Sprite[] weaponBtn_allimg;
    [Header("武器取消按鈕")]
    public Button[] weaponExit;
    [Header("武器畫面")]
    public GameObject[] weaponPanel;

    [Header("武器使用按鈕")]
    public Button[] weaponUse_Btn;
    [Header("武器使用全部照片")]
    public Sprite[] weaponUse_allimg;
    [Header("武器使用中圖片")]
    public Image[] weaponUse_Img;

    [Header("武器等級文字")]
    public Text[] weaponLevel_text;
    [Header("武器傷害文字")]
    public Text[] weaponDamage_text;
    [Header("武器升級按鈕")]
    public Button[] weaponUp_Btn;
    [Header("武器升級條")]
    public Image[] weaponUp_Bar;
    [Header("武器碎片文字")]
    public Text[] weaponChip_Text;

    [Header("寵物按鈕")]
    public Button[] petBtn;
    [Header("寵物按鈕全部圖片")]
    public Sprite[] petBtn_allimg;
    [Header("寵物取消按鈕")]
    public Button[] petExit;
    [Header("寵物畫面")]
    public GameObject[] petPanel;

    [Header("寵物使用按鈕")]
    public Button[] petUse_Btn;
    [Header("寵物使用中全部照片")]
    public Sprite[] petUse_allimg;
    [Header("寵物使用中圖片")]
    public Image[] petUse_img;

    [Header("寵物等級文字")]
    public Text[] petLevel_text;
    [Header("寵物傷害文字")]
    public Text[] petDamage_text;
    [Header("寵物升級按鈕")]
    public Button[] petUp_Btn;
    [Header("寵物升級條")]
    public Image[] petU_Bar;
    [Header("寵物碎片文字")]
    public Text[] petChip_Text;

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

    [Header("成就獎盃")]
    public Image[] trophy;
    [Header("成就領獎按鈕")]
    public Button[] trophy_reward_Btn;
    [Header("成就畫面")]
    public GameObject trophy_Panel;
    [Header("成就獎盃照片")]
    public Sprite trophy_Img;
    [Header("成就提示")]
    public GameObject red_dot;
    [Header("關閉成就按鈕")]
    public Button trophy_Exit;

    [Header("全部地區圖片")]
    public Sprite[] allArea_img;
    [Header("地區畫面")]
    public GameObject area_Panel;
    [Header("關閉地區按鈕")]
    public Button area_Exit;
    [Header("地區按鈕")]
    public Button[] area_Btn;
    [Header("進入地區按鈕")]
    public Button enteaArea_Btn;
    [Header("關卡進度文字")]
    public Text schedule_Text;

    private Button valueDetailBtn;  // 詳細資訊按鈕
    private Button setButton1;      // 設定按鈕
    private Button setButton2;
    private Button setButton3;
    private Button setButton4;
    private Button lowButton1;      // 下方選單按鈕
    private Button lowButton2;
    private Button lowButton3;
    private Button lowButton4;
    private Button atkButton;       // 選擇武器的按鈕
    private Button petButton;       // 選擇寵物的按鈕
    private Button ifinite_Btn;     // 噩夢遠征按鈕
    private Button achieve_Btn;     // 成就按鈕
    private Button chooseArea_Btn;  // 選地區按鈕
    private Button chooseLevel_Btn; // 選層數按鈕

    private GameObject ps;          // 粒子
    private Player player;
    private DrawTalent draw;

    private Image atkBtn_img;       // 選擇武器的按鈕的圖片
    private Image petBtn_img;       // 選擇寵物的按鈕的圖片
    private Image level_img;        // 關卡圖片
    private Text level_Text;        // 關卡名稱
    private Text atkValue;          // 裝備 攻擊力數值
    private Text hpValue;           // 裝備 生命數值
    private Text player_name;       // 玩家名稱

    private int area_Num;           // 地區編號
    private bool isDetail;

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
        valueDetailBtn = GameObject.Find("詳細資訊按鈕").GetComponent<Button>();
        ifinite_Btn = GameObject.Find("噩夢遠征按鈕").GetComponent<Button>();
        achieve_Btn = GameObject.Find("成就系統按鈕").GetComponent<Button>();
        chooseArea_Btn = GameObject.Find("選地區按鈕").GetComponent<Button>();
        chooseLevel_Btn = GameObject.Find("層數按鈕").GetComponent<Button>();
        level_img = GameObject.Find("關卡圖片").GetComponent<Image>();
        level_Text = GameObject.Find("關卡名稱").GetComponent<Text>();
        hpValue = GameObject.Find("生命值").GetComponent<Text>();
        atkValue = GameObject.Find("攻擊力").GetComponent<Text>();
        player_name = GameObject.Find("玩家名稱").GetComponent<Text>();
        schedule_Text = GameObject.Find("關卡進度").GetComponent<Text>();
        ps = GameObject.Find("選單粒子效果");
        atkBtn_img = atkButton.GetComponent<Image>();
        petBtn_img = petButton.GetComponent<Image>();

        player = FindObjectOfType<Player>();
        draw = FindObjectOfType<DrawTalent>();

        isDetail = false;
        area_Num = 0;

        Updatedata();
        Allowbtn();
        ClickButton();
    }

    private void Update()
    {
        Achievement();
        StageCount();
    }

    public void Updatedata() //更新數值
    {
        for (int i = 0; i < coin.Length; i++)  // 金幣更新
        {
            int index = i;
            coin[index].text = data.PlayerCoin.ToString("F0");
        }

        for (int i = 0; i < jewel.Length; i++)  // 鑽石更新
        {
            int index = i;
            jewel[index].text = data.PlayerJewel.ToString("F0");
        }

        for (int i = 0; i < weaponBtn.Length; i++)  // 裝備 武器照片抓資料
        {
            int index = i;
            weaponUse_Img[index] = weaponBtn[index].GetComponent<Image>();
        }

        for (int i = 0; i < petBtn.Length; i++)
        {
            int index = i;
            petUse_img[index] = petBtn[index].GetComponent<Image>();
        }

        for (int i = 0; i < weaponChip_Text.Length; i++) // 裝備 武器碎片數量更新
        {
            int index = i;
            weaponChip_Text[index].text = data.weaponChips[index].count + "/ 30";
        }

        for (int i = 0; i < petChip_Text.Length; i++)    // 裝備 武器碎片數量更新
        {
            int index = i;
            petChip_Text[index].text = data.petChips[index].count + "/ 30";
        }

        for (int i = 0; i < weaponUp_Bar.Length; i++)    // 裝備 武器 升級條更新
        {
            int index = i;
            weaponUp_Bar[index].fillAmount = (float)data.weaponChips[index].count / 30;
        }

        for (int i = 0; i < petU_Bar.Length; i++)    // 裝備 武器 升級條更新
        {
            int index = i;
            petU_Bar[index].fillAmount = (float)data.petChips[index].count / 30;
        }

        for (int i = 0; i < weaponUp_Btn.Length; i++)    // 裝備 武器 升級按鈕控制
        {
            int index = i;
            if (data.weaponChips[index].count >= 10)
            {
                weaponUp_Btn[index].interactable = true;
            }
            else if (data.weaponChips[index].count < 10)
            {
                weaponUp_Btn[index].interactable = false;
            }
        }

        for (int i = 0; i < petUp_Btn.Length; i++)    // 裝備 武器 升級按鈕控制
        {
            int index = i;
            if (data.petChips[index].count >= 10)
            {
                petUp_Btn[index].interactable = true;
            }
            else if (data.petChips[index].count < 10)
            {
                petUp_Btn[index].interactable = false;
            }
        }

        for (int i = 0; i < weaponLevel_text.Length; i++)  // 裝備 武器 等級文字更新
        {
            int index = i;
            weaponLevel_text[index].text = "Lv." + data.ownWeapons[index].level;
        }

        for (int i = 0; i < petLevel_text.Length; i++)  // 裝備 武器 等級文字更新
        {
            int index = i;
            petLevel_text[index].text = "Lv." + data.ownPets[index].level;
        }

        for (int i = 0; i < weaponDamage_text.Length; i++)
        {
            int index = i;
            weaponDamage_text[index].text = "傷害 : " + data.ownWeapons[i].damage;
        }

        for (int i = 0; i < petDamage_text.Length; i++)
        {
            int index = i;
            petDamage_text[index].text = "傷害 : " + data.ownPets[i].damage;
        }

        for (int i = 0; i < weaponBtn.Length; i++)
        {
            int index = i;
            if (data.ownWeapons[index].owned == true)
            {
                weaponUse_Img[index].sprite = weaponBtn_allimg[index];
            }
        }

        talentValue[0].text = "天賦生命增加 + " + draw.hpValue.ToString("F0");
        talentValue[1].text = "天賦攻擊增加 + " + draw.atkValue.ToString("F0");
        talentValue[2].text = "天賦爆擊增加 + " + draw.criticalValue.ToString("F0");
        talentValue[3].text = "天賦跑速增加 + " + draw.speedValue.ToString("F0");
        talentValue[4].text = "天賦減傷增加 + " + draw.armorValue.ToString("F0");
        talentValue[5].text = "天賦回復增加 + " + draw.rehpValue.ToString("F0");

        float atk = data.attack + data.WeaponAttack + data.CriticalAttack;
        atkValue.text = atk.ToString();
        hpValue.text = data.hp.ToString("F0");
        player_name.text = data.player_name + "の裝備";

        Player.hp = player.data.hp;
        Player.hpMax = player.data.hpMax;
        Player.attack = player.data.attack;
        Player.attack_WP = player.data.WeaponAttack;
        Player.cd = player.data.cd;
        Player.criticalAttack = player.data.CriticalAttack;
        Player.speed = player.data.speed;
        Player.rehp = player.data.rehp;
    }

    public void SetNum0() // 編號0
    {
        area_Num = 0;
    }

    public void SetNum1() // 編號1
    {
        area_Num = 1;
    }

    public void SetNum2() // 編號2
    {
        area_Num = 2;
    }

    public void ChangeScene() // 切換場景1
    {
        player.revived = false;
        if (area_Num == 0)
        {
            LoadLevel(4);
        }
        else if (area_Num == 1)
        {
            LoadLevel(16);
        }
        else if (area_Num == 2)
        {
            LoadLevel(28);
        }
    }

    public void LoadScene() // 切換場景2
    {
        player.revived = false;
        if (area_Num == 0)
        {
            LoadLevel(10);
        }
        else if (area_Num == 1)
        {
            LoadLevel(22);
        }
        else if (area_Num == 2)
        {
            LoadLevel(28);
        }
    }

    private void Allowbtn() // 激活按鈕
    {
        if (LevelManager.lv_9 == true)
        {
            stage2_Btn.interactable = true;
        }
        if (LevelManager.lv_15 == true)
        {
            area_Btn[1].interactable = true;
        }
        if (LevelManager.lv_21 == true)
        {
            stage2_Btn.interactable = true;
        }
        if (LevelManager.lv_27 == true)
        {
            area_Btn[2].interactable = true;
        }
    }

    private void ClickButton() // 點擊按鈕
    {
        lowButton1.onClick.AddListener(NoShowParticle);
        lowButton2.onClick.AddListener(NoShowParticle);
        lowButton4.onClick.AddListener(NoShowParticle);
        lowButton3.onClick.AddListener(ShowParticle);

        atkButton.onClick.AddListener(ShowWeaponArea);
        petButton.onClick.AddListener(ShowPetArea);

        ifinite_Btn.onClick.AddListener(LoadIfinite);
        achieve_Btn.onClick.AddListener(ShowAchieve);
        trophy_Exit.onClick.AddListener(NoShowAchieve);
        chooseArea_Btn.onClick.AddListener(ShowAreaPanel);
        area_Exit.onClick.AddListener(NoShowAreaPanel);
        enteaArea_Btn.onClick.AddListener(EnterArea);
        chooseLevel_Btn.onClick.AddListener(ShowChooseLevel);
        chooseLevel_Exit.onClick.AddListener(NoShowChooseLevel);

        setButton1.onClick.AddListener(() => { ShowSetPanel(0); });
        setButton2.onClick.AddListener(() => { ShowSetPanel(1); });
        setButton3.onClick.AddListener(() => { ShowSetPanel(2); });
        setButton4.onClick.AddListener(() => { ShowSetPanel(3); });

        trophy_reward_Btn[0].onClick.AddListener(() => { TrophyReward(300, 0); });
        trophy_reward_Btn[1].onClick.AddListener(() => { TrophyReward(500, 1); });
        trophy_reward_Btn[2].onClick.AddListener(() => { TrophyReward(800, 2); });
        trophy_reward_Btn[3].onClick.AddListener(() => { TrophyReward(1000, 3); });
        trophy_reward_Btn[4].onClick.AddListener(() => { TrophyReward(500, 4); });
        trophy_reward_Btn[5].onClick.AddListener(() => { TrophyReward(1000, 5); });
        trophy_reward_Btn[6].onClick.AddListener(() => { TrophyReward(1500, 6); });
        trophy_reward_Btn[7].onClick.AddListener(() => { TrophyReward(2000, 6); });

        valueDetailBtn.onClick.AddListener(ShowValueDetail);

        for (int i = 0; i < setExit.Length; i++)  // 退出設定按鈕
        {
            int index = i;
            setExit[index].onClick.AddListener(() => { NoShowSetPanel(index); });
        }

        for (int i = 0; i < weaponBtn.Length; i++)  // 武器按鈕
        {
            int index = i;
            weaponBtn[index].onClick.AddListener(() => { ShowWeaponPanel(index); });
            weaponExit[index].onClick.AddListener(() => { NoShowWeaponPanel(index); });
            weaponUse_Btn[index].onClick.AddListener(() => { UseWeapon(index); });
            weaponUp_Btn[index].onClick.AddListener(() => { WeaponLevelUp(index); });
        }

        for (int i = 0; i < petBtn.Length; i++)  // 寵物按鈕
        {
            int index = i;
            petBtn[index].onClick.AddListener(() => { ShowPetPanel(index); });
            petExit[index].onClick.AddListener(() => { NoShowPetPanel(index); });
            petUse_Btn[index].onClick.AddListener(() => { UsePet(index); });
            petUp_Btn[index].onClick.AddListener(() => { PetLevelUp(index); });
        }

        for (int i = 0; i < soueceBtn.Length; i++)  // 人員素材按鈕
        {
            int index = i;
            soueceBtn[index].onClick.AddListener(() => { ShowSourcePanel(index); });
            soueceExit[index].onClick.AddListener(() => { NoShowSourcePanel(index); });
        }

        for (int i = 0; i < talentBtn.Length; i++)  // 天賦按鈕
        {
            int index = i;
            talentBtn[index].onClick.AddListener(() => { ShowTalentPanel(index); });
        }

        for (int i = 0; i < area_Btn.Length; i++)
        {
            int index = i;
            area_Btn[index].onClick.AddListener(ActivateEnterArea_Btn);
        }
    }

    private void LoadLevel(int level) // 切換場景 - 劇情
    {
        SceneManager.LoadScene(level);
    }

    private void LoadIfinite() // 噩夢遠征關卡
    {
        SceneManager.LoadScene(3);
    }

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
        setPanel[i].SetActive(true);
        lowButton1.interactable = false;
        lowButton2.interactable = false;
        lowButton3.interactable = false;
        lowButton4.interactable = false;
    }

    private void NoShowSetPanel(int i) // 關閉設定畫面
    {
        setPanel[i].SetActive(false);
        lowButton1.interactable = true;
        lowButton2.interactable = true;
        lowButton3.interactable = true;
        lowButton4.interactable = true;
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
        lowButton1.interactable = false;
        lowButton2.interactable = false;
        lowButton3.interactable = false;
        lowButton4.interactable = false;
    }

    private void NoShowWeaponPanel(int i) // 不顯示武器畫面
    {
        weaponPanel[i].SetActive(false);
        lowButton1.interactable = true;
        lowButton2.interactable = true;
        lowButton3.interactable = true;
        lowButton4.interactable = true;
    }

    private void ShowPetPanel(int i) // 顯示武器畫面
    {
        petPanel[i].SetActive(true);
        lowButton1.interactable = false;
        lowButton2.interactable = false;
        lowButton3.interactable = false;
        lowButton4.interactable = false;
    }

    private void NoShowPetPanel(int i) // 不顯示武器畫面
    {
        petPanel[i].SetActive(false);
        lowButton1.interactable = true;
        lowButton2.interactable = true;
        lowButton3.interactable = true;
        lowButton4.interactable = true;
    }

    private void ShowTalentPanel(int i)  // 顯示天賦畫面
    {
        for (int j = 0; j < talentPanel.Length; j++)
        {
            talentPanel[j].SetActive(false);
        }
        talentPanel[i].SetActive(true);
    }

    private void ShowValueDetail()  // 開關詳細資訊
    {
        playerValue[0].text = data.hp.ToString("F0");
        float PV_atk = data.attack + data.CriticalAttack + data.WeaponAttack;
        playerValue[1].text = PV_atk.ToString("F0");
        playerValue[2].text = data.speed.ToString("F0");
        playerValue[3].text = data.armor.ToString("F2") + " %";
        playerValue[4].text = data.rehp.ToString("F2") + " /s";
        playerValue[5].text = data.cd.ToString("F1") + " /s";

        isDetail = !isDetail;
        if (isDetail)
        {
            valueDetail.SetActive(true);
        }
        else
        {
            valueDetail.SetActive(false);
        }
    }

    private void UseWeapon(int i)  // 選擇武器
    {
        for (int j = 0; j < weaponUp_Btn.Length; j++)
        {
            if (data.ownWeapons[j].owned == true)
            {
                weaponUse_Img[j].sprite = weaponBtn_allimg[j];
            }
        }
        atkBtn_img.sprite = weaponBtn_allimg[i];
        weaponUse_Img[i].sprite = weaponUse_allimg[i];
        Player.bullet = weapon[i];
        Player.property = data.ownWeapons[i].attributes.ToString();
        data.WeaponAttack = data.ownWeapons[i].damage;
        data.cd = data.ownWeapons[i].cd;
        Updatedata();
        weaponPanel[i].SetActive(false);
        lowButton1.interactable = true;
        lowButton2.interactable = true;
        lowButton3.interactable = true;
        lowButton4.interactable = true;
    }

    private void UsePet(int i)  // 選擇寵物
    {
        for (int j = 0; j < petUp_Btn.Length; j++)
        {
            if (data.ownPets[j].owned == true)
            {
                petUse_img[j].sprite = petBtn_allimg[j];
            }
        }
        petBtn_img.sprite = petBtn_allimg[i];
        petUse_img[i].sprite = petUse_allimg[i];
        Player.pet1 = pet[i + 1];
        Updatedata();
        petPanel[i].SetActive(false);
        lowButton1.interactable = true;
        lowButton2.interactable = true;
        lowButton3.interactable = true;
        lowButton4.interactable = true;
    }

    private void WeaponLevelUp(int i)  // 武器升級
    {
        data.weaponChips[i].count -= 10;
        data.ownWeapons[i].level++;
        data.ownWeapons[i].damage += 10;
        Updatedata();
    }

    private void PetLevelUp(int i)  // 武器升級
    {
        data.petChips[i].count -= 10;
        data.ownPets[i].level++;
        data.ownPets[i].damage += 10;
        Updatedata();
    }

    private void ShowAchieve() // 顯示成就畫面
    {
        trophy_Panel.SetActive(true);
        lowButton1.interactable = false;
        lowButton2.interactable = false;
        lowButton3.interactable = false;
        lowButton4.interactable = false;
        ps.SetActive(false);
        red_dot.SetActive(false);
    }

    private void NoShowAchieve() // 關閉成就畫面
    {
        trophy_Panel.SetActive(false);
        ps.SetActive(true);
        lowButton1.interactable = true;
        lowButton2.interactable = true;
        lowButton3.interactable = true;
        lowButton4.interactable = true;
    }

    private void Achievement() // 成就判定
    {
        if (data.weapon_Count >= 2)
        {
            data.achievements[0].owned = true;
            trophy_reward_Btn[0].interactable = true;
            if (data.achievements[0].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
        else if (data.weapon_Count >= 4)
        {
            data.achievements[1].owned = true;
            trophy_reward_Btn[1].interactable = true;
            if (data.achievements[1].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
        else if (data.weapon_Count >= 6)
        {
            data.achievements[2].owned = true;
            trophy_reward_Btn[2].interactable = true;
            if (data.achievements[2].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
        else if (data.weapon_Count >= 8)
        {
            data.achievements[3].owned = true;
            trophy_reward_Btn[3].interactable = true;
            if (data.achievements[3].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
        else if (data.ifinite_round >= 5)
        {
            data.achievements[4].owned = true;
            trophy_reward_Btn[4].interactable = true;
            if (data.achievements[4].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
        else if (data.ifinite_round >= 10)
        {
            data.achievements[5].owned = true;
            trophy_reward_Btn[5].interactable = true;
            if (data.achievements[5].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
        else if (data.ifinite_round >= 15)
        {
            data.achievements[6].owned = true;
            trophy_reward_Btn[6].interactable = true;
            if (data.achievements[6].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
        else if (data.ifinite_round >= 20)
        {
            data.achievements[7].owned = true;
            trophy_reward_Btn[7].interactable = true;
            if (data.achievements[7].rewarded == false)
            {
                red_dot.SetActive(true);
            }
        }
    }

    private void TrophyReward(int money, int number) // 成就獎項
    {
        data.PlayerCoin += money;
        trophy[number].sprite = trophy_Img;
        trophy_reward_Btn[number].interactable = false;
        data.achievements[number].rewarded = true;
        red_dot.SetActive(false);
        Updatedata();
    }

    private void ShowAreaPanel() // 顯示地區畫面
    {
        enteaArea_Btn.interactable = false;
        ps.SetActive(false);
        area_Panel.SetActive(true);
    }

    private void NoShowAreaPanel() // 關閉地區畫面
    {
        ps.SetActive(true);
        area_Panel.SetActive(false);
    }

    private void ActivateEnterArea_Btn() // 激活進入地區按鈕
    {
        enteaArea_Btn.interactable = true;
    }

    private void EnterArea() // 進入地區
    {
        if (area_Num == 0)
        {
            level_Text.text = "1.皇室古堡";
            level_img.sprite = allArea_img[0];
        }
        else if (area_Num == 1)
        {
            level_Text.text = "2.希臘神殿";
            level_img.sprite = allArea_img[1];
        }
        else if (area_Num == 2)
        {
            level_Text.text = "3.八幡鳥居";
            level_img.sprite = allArea_img[2];
        }
        ps.SetActive(true);
        area_Panel.SetActive(false);
    }

    private void ShowChooseLevel() // 顯示選擇層數
    {
        stage2_Btn.interactable = false;

        if (area_Num == 0)
        {
            if (LevelManager.lv_9 == true)
            {
                stage2_Btn.interactable = true;
            }
        }
        else if (area_Num == 1)
        {
            if (LevelManager.lv_21 == true)
            {
                stage2_Btn.interactable = true;
            }
        }
        else if (area_Num == 2)
        {
            if (LevelManager.lv_33 == true)
            {
                stage2_Btn.interactable = true;
            }
        }
        chooseLevel_Panel.SetActive(true);
    }

    private void NoShowChooseLevel() // 關閉選擇層數
    {
        chooseLevel_Panel.SetActive(false);
    }

    private void StageCount() // 進度更新
    {
        if (area_Num == 0)
        {
            schedule_Text.text = data.areas[0].stage + " / " + "10";
        }
        else if (area_Num == 1)
        {
            schedule_Text.text = data.areas[1].stage + " / " + "10";
        }
        else if (area_Num == 2)
        {
            schedule_Text.text = data.areas[2].stage + " / " + "10";
        }
    }
}
