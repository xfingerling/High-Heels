using UnityEngine;

public class PlayerStateDeath : PlayerStateBase, IState
{
    public void Construct()
    {
        player.Animator.SetTrigger("Death");

        player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
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
