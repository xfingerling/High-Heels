using UnityEngine;
using UnityEngine.UI;

public class UIDeathPopup : View
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _skipLevelButton;

    public override void Initialize()
    {
        _restartButton.onClick.AddListener(RestartLevel);
        _skipLevelButton.onClick.AddListener(SkipLevel);
    }

    private void RestartLevel()
    {
        levelInteractor.ResetLevel();
        player.PlayerState.SetState<PlayerStateInit>();
    }

    private void SkipLevel()
    {
        levelInteractor.NextLevel();
        player.PlayerState.SetState<PlayerStateInit>();
    }
}
