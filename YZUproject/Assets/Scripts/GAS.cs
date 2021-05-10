using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GAS : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerDate data;

    private void Start()
    {
        StartCoroutine(Read());
        //StartCoroutine(Change());
        //StartCoroutine(Write());
    }

    private IEnumerator Read()     // 讀取資料
    {
        WWWForm form = new WWWForm();

        form.AddField("method", "read");
        form.AddField("row", 2);
        form.AddField("col", 1);
        form.AddField("method", "read");
        form.AddField("row", 2);
        form.AddField("col", 2);
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbwy1NfNq6tDjz-psMhguYbmCWwJnGHupI5BGglWZfdcA23-A3Hijn0oyEkT2dVLQBP2Kg/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                print("錯誤：" + www.error);
            }
            else
            {
                print(www.downloadHandler.text);
            }
        }
    }

    private IEnumerator Change()    // 修改資料
    {
        WWWForm form = new WWWForm();

        form.AddField("method", "change");

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbwy1NfNq6tDjz-psMhguYbmCWwJnGHupI5BGglWZfdcA23-A3Hijn0oyEkT2dVLQBP2Kg/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                print("錯誤：" + www.error);
            }
            else
            {
                print(www.downloadHandler.text);
            }
        }
    }

    private IEnumerator Write()  // 寫入資料 
    {
        WWWForm form = new WWWForm();                       // 創建表格文件
                                                            //*************************************************************************************//
        form.AddField("method", "write");                   // 設定方法
        form.AddField("name", data.name);                   // 把data 寫給GAS
        form.AddField("hp", data.hp.ToString("F1"));
        form.AddField("atk", data.attack.ToString("F1"));
        form.AddField("critical", data.CriticalAttack.ToString("F1"));
        form.AddField("cd", data.cd.ToString("F1"));
        form.AddField("speed", data.speed.ToString("F1"));
        form.AddField("armor", data.armor.ToString("F1"));
        form.AddField("rehp", data.rehp.ToString("F1"));
        //*************************************************************************************//
        form.AddField("weaponAtk", data.WeaponAttack.ToString("F1"));
        form.AddField("coin", data.PlayerCoin.ToString("F1"));
        form.AddField("jewel", data.PlayerJewel.ToString("F1"));

        // Sending the request to API url with form object.
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbwy1NfNq6tDjz-psMhguYbmCWwJnGHupI5BGglWZfdcA23-A3Hijn0oyEkT2dVLQBP2Kg/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                print("錯誤：" + www.error);
            }
            else
            {
                print(www.downloadHandler.text);
            }
        }
    }
}
