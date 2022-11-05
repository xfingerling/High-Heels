using UnityEngine;

public class HeelContainer : MonoBehaviour
{
    protected Player _player;
    protected Vector3 _offset = new Vector3(0, 0.15f, 0);

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.Player;
    }
}
