using System;

[Serializable]
public class SaveData
{
    public int Coin { get; set; }
    public int CurrentLevelIndex { get; set; }

    public SaveData()
    {
        Coin = 0;
        CurrentLevelIndex = 0;
    }
}
