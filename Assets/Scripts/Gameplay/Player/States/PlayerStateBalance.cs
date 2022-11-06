using DG.Tweening;
using UnityEngine;

public class PlayerStateBalance : PlayerStateBase, IState
{
    public void Construct()
    {
        player.transform.DOMoveX(0, 0.5f);
    }

    public void Destruct()
    {
        player.transform.DORotate(Vector3.zero, 0.5f);
    }

    public void Transition()
    {
        if (!player.IsGrounded)
            player.PlayerState.SetState<PlayerStateFalling>();
    }

    public void Update()
    {
        playerMove.MoveBalance();
    }
}
