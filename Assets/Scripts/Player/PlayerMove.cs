using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float runningSpeed;
    [SerializeField] private float xSpeed;

    private Player _player;
    private float limitX = 1;

    private Camera _cam;
    private Vector3 _startTouchPos, _currentPosPlayer, _targetPosPlayer;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _cam = Camera.main;
    }

    public void MoveX()
    {
        float newX = 0;
        float touchXDelta = 0;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            touchXDelta = Input.GetTouch(0).deltaPosition.x;
        else if (Input.GetMouseButton(0))
            touchXDelta = Input.GetAxis("Mouse X");


        float limimDelta = Mathf.Clamp(touchXDelta, -limitX, limitX);
        newX = xSpeed * limimDelta;

        Vector3 m = Vector3.zero;
        m.x = newX;
        m.y = -2f;
        m.z = _player.baseRunSpeed;

        _player.moveVector = m;
    }
}
