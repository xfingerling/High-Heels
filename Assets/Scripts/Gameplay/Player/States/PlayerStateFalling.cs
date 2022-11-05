public class PlayerStateFalling : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Falling");
    }

    public void Destruct()
    {

    }

    public void Transition()
    {
        if (player.IsGrounded)
            player.PlayerState.SetState<PlayerStateRunning>();

        if (player.transform.position.y < -30)
            player.PlayerState.SetState<PlayerStateDeath>();
    }

    public void Update()
    {
        playerMove.Move();
        playerMove.ApplyGravity();
    }
}
