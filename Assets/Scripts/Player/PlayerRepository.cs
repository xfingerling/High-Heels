public class PlayerRepository : Repository
{
    public int Coins { get; set; }

    public override void Initialize()
    {
        Coins = SaveManager.Instance.saveData.Coin;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.Instance.saveData.Coin = Coins;
        SaveManager.Instance.Save();
    }
}
