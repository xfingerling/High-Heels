public class PLayerStateFinish : PlayerStateBase, IState
{
    public void Construct()
    {
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
