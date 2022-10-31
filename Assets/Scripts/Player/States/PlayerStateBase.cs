public abstract class PlayerStateBase
{
    protected Player player;
    protected PlayerMove playerMove;
    protected LevelInteractor levelInteractor;
    protected UIControllerInteractor UIcontroller;

    public PlayerStateBase()
    {
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        UIcontroller = Game.GetInteractor<UIControllerInteractor>();

        player = playerInteractor.Player;
        playerMove = player.GetComponent<PlayerMove>();
    }
}
