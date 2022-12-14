using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SaveManager();
            return _instance;
        }
    }
    private static SaveManager _instance;

    private const string SAVE_FILE_NAME = "SaveData.ss";

    public SaveData saveData;

    private BinaryFormatter _formatter;

    public SaveManager()
    {
        _formatter = new BinaryFormatter();

        Load();
    }

    public void Load()
    {
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Open, FileAccess.Read);
            saveData = _formatter.Deserialize(file) as SaveData;
            file.Close();
        }
        catch
        {
            Debug.Log("Save file not found");
            Save();
        }

    }

    public void Save()
    {
        if (saveData == null)
            saveData = new SaveData();

        FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write);
        _formatter.Serialize(file, saveData);
        file.Close();
    }
}
