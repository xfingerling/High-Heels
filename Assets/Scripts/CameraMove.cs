using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _lerpRate = 5f;
    [SerializeField] private Vector3 _offset;

    private Transform _target;

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPos = new Vector3(0, _target.position.y, _target.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos - _offset, Time.deltaTime * _lerpRate);
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _target = playerInteractor.Player.transform;
    }
}
