using UnityEngine;

public class PLayerStateFinish : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Dancing");
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
        if (Input.GetMouseButton(0))
        {
            levelInteractor.NextLevel();
            player.PlayerState.SetState<PlayerStateInit>();
        }
    }
}
