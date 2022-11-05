public class PlayerStateFalling : PlayerStateBase, IState
{
    public void Construct()
    {

    }

    public void Destruct()
    {

    }

    public void Transition()
    {
        if (player.IsGrounded)
            player.PlayerState.SetState<PlayerStateRunning>();
    }

    public void Update()
    {
        playerMove.Move();
        playerMove.ApplyGravity();
    }
}
