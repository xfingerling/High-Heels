using UnityEngine;

public class PlayerStateInit : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Idle");
        player.ResetPlayer();

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
        //костыль из-за кривых(как я понял) анимаций
        player.ResetModel();
    }
}
