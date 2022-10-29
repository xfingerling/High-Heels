public class PlayerState : StateBase
{
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
    }

    private void SetDefaultState()
    {
        SetState<PlayerStateInit>();
    }
}
