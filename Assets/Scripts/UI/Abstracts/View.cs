using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected Player player;
    protected LevelInteractor levelInteractor;
    protected PlayerInteractor playerInteractor;

    private void Awake()
    {
        playerInteractor = Game.GetInteractor<PlayerInteractor>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();

        player = playerInteractor.Player;
    }

    public abstract void Initialize();

    public virtual void Update() { }

    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void Show() => gameObject.SetActive(true);
}
