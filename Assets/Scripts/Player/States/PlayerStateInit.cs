public class PlayerStateInit : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Idle");
    }

    public void Destruct()
    {

    }

    public void Transition()
    {
        if (PlayerInput.Instance.Tap)
            player.PlayerState.SetState<PlayerStateRunning>();
    }

    public void Update()
    {

    }
}
