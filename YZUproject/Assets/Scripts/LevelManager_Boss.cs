using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager_Boss : LevelManager
{
    public bool insBoss;

    protected override void Start()
    {
        door = GameObject.Find("木頭門").GetComponent<Animator>();
        imgCross = GameObject.Find("轉場效果").GetComponent<Image>();

        player = FindObjectOfType<Player>();

        insBoss = true;

        if (autoShowSkill) ShowSkill();
        if (autoOpenDoor) Invoke("Opendoor", 6);
    }

    protected override void IsPass()
    {
        enemys = FindObjectsOfType<Enemy>();

        if (enemys.Length == 0 && insBoss == false)
        {
            Pass();
            return;
        }
    }
}
