using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected Player player;

    private void Start()
    {
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();

        player = playerInteractor.Player;
    }

    public abstract void Initialize();

    public virtual void Update() { }

    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void Show() => gameObject.SetActive(true);
}
