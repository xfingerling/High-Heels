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
        if (!player.isGrounded)
            player.PlayerState.SetState<PlayerStateFalling>();
    }

    public void Update()
    {
        player.GetComponent<PlayerMove>().MoveX();
    }
}
