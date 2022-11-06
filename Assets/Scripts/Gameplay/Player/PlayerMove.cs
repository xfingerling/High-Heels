using DG.Tweening;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _xSpeed;
    [SerializeField] private int _limitX = 2;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _tiltSpeed = 10;
    [SerializeField] private float _balanceForce = 15;
    [SerializeField] private float _tiltLimit = 0.4f;

    private Player _player;
    private Camera _camera;
    private Vector3 _moveVector;
    private Vector3 _startTouchPos, _currentPosPlayer, _swipeDirection;
    private float _leftLimitPoint, _rightLimitPoint;
    private float _limitVelocity = 20f;
    private float _verticalVelocity;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _camera = Camera.main;

        _leftLimitPoint = transform.position.x - _limitX;
        _rightLimitPoint = transform.position.x + _limitX;
    }

    public void Move()
    {
        UpdateSwipeDirection();

        _moveVector.x = GetLimitXPosition(_swipeDirection.x);

        transform.DOMoveX(_moveVector.x, 0.5f);
        transform.Translate(Vector3.forward * _runningSpeed * Time.deltaTime);
    }

    public void MoveBalance()
    {
        UpdateSwipeDirection();

        float randomSideTilt = transform.rotation.z < 0 ? -1 : 1;

        if (Mathf.Abs(transform.rotation.z) > _tiltLimit)
            transform.DOMoveY(-1, 0.3f);


        if (TouchUtility.TouchCount > 0)
        {
            if (TouchUtility.GetTouch(0).phase == TouchPhase.Moved)
                transform.Rotate(new Vector3(0, 0, -_swipeDirection.x * _balanceForce) * Time.deltaTime);
        }

        transform.Rotate(new Vector3(0, 0, _tiltSpeed * randomSideTilt) * Time.deltaTime);

        transform.Translate(Vector3.forward * _runningSpeed * Time.deltaTime);
    }


    public void ApplyGravity()
    {
        if (_player.IsGrounded)
            _verticalVelocity = -2;

        _verticalVelocity -= _gravity * Time.deltaTime;

        if (_verticalVelocity < -_limitVelocity)
            _verticalVelocity = -_limitVelocity;

        transform.Translate(Vector3.down * -_verticalVelocity * Time.deltaTime);
    }

    public void ResetStartTouch()
    {
        _startTouchPos = Vector3.zero;
    }

    private void UpdateSwipeDirection()
    {
        if (TouchUtility.TouchCount > 0)
        {
            Touch touch = TouchUtility.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                _currentPosPlayer = transform.position;

                _startTouchPos = (_camera.transform.position - ((ray.direction) *
                        ((_camera.transform.position - transform.position).z / ray.direction.z)));
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (_startTouchPos == Vector3.zero)
                {
                    _currentPosPlayer = transform.position;

                    _startTouchPos = (_camera.transform.position - ((ray.direction) *
                            ((_camera.transform.position - transform.position).z / ray.direction.z)));
                }

                _swipeDirection = (_currentPosPlayer + ((_camera.transform.position - ((ray.direction) *
                        ((_camera.transform.position - transform.position).z / ray.direction.z))) - _startTouchPos));
            }
        }
        else
        {
            _swipeDirection = transform.position;
        }
    }

    private float GetLimitXPosition(float x)
    {
        if (_leftLimitPoint > x)
        {
            return _leftLimitPoint;
        }
        else if (_rightLimitPoint < x)
        {
            return _rightLimitPoint;
        }
        else
        {
            return x;
        }
    }
}
