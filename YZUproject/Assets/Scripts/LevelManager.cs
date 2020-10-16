using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class LevelManager : MonoBehaviour
{
    public GameObject objLight;
    public GameObject ramdomSkill;

    [Header("是否顯示隨機技能")]
    public bool autoShowSkill;
    [Header("是否自動開門")]
    public bool autoOpenDoor;
    [Header("復活介面")]
    public GameObject panelRevival;



    private Animator door;
    private Image imgCross;


    private void Start() // 轉場
    {
        door = GameObject.Find("木頭門").GetComponent<Animator>();
        imgCross = GameObject.Find("轉場效果").GetComponent<Image>();

        if (autoShowSkill) showSkill();
        if (autoOpenDoor) Invoke("Opendoor", 6);
    }

    private void showSkill()
    {
        ramdomSkill.SetActive(true);
    }

    private void Opendoor()
    {
        objLight.SetActive(true);
        door.SetTrigger("開門觸發");
    }

    public IEnumerator NextLevel()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("關卡2");

        async.allowSceneActivation = false;

        for (int i = 0; i < 100; i++)
        {
            imgCross.color += new Color(1, 1, 1, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }

        async.allowSceneActivation = true;

    }

    public IEnumerator ShowRevival()
    {
        panelRevival.SetActive(true);

        for (int i = 5; i > 0; i--)
        {
            panelRevival.transform.GetChild(1).GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    public void CloseRevival()
    {
        StopCoroutine(ShowRevival());
        panelRevival.SetActive(false);
    }

}
