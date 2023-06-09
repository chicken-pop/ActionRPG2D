using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;

    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [ContextMenu("Delete Save File")]
    public void DeleteSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
        /*
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        saveManagers = FindAllSaveManagers();

        for (int i = 0; i < saveManagers.Count; i++)
        {
            Debug.Log(saveManagers[i]);　
        }
        //Invoke("LoadGame", 0.01f);
        LoadGame();
        */
    }

    private void Start()
    {
        
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        saveManagers = FindAllSaveManagers();

        for (int i = 0; i < saveManagers.Count; i++)
        {
            //Debug.Log(saveManagers[i]); スキルの確認
        }
        //Invoke("LoadGame", 0.01f);
        LoadGame(); //ここで必ずLoad。ここ変えると音量など他の設定がバグる。
        
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        //セーブデータがなかったら
        if (this.gameData == null)
        {
            NewGame();
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //セーブデータの更新
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        //セーブするデータを渡す
        dataHandler.Save(gameData);

    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        //ISaveManagerを持っているものを検索
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    //アプリを落としたときにセーブ
    private void OnApplicationQuit()
    {
        //SaveGame();
    }
}
