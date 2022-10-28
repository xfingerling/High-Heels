public abstract class PlayerStateBase
{
    protected Player player;

    public PlayerStateBase()
    {
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        player = playerInteractor.Player;
    }
}
