using UnityEngine;

public class PlayerStateDeath : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Death");

        player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);

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
