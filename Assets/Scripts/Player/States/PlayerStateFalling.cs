using UnityEngine;

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
        if (player.isGrounded)
            player.PlayerState.SetState<PlayerStateRunning>();
    }

    public void Update()
    {
        player.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.z = player.moveVector.z;
        m.y = player.verticalVelocity;

        player.moveVector = m;
    }
}
