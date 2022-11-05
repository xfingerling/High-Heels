using DG.Tweening;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private float _offsetX;
    [SerializeField] private GameObject _marker;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _speed;

    private Vector3 _from, _to;
    private float _wallSize = 1;

    private void Awake()
    {
        _from = transform.position;
        _to = new Vector3(_offsetX - 1 + _from.x, _from.y, _from.z);
        _speed /= 2;

        _marker.transform.position = GetMidpointVector(_from, _to);
        _marker.transform.localScale = new Vector3(_offsetX, _marker.transform.localScale.y, _marker.transform.localScale.z);
    }

    private void Start()
    {
        DOTween.Sequence()
            .Append(_model.transform.DOMoveX(_to.x, _speed))
            .SetEase(Ease.Linear)
            .Append(_model.transform.DOMoveX(_from.x, _speed))
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    private Vector3 GetMidpointVector(Vector3 a, Vector3 b)
    {
        Vector3 result = Vector3.zero;

        result.x = (a.x + b.x) / 2;
        result.y = (a.y + b.y) / 2;
        result.z = (a.z + b.z) / 2;

        return result;
    }
}
