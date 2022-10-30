using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private Transform _rightHeelContainer;
    [SerializeField] private Transform _leftHeelContainer;
    [SerializeField] private SphereCollider _groundCheckerPivot;
    [SerializeField] private float _checkGroundRadius = 0.1f;
    [SerializeField] private LayerMask _groundMask;


    public bool isInWall { get; set; }
    public PlayerState PlayerState;
    public Animator Animator { get; set; }
    public Vector3 moveVector { get; set; }
    public float verticalVelocity { get; set; }
    public bool isGrounded { get; set; }

    private float _gravity = 14f;
    private float _terminalVelocity = 20f;
    private Wall _prevWall;

    private IState _currentState => PlayerState.currentState;
    private int _heelCount;
    private List<GameObject> _poolLeftHeels;
    private List<GameObject> _poolRightHeels;

    private void Awake()
    {
        PlayerState = new PlayerState();
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        FillPoolHeels(10);
    }

    private void Update()
    {
        UpdateMotor();

        if (_prevWall != null && transform.position.z > _prevWall.EndPoint)
            _prevWall = null;
    }

    public void ApplyGravity()
    {
        verticalVelocity -= _gravity * Time.deltaTime;

        if (verticalVelocity < -_terminalVelocity)
            verticalVelocity = -_terminalVelocity;
    }

    private void UpdateMotor()
    {
        isGrounded = IsOnGround();

        if (_currentState != null)
        {
            _currentState.Update();
            _currentState.Transition();
        }

        transform.Translate(moveVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Wall wall = other.GetComponent<Wall>();
        FinishWall finishWall = other.GetComponent<FinishWall>();
        Heels heels = other.GetComponent<Heels>();

        if (wall != null)
        {
            if (_prevWall == null)
            {
                _prevWall = wall;
                CheckDeath(wall.Height);
                DicrementHeels(wall.Height);
            }
            else
            {
                if (_prevWall.Height < wall.Height)
                {
                    CheckDeath(wall.Height - _prevWall.Height);
                    DicrementHeels(wall.Height - _prevWall.Height);
                    _prevWall = wall;
                }
            }
        }

        if (heels != null)
            IncrementHeels();

        if (finishWall != null)
        {
            DicrementHeels(finishWall.Height);
            if (_heelCount == 0)
                PlayerState.SetState<PLayerStateFinish>();
        }
    }

    private bool IsOnGround()
    {
        bool result = Physics.CheckSphere(_groundCheckerPivot.transform.position, _checkGroundRadius, _groundMask);
        return result;
    }

    private void FillPoolHeels(int heelCount)
    {
        _poolLeftHeels = new List<GameObject>();
        _poolRightHeels = new List<GameObject>();

        GameObject heelPrefab = Resources.Load<GameObject>("Heel");

        for (int i = 0; i < heelCount; i++)
        {
            GameObject goLeftHeel = Instantiate(heelPrefab, _leftHeelContainer);
            GameObject goRightHeel = Instantiate(heelPrefab, _rightHeelContainer);
            goLeftHeel.SetActive(false);
            goRightHeel.SetActive(false);

            _poolLeftHeels.Add(goLeftHeel);
            _poolRightHeels.Add(goRightHeel);

            float goHeelScale = goLeftHeel.transform.localScale.y;
            Vector3 hellPosition = new Vector3(0, (goHeelScale + _poolLeftHeels.Count - 1) * -1, 0);

            goLeftHeel.transform.localPosition = hellPosition;
            goRightHeel.transform.localPosition = hellPosition;
        }
    }

    private void IncrementHeels()
    {
        GameObject leftHeel = _poolLeftHeels.Find(heel => !heel.activeInHierarchy);
        GameObject rightHeel = _poolRightHeels.Find(heel => !heel.activeInHierarchy);
        leftHeel.SetActive(true);
        rightHeel.SetActive(true);

        _heelCount++;

        transform.position = new Vector3(transform.position.x, _heelCount, transform.position.z);
        _groundCheckerPivot.transform.localPosition = new Vector3(0, -_heelCount, 0);
    }

    private void DicrementHeels(int value)
    {
        if (value > _heelCount)
            value = _heelCount;

        var leftActive = _poolLeftHeels.FindAll(heel => heel.activeInHierarchy);
        var rightActive = _poolRightHeels.FindAll(heel => heel.activeInHierarchy);

        for (int i = leftActive.Count; i > leftActive.Count - value; i--)
        {
            int index = i - 1;

            leftActive[index].SetActive(false);
            rightActive[index].SetActive(false);
        }

        _groundCheckerPivot.transform.localPosition = new Vector3(0, _groundCheckerPivot.transform.localPosition.y + value, 0);

        _heelCount -= value;
    }

    private void CheckDeath(int value)
    {
        Debug.Log("Value: " + value);
        Debug.Log("Heels: " + _heelCount);
        if (value > _heelCount)
            PlayerState.SetState<PlayerStateDeath>();
    }
}
