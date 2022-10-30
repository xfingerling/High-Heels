using UnityEngine;

public class LevelRepository : Repository
{
    public int CurrentLevelIndex { get; set; }


    public override void OnCreate()
    {
        CurrentLevelIndex = SaveManager.Instance.saveData.CurrentLevelIndex;
        Debug.Log(CurrentLevelIndex + "Load");
    }

    public override void Initialize()
    {


    }


    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.Instance.saveData.CurrentLevelIndex = CurrentLevelIndex;
        SaveManager.Instance.Save();
    }
}
