using UnityEngine;

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
        //MoveZ();

        //player.MoveX(PlayerInput.Instance.TouchPosition);
        player.GetComponent<PlayerMove>().MoveX();
    }

    private void MoveZ()
    {
        Vector3 m = Vector3.zero;

        m.y = -2f;
        m.z = player.baseRunSpeed;

        player.moveVector = m;
    }
}
