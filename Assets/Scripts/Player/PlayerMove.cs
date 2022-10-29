using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float runningSpeed;
    [SerializeField] private float xSpeed;
    [SerializeField] private int _limitX = 2;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void Move()
    {
        float newX = 0;
        float touchXDelta = 0;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            touchXDelta = Input.GetTouch(0).deltaPosition.x;
        else if (Input.GetMouseButton(0))
            touchXDelta = Input.GetAxis("Mouse X");


        float limimDelta = Mathf.Clamp(touchXDelta, -1, 1);
        newX = xSpeed * limimDelta;

        Vector3 m = Vector3.zero;
        m.x = newX;
        m.y = 0;
        m.z = runningSpeed;

        _player.moveVector = m;
    }

    public void LimitMovementX()
    {
        if (transform.position.x > _limitX)
            transform.position = new Vector3(_limitX, transform.position.y, transform.position.z);
        else if (transform.position.x < -_limitX)
            transform.position = new Vector3(-_limitX, transform.position.y, transform.position.z);
    }
}
