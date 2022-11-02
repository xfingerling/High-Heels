using TMPro;
using UnityEngine;

public class UIGameHUD : View
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _coinPreLevelText;

    public override void Initialize()
    {

    }

    private void OnEnable()
    {
        player.OnPickedUpCoinEvent += OnPickedUpCoin;
        _currentLevelText.text = $"Level {levelInteractor.CurrentLevelIndex + 1}";
    }

    private void OnDisable()
    {
        _coinPreLevelText.text = "0";
        player.OnPickedUpCoinEvent -= OnPickedUpCoin;
    }


    private void OnPickedUpCoin()
    {
        _coinPreLevelText.text = $"{playerInteractor.CoinsPerLevel}";
    }
}
