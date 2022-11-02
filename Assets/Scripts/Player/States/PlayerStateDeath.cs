using DG.Tweening;

public class PlayerStateDeath : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Death");
        player.transform.DOMoveY(0, 0.5f);
        player.AudioSources.Death.Play();

        UIcontroller.HideHUD();
        UIcontroller.ShowPopup<UIDeathPopup>();
    }

    public void Destruct()
    {
        UIcontroller.GetView<UIDeathPopup>().Hide();
    }

    public void Transition()
    {

    }

    public void Update()
    {

    }
}
