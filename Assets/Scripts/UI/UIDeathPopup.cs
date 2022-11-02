using UnityEngine;
using UnityEngine.UI;

public class UIDeathPopup : View
{
    [SerializeField] private Button _restartButton;

    public override void Initialize()
    {
        _restartButton.onClick.AddListener(RestartLevel);
    }

    private void RestartLevel()
    {
        levelInteractor.ResetLevel();
        player.PlayerState.SetState<PlayerStateInit>();
    }
}
