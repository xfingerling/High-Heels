public class PLayerStateFinish : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Dancing");
        player.AudioSources.Victory.Play();
        player.ConffetiParticle.Play();

        UIcontroller.ShowPopup<UIFinishPopup>();
    }

    public void Destruct()
    {
        player.ConffetiParticle.Stop();
        playerInteractor.ResetCoinPerSession();

        UIcontroller.HideHUD();
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
