using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFinishPopup : View
{
    [SerializeField] private Button _getCoinButton;
    [SerializeField] private Button _getBonusCoinButton;
    [SerializeField] private TextMeshProUGUI _finishBonusText;
    [SerializeField] private TextMeshProUGUI _coinWithoutBonus;
    [SerializeField] private TextMeshProUGUI _coinWithBonus;

    public override void Initialize()
    {
        _getCoinButton.onClick.AddListener(OnCompleteLevelWithoutBonus);
        _getBonusCoinButton.onClick.AddListener(OnCompleteLevelWithBonus);
    }

    private void OnEnable()
    {
        _finishBonusText.text = $"CLAIM {player.GetFinishWallBonus()}x";
        _coinWithBonus.text = $"+{playerInteractor.CoinsPerLevel * player.GetFinishWallBonus()}";
        _coinWithoutBonus.text = playerInteractor.CoinsPerLevel.ToString();
    }

    private void OnCompleteLevelWithBonus()
    {
        playerInteractor.GetCoinWithBonus();
        levelInteractor.NextLevel();
        player.PlayerState.SetState<PlayerStateInit>();
    }

    private void OnCompleteLevelWithoutBonus()
    {
        playerInteractor.GetCoinWithoutBonus();
        levelInteractor.NextLevel();
        player.PlayerState.SetState<PlayerStateInit>();
    }
}
