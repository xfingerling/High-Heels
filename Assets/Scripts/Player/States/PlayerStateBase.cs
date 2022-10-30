public abstract class PlayerStateBase
{
    protected Player player;
    protected LevelInteractor levelInteractor;

    public PlayerStateBase()
    {
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();

        player = playerInteractor.Player;
    }
}
