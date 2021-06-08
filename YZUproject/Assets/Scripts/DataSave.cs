using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Text;

public class DataSave : MonoBehaviour
{
    [SerializeField]
    private PlayerDate data;


    /*
    private void SaveData() // by Xml
    {
        XmlSerializer xml = new XmlSerializer(data.GetType());
        Stream s = File.Open(Application.dataPath + "/Save.xml", FileMode.Create);
        xml.Serialize(s, data);
        s.Close();
        ui_text.text = "儲存成功";
    }

    private void LoadData()
    {
        XmlSerializer xml = new XmlSerializer(data.GetType());
        Stream s = File.Open(Application.dataPath + "/Save.xml", FileMode.Open);
        data = (PlayerDate)xml.Deserialize(s);
        ui_text.text = "更新成功";
    }
    */

    /*
    private void SaveData() // by Json
    {
       string json = JsonUtility.ToJson(data));
        ui_text.text = "儲存成功" ;
    }

    private void LoadData()
    {
        data = JsonUtility.FromJson<PlayerDate>(json);
    }
    */

    public void SaveData()  // 儲存資料 by fileSreeam txt
    {
        FileStream fs = new FileStream(Application.dataPath + "/Save.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        #region 
        sw.WriteLine(data.player_name);
        sw.WriteLine(data.hp = 600f);
        sw.WriteLine(data.attack = 60f);
        sw.WriteLine(data.CriticalAttack = 0f);
        sw.WriteLine(data.cd = 1f);
        sw.WriteLine(data.speed = 260f);
        sw.WriteLine(data.armor = 0.02f);
        sw.WriteLine(data.rehp = 0.5f);
        sw.WriteLine(data.hpMax = 600f);
        sw.WriteLine(data.power = 1200f);
        sw.WriteLine(data.WeaponAttack = 28f);
        sw.WriteLine(data.PlayerCoin = 100f);
        sw.WriteLine(data.PlayerJewel = 0f);
        sw.WriteLine(data.weapon_Count = 1);
        sw.WriteLine(data.ifinite_round = 0);

        for (int i = 0; i < data.areas.Length; i++)
        {
            sw.WriteLine(data.areas[i].name);
            sw.WriteLine(data.areas[i].stage = 0);
        }

        for (int i = 0; i < data.ownWeapons.Length; i++)
        {
            sw.WriteLine(data.ownWeapons[i].name);
            sw.WriteLine(data.ownWeapons[i].owned);
            sw.WriteLine(data.ownWeapons[i].level = 1);
            sw.WriteLine(data.ownWeapons[i].damage);
            sw.WriteLine(data.ownWeapons[i].cd);
        }

        for (int i = 0; i < data.weaponChips.Length; i++)
        {
            sw.WriteLine(data.weaponChips[i].name);
            sw.WriteLine(data.weaponChips[i].count = 0);
        }

        for (int i = 0; i < data.ownPets.Length; i++)
        {
            sw.WriteLine(data.ownPets[i].name);
            sw.WriteLine(data.ownPets[i].owned = false);
            sw.WriteLine(data.ownPets[i].level = 1);
            sw.WriteLine(data.ownPets[i].damage);
        }

        for (int i = 0; i < data.petChips.Length; i++)
        {
            sw.WriteLine(data.petChips[i].name);
            sw.WriteLine(data.petChips[i].count);
        }

        for (int i = 0; i < data.talents.Length; i++)
        {
            sw.WriteLine(data.talents[i].name);
            sw.WriteLine(data.talents[i].level = 0);
        }

        #endregion
        sw.Close();
        fs.Close();
    }

    public void LoadData()  // 載入資料
    {
        FileStream fs = new FileStream(Application.dataPath + "/Save.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        data.player_name = sr.ReadLine();
        data.hp = float.Parse(sr.ReadLine());
        data.attack = float.Parse(sr.ReadLine());
        data.CriticalAttack = float.Parse(sr.ReadLine());
        data.cd = float.Parse(sr.ReadLine());
        data.speed = float.Parse(sr.ReadLine());
        data.armor = float.Parse(sr.ReadLine());
        data.rehp = float.Parse(sr.ReadLine());
        data.hpMax = float.Parse(sr.ReadLine());
        data.power = float.Parse(sr.ReadLine());
        data.WeaponAttack = float.Parse(sr.ReadLine());
        data.PlayerCoin = float.Parse(sr.ReadLine());
        data.PlayerJewel = float.Parse(sr.ReadLine());
        data.weapon_Count = int.Parse(sr.ReadLine());
        data.ifinite_round = int.Parse(sr.ReadLine());

        for (int i = 0; i < data.areas.Length; i++)
        {
            data.areas[i].name = sr.ReadLine();
            data.areas[i].stage = int.Parse(sr.ReadLine());
        }

        for (int i = 0; i < data.ownWeapons.Length; i++)
        {
            data.ownWeapons[i].name = sr.ReadLine();
            data.ownWeapons[i].owned = bool.Parse(sr.ReadLine());
            data.ownWeapons[i].level = int.Parse(sr.ReadLine());
            data.ownWeapons[i].damage = float.Parse(sr.ReadLine());
            data.ownWeapons[i].cd = float.Parse(sr.ReadLine());
        }

        for (int i = 0; i < data.weaponChips.Length; i++)
        {
            data.weaponChips[i].name = sr.ReadLine();
            data.weaponChips[i].count = int.Parse(sr.ReadLine());
        }

        for (int i = 0; i < data.ownPets.Length; i++)
        {
            data.ownPets[i].name = sr.ReadLine();
            data.ownPets[i].owned = bool.Parse(sr.ReadLine());
            data.ownPets[i].level = int.Parse(sr.ReadLine());
            data.ownPets[i].damage = int.Parse(sr.ReadLine());
        }

        for (int i = 0; i < data.petChips.Length; i++)
        {
            data.petChips[i].name = sr.ReadLine();
            data.petChips[i].count = int.Parse(sr.ReadLine());
        }

        for (int i = 0; i < data.talents.Length; i++)
        {
            data.talents[i].name = sr.ReadLine();
            data.talents[i].level = int.Parse(sr.ReadLine());
        }
    }
}
