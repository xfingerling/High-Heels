using UnityEngine;

public class PlayerStateInit : PlayerStateBase, IState
{
    public void Construct()
    {
        player.moveVector = Vector3.zero;
        player.Animator.SetTrigger("Idle");
    }

    public void Destruct()
    {

    }

    public void Transition()
    {
        if (Input.GetMouseButtonDown(0))
            player.PlayerState.SetState<PlayerStateRunning>();
    }

    public void Update()
    {

    }
}
