using DG.Tweening;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _xSpeed;
    [SerializeField] private int _limitX = 2;

    private Camera _camera;
    private Vector3 _moveVector;
    private Vector3 _startTouchPos, _currentPosPlayer, _swipeDirection;
    private float _leftLimitPoint;
    private float _rightLimitPoint;
    private float _gravity = 14f;
    private float _terminalVelocity = 20f;
    private float _verticalVelocity;

    private void Awake()
    {
        _camera = Camera.main;

        _leftLimitPoint = transform.position.x - _limitX;
        _rightLimitPoint = transform.position.x + _limitX;
    }

    public void Move()
    {
        UpdateSwipeDirection();

        _moveVector.x = GetLimitXPosition(_swipeDirection.x);

        transform.Translate(Vector3.forward * _runningSpeed * Time.deltaTime);
        transform.DOMoveX(_moveVector.x, 0.5f);
    }

    public void ApplyGravity()
    {
        _verticalVelocity -= _gravity * Time.deltaTime;

        if (_verticalVelocity < -_terminalVelocity)
            _verticalVelocity = -_terminalVelocity;

        transform.Translate(Vector3.down * -_verticalVelocity * Time.deltaTime);
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
