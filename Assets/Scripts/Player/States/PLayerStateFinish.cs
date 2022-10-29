using UnityEngine;

public class PLayerStateFinish : PlayerStateBase, IState
{
    public void Construct()
    {
        player.moveVector = Vector3.zero;
        player.Animator.SetTrigger("Dancing");
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
