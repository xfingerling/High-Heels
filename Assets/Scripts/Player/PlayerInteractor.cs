using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player Player { get; private set; }
    public int Coins => _repository.Coins;
    public int CoinsPerLevel { get; private set; }

    private PlayerRepository _repository;


    public override void OnCreate()
    {
        base.OnCreate();

        _repository = Game.GetRepository<PlayerRepository>();
    }

    public override void Initialize()
    {
        base.Initialize();

        Player playerPrefab = Resources.Load<Player>("Player");
        JointBox boxPrefab = Resources.Load<JointBox>("JointBox");

        Player = Object.Instantiate(playerPrefab);
        Object.Instantiate(boxPrefab);
    }

    public void AddCoins(int value = 1)
    {
        CoinsPerLevel += value;
    }

    public void GetCoinWithoutBonus()
    {
        _repository.Coins += CoinsPerLevel;
        _repository.Save();
    }

    public void GetCoinWithBonus()
    {
        int coinWithBonus = CoinsPerLevel * Player.GetFinishWallBonus();

        _repository.Coins += coinWithBonus;
        _repository.Save();
    }

    public void ResetCoinPerSession()
    {
        CoinsPerLevel = 0;
    }
}
