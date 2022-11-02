using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIMainMenu : View
{
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _totalCoinsText;

    private Tween _startTextScaleTween;

    public override void Initialize()
    {
        DoStartTextAnim();
    }

    private void OnEnable()
    {
        _totalCoinsText.text = $"{playerInteractor.Coins}";
        _currentLevelText.text = $"{levelInteractor.CurrentLevelIndex + 1} / {levelInteractor.TotalLevels}";
    }

    private void DoStartTextAnim()
    {
        _startTextScaleTween = DOTween.Sequence()
            .Append(_startText.transform.DOScale(1.5f, 0.7f))
            .Append(_startText.transform.DOScale(1f, 0.7f))
            .SetLoops(-1);
    }
}
