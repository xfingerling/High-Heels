using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIMainMenu : View
{
    [SerializeField] private TextMeshProUGUI _startText;

    private Tween _startTextScaleTween;

    public override void Initialize()
    {
        _startTextScaleTween = DOTween.Sequence()
            .Append(_startText.transform.DOScale(1.5f, 0.7f))
            .Append(_startText.transform.DOScale(1f, 0.7f))
            .SetLoops(-1);
    }
}
