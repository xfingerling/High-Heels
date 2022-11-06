using DG.Tweening;
using UnityEngine;

public class PlayerStateRunning : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Running");
        UIcontroller.ShowHUD();
        player.transform.DORotate(Vector3.zero, 0.5f);
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
