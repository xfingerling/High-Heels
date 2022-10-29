using UnityEngine;

public class PlayerStateDeath : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Death");
        player.moveVector = Vector3.zero;
    }

    public void Destruct()
    {

    }

    public void Transition()
    {

    }

    public void Update()
    {

    }
}
