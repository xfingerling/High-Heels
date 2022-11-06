using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _lerpRate = 2f;
    [SerializeField] private float _distnceRate = 0.5f;
    [SerializeField] private Vector3 _offset;

    private Player _player;
    private Transform _target;

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void LateUpdate()
    {
        if (_player == null) return;

        Vector3 targetPos = new Vector3(0, _target.position.y, _target.position.z);
        Vector3 newCameraPos = targetPos - _offset;
        newCameraPos.z = -_distnceRate * targetPos.y + newCameraPos.z;
        transform.position = Vector3.Lerp(transform.position, newCameraPos, Time.deltaTime * _lerpRate);
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.Player;
        _target = _player.transform;
    }
}
