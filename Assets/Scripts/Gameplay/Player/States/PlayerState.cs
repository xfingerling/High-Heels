public class PlayerState : StateBase
{
    protected Player player;
    protected PlayerMove playerMove;
    protected LevelInteractor levelInteractor;
    protected PlayerInteractor playerInteractor;
    protected UIControllerInteractor UIcontroller;

    public PlayerState()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        InitState();
        SetDefaultState();
    }

    protected override void InitState()
    {
        base.InitState();

        CreateState<PlayerStateInit>();
        CreateState<PlayerStateRunning>();
        CreateState<PlayerStateFalling>();
        CreateState<PLayerStateFinish>();
        CreateState<PlayerStateDeath>();
        CreateState<PlayerStateBalance>();
    }

    private void SetDefaultState()
    {
        SetState<PlayerStateInit>();
    }
}
