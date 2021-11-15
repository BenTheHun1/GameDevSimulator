using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class SaveScript : MonoBehaviour
{
    private GameManager gameManager;
    private string savePath;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        savePath = Application.persistentDataPath + "/gamesave.sav";
    }

    // Update is called once per frame
    void SaveData()
    {
        
    }
}
