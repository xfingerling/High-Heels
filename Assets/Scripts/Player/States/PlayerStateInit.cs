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
            player.PlayerState.SetState<PlayerStateRunning>();
        }
    }

    public void Update()
    {
        //������� ��-�� ������(��� � �����) ��������
        player.ResetModel();
    }
}
