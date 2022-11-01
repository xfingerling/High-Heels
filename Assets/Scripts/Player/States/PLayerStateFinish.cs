public class PLayerStateFinish : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Dancing");
        UIcontroller.ShowPopup<UIFinishPopup>();
    }

    public void Destruct()
    {
        UIcontroller.GetView<UIFinishPopup>().Hide();
    }

    public void Transition()
    {

    }

    public void Update()
    {
        player.ResetModel();
    }
}
