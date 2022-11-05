using DG.Tweening;

public class PlayerStateBalance : PlayerStateBase, IState
{
    public void Construct()
    {
        player.transform.DOMoveX(0, 0.5f);
    }

    public void Destruct()
    {

    }

    public void Transition()
    {

    }

    public void Update()
    {
        playerMove.MoveBalance();
    }
}
