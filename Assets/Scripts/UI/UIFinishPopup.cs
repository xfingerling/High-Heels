using UnityEngine;
using UnityEngine.UI;

public class UIFinishPopup : View
{
    [SerializeField] private Button _getCoinButton;

    public override void Initialize()
    {
        _getCoinButton.onClick.AddListener(OnGetCoin);
    }

    private void OnGetCoin()
    {
        levelInteractor.NextLevel();
        player.PlayerState.SetState<PlayerStateInit>();
    }
}
