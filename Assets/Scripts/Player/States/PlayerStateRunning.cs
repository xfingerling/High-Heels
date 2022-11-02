public class PlayerStateRunning : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Running");
    }

    public void Destruct()
    {

    }

    public void Transition()
    {
        if (!player.IsGrounded)
            player.PlayerState.SetState<PlayerStateFalling>();
    }

    public void Update()
    {
        playerMove.Move();
    }
}
