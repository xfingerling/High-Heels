public class PlayerStateRunning : PlayerStateBase, IState
{
    private PlayerMove _playerMove;

    public void Construct()
    {
        if (_playerMove == null)
            _playerMove = player.GetComponent<PlayerMove>();

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
        _playerMove.Move();
        _playerMove.LimitMovementX();
    }
}
