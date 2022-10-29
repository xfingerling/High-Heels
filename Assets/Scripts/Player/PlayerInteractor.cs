using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player Player { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        Player playerPrefab = Resources.Load<Player>("Player");
        JointBox boxPrefab = Resources.Load<JointBox>("JointBox");

        Player = Object.Instantiate(playerPrefab);
        Object.Instantiate(boxPrefab);
    }
}
