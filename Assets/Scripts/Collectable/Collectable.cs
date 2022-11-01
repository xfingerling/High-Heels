using DG.Tweening;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject _model;

    private void Start()
    {
        DOTween.Sequence()
            .Append(_model.transform.DORotate(new Vector3(0, 200, 0), 2f))
            .Join(_model.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 2))
            .Append(_model.transform.DORotate(new Vector3(0, 0, 0), 2))
            .Join(_model.transform.DOScale(new Vector3(1, 1, 1), 1))
            .SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
