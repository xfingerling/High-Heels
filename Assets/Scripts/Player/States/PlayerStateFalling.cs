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
        if (player.isGrounded)
            player.PlayerState.SetState<PlayerStateRunning>();
    }

    public void Update()
    {
        playerMove.Move();
        playerMove.ApplyGravity();
    }
}
