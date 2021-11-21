using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class SaveScript : MonoBehaviour
{
    private GameManager gm;
    private string savePath;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        savePath = Application.persistentDataPath + "/gamesave.sav";
    }

    // Update is called once per frame
    public void SaveData()
    {
        var save = new Save()
        {
            ptsSaved = gm.pts,
            moneySaved = gm.money,
            ClickAmountSaved = gm.ClickAmount,
            ClickMultSaved = gm.ClickMult,
            AutoClickSaved = gm.AutoClick,
            AutoMultSaved = gm.AutoMult,
            upgradesTimesPurchased = new List<int>()
        };
        foreach (GameObject upgrade in gm.upgradesInScene)
        {
            int disTimesPurchased = upgrade.GetComponent<Upgrade>().timesPurchased;
            save.upgradesTimesPurchased.Add(disTimesPurchased);
        }

        var bf = new BinaryFormatter();
        using (var fileStream = File.Create(savePath))
        {
            bf.Serialize(fileStream, save);
        }

        Debug.Log("Data Saved");
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            Save save;

            var bf = new BinaryFormatter();
            using (var fileStream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)bf.Deserialize(fileStream);
            }

            gm.pts = save.ptsSaved;
            gm.money = save.moneySaved;
            gm.ClickAmount = save.ClickAmountSaved;
            gm.ClickMult = save.ClickMultSaved;
            gm.AutoClick = save.AutoClickSaved;
            gm.AutoMult = save.AutoMultSaved;

            for (int i = 0; i < gm.upgradeList.Count; i++) 
            {
                gm.upgradesInScene[i].GetComponent<Upgrade>().timesPurchased = save.upgradesTimesPurchased[i];
                gm.upgradesInScene[i].GetComponent<Upgrade>().Reload();
            }
            Debug.Log("Data Loaded");
        }
        else
        {
            Debug.LogWarning("Save File Doesn't Exist");
        }
    }
}
