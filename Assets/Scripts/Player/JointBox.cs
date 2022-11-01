using UnityEngine;

public class JointBox : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleDown;
    [SerializeField] private Vector3 _scaleUp;
    [SerializeField] private float _scaleKoefficient;
    [SerializeField] private float _rotationKoefficient;

    private ConfigurableJoint _joint;
    private Transform _playerTransform;
    private Transform _playerBody;

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
        _joint = GetComponent<ConfigurableJoint>();

    }

    private void Update()
    {
        if (_playerTransform == null) return;

        Vector3 reletivePosition = _playerTransform.InverseTransformPoint(transform.position);

        _playerBody.localEulerAngles = new Vector3(0, 0, -reletivePosition.x) * _rotationKoefficient;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _playerTransform = playerInteractor.Player.transform;
        _playerBody = _playerTransform.Find("BeachGirl");

        _joint.connectedBody = playerInteractor.Player.GetComponent<Rigidbody>();

        transform.position = _playerTransform.position;
    }

    private Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        if (t < 0)
            return Vector3.LerpUnclamped(a, b, t + 1f);
        else
            return Vector3.LerpUnclamped(b, c, t);
    }

}
