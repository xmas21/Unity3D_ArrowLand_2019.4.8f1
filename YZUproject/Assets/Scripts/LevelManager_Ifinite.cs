using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager_Ifinite : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;
    [Header("怪物類型")]
    public GameObject[] enemy_Type;
    [Header("怪物生成點")]
    public GameObject[] enemy_SpawnPoint;
    [Header("怪物資料")]
    public EnemyData[] enemy_Data;
    [Header("怪物生成點")]
    public ParticleSystem[] SP_Partical;
    [Header("設定畫面")]
    public GameObject set_Panel;

    [Header("當前回合")]
    public int round;
    [Header("總殺敵數量")]
    public int kill_count;
    [Header("敵人生命陣列")]
    public float[] enemys_hp;
    [Header("敵人最大生命陣列")]
    public float[] enemys_hpMax;
    [Header("敵人攻擊力陣列")]
    public float[] enemys_attack;
    [Header("敵人跑速陣列")]
    public float[] enemys_speed;
    [Header("敵人跑速陣列")]
    public Vector2[] enemys_coin;

    private bool startLevel;
    private bool isPass;
    private int count;       // 每關放怪數量
    private int round_count; // 總遊玩回合

    private Enemy_IFI[] enemys;
    private Text killed_Text;     // 擊殺數量文字
    private Text lastEnemy_Text;  // 剩餘數量文字
    private Text round_Text;      // 擊殺數量文字
    private Text start_Text;      // 開始倒數
    private Button set_Btn;

    [System.Obsolete]
    private void Start()
    {
        killed_Text = GameObject.Find("擊殺數量文字").GetComponent<Text>();
        lastEnemy_Text = GameObject.Find("剩餘敵人文字").GetComponent<Text>();
        round_Text = GameObject.Find("回合數文字").GetComponent<Text>();
        start_Text = GameObject.Find("開始倒數").GetComponent<Text>();
        set_Btn = GameObject.Find("選單按鈕").GetComponent<Button>();

        for (int i = 0; i < SP_Partical.Length; i++)
        {
            int index = i;
            SP_Partical[index] = enemy_SpawnPoint[index].GetComponent<ParticleSystem>();
        }

        for (int i = 0; i < enemy_Data.Length; i++)
        {
            int index = i;
            enemys_hp[index] = enemy_Data[index].hp;
            enemys_hpMax[index] = enemy_Data[index].hpMax;
            enemys_attack[index] = enemy_Data[index].attack;
            enemys_speed[index] = enemy_Data[index].speed;
            enemys_coin[index] = enemy_Data[index].coinRandom;
        }

        isPass = false;
        startLevel = false;
        round = 1;
        kill_count = 0;
        count = 7;
        round_count = 1;

        ClickBtn();
        StartCoroutine(SpawnEnemy());
    }

    [System.Obsolete]
    private void Update()
    {
        Physics.IgnoreLayerCollision(9, 9);
        UpdateData();
        IsPass();
    }

    [System.Obsolete]
    private IEnumerator SpawnEnemy() // 生成敵人
    {
        start_Text.color = new Color(1, 1, 1, 1);
        start_Text.text = "回合" + round_count.ToString("F0");
        yield return new WaitForSeconds(1f);
        start_Text.text = "3";
        yield return new WaitForSeconds(1f);
        start_Text.text = "2";
        yield return new WaitForSeconds(1f);
        start_Text.text = "1";
        yield return new WaitForSeconds(1f);
        start_Text.text = "GO";
        yield return new WaitForSeconds(1f);
        start_Text.color = new Color(1, 1, 1, 0);

        for (int i = 0; i < count; i++)
        {
            int h = Random.Range(0, enemy_Type.Length);
            int r = Random.Range(0, enemy_SpawnPoint.Length);
            SP_Partical[r].startColor = new Color(1, 0, 0, 1);
            Instantiate(enemy_Type[h], enemy_SpawnPoint[r].transform.position, enemy_SpawnPoint[r].transform.rotation);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < SP_Partical.Length; i++)
        {
            int index = i;
            SP_Partical[index].startColor = new Color(1, 0, 0, 0);
        }
        startLevel = true;
        isPass = false;
    }

    [System.Obsolete]
    private void IsPass()     // 通關判定
    {
        enemys = FindObjectsOfType<Enemy_IFI>();

        if (enemys.Length == 0 && startLevel == true && isPass == false)
        {
            lastEnemy_Text.text = "0" + "/" + count.ToString("F0");
            Item_IFI[] coins = FindObjectsOfType<Item_IFI>();

            for (int i = 0; i < coins.Length; i++)
            {
                coins[i].pass = true;
            }

            isPass = true;
            NextRound();
            return;
        }
    }

    [System.Obsolete]
    private void NextRound()
    {
        kill_count += count;
        round_count++;
        count += 3;
        LevelUp();
        StartCoroutine(SpawnEnemy());
        if (data.ifinite_round < round_count)
        {
            data.ifinite_round = round_count;
        }
        else if (data.ifinite_round < round_count)
        {
            return;
        }
    }

    public void MainMenu()    // 回主選單
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

    public void ResumeGame()  // 繼續遊戲
    {
        set_Panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void ClickBtn()
    {
        set_Btn.onClick.AddListener(ShowSet);
    }

    private void UpdateData() // 更新資料
    {
        enemys = FindObjectsOfType<Enemy_IFI>();
        if (enemys.Length == 0)
        {
            return;
        }
        else
        {
            killed_Text.text = kill_count.ToString("F0");
            lastEnemy_Text.text = enemys.Length + "/" + count.ToString("F0");
            round_Text.text = round_count.ToString("F0");
        }
    }

    private void ShowSet()    // 設定畫面
    {
        Time.timeScale = 0;
        set_Panel.SetActive(true);
    }

    private void LevelUp()    // 敵人數值升級
    {
        for (int i = 0; i < enemy_Data.Length; i++)
        {
            int index = i;
            enemys_hp[index] += 100;
            enemys_hpMax[index] += 100;
            enemys_attack[index] += 30;
            enemys_speed[index] += 1;
            enemys_coin[index].x += 5;
            enemys_coin[index].y += 10;
        }
    }
}
