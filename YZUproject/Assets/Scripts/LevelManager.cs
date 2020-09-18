using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LevelManager : MonoBehaviour
{
    public GameObject objLight;
    public GameObject ramdomSkill;

    [Header("是否顯示隨機技能")]
    public bool autoShowSkill;
    [Header("是否自動開門")]
    public bool autoOpenDoor;

    private Animator door;
    private Image imgCross;


    private void Start()
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
        imgCross.color += new Color(1, 1, 1, 0.01f);
        yield return new WaitForSeconds(0.01f);
    }


}
