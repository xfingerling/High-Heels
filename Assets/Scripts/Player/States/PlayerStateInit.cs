using UnityEngine;

public class PlayerStateInit : PlayerStateBase, IState
{
    public void Construct()
    {
        player.ResetPlayer();
        player.Animator.SetTrigger("Idle");

        UIcontroller.ShowPopup<UIMainMenu>();
    }

    public void Destruct()
    {
        UIcontroller.GetView<UIMainMenu>().Hide();
    }

    public void Transition()
    {
        if (TouchUtility.TouchCount > 0)
        {
            if (TouchUtility.GetTouch(0).phase == TouchPhase.Began)
                player.PlayerState.SetState<PlayerStateRunning>();
        }
    }

    public void Update()
    {

    }
}
