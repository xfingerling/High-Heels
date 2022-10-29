using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private int _speedX;
    [SerializeField] private float _baseRunSpeed = 5f;
    [SerializeField] private Transform _rightHeelContainer;
    [SerializeField] private Transform _leftHeelContainer;

    public bool isInWall { get; set; }
    public PlayerState PlayerState;
    public Animator Animator { get; set; }
    public Vector3 moveVector { get; set; }
    public float verticalVelocity { get; set; }
    public float baseRunSpeed => _baseRunSpeed;
    public bool isGrounded { get; set; }

    private float _gravity = 14f;
    private float _terminalVelocity = 20f;
    private CharacterController _characterController;
    private IState _currentState => PlayerState.currentState;
    private int _heelCount;
    private List<GameObject> _poolLeftHeels;
    private List<GameObject> _poolRightHeels;

    private void Awake()
    {
        PlayerState = new PlayerState();
        Animator = GetComponentInChildren<Animator>();

        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        FillPoolHeels(10);
    }

    private void Update()
    {
        UpdateMotor();
    }

    public void ApplyGravity()
    {
        verticalVelocity -= _gravity * Time.deltaTime;

        if (verticalVelocity < -_terminalVelocity)
            verticalVelocity = -_terminalVelocity;
    }

    public void MoveX(Vector2 dir)
    {
        Vector3 direction = new Vector3(dir.x * Time.deltaTime * _speedX, 0f, 0f);
        _characterController.Move(direction);

    }

    private void UpdateMotor()
    {
        isGrounded = _characterController.isGrounded;
        if (_currentState != null)
        {
            _currentState.Update();
            _currentState.Transition();
        }

        //Move the player
        _characterController.Move(moveVector * Time.deltaTime);
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

    public void IncrementHeels()
    {
        GameObject leftHeel = _poolLeftHeels.Find(heel => !heel.activeInHierarchy);
        GameObject rightHeel = _poolRightHeels.Find(heel => !heel.activeInHierarchy);
        leftHeel.SetActive(true);
        rightHeel.SetActive(true);

        float heelHeight = 0.85f;

        _characterController.center = new Vector3(0, (1 - heelHeight) - _heelCount, 0);

        transform.position = new Vector3(transform.position.x, heelHeight + _heelCount, transform.position.z);

        _heelCount++;
    }

    private void DicrementHeels(int value)
    {
        var leftActive = _poolLeftHeels.FindAll(heel => heel.activeInHierarchy);
        var rightActive = _poolRightHeels.FindAll(heel => heel.activeInHierarchy);

        for (int i = leftActive.Count; i > leftActive.Count - value; i--)
        {
            int index = i - 1;

            leftActive[index].SetActive(false);
            rightActive[index].SetActive(false);
        }

        _heelCount -= value;
    }

    private void OnTriggerEnter(Collider other)
    {
        Wall wall = other.GetComponent<Wall>();
        FinishWall finishWall = other.GetComponent<FinishWall>();
        Heels heels = other.GetComponent<Heels>();

        if (wall != null)
            DicrementHeels(wall.Height);


        if (heels != null)
            IncrementHeels();

        if (finishWall != null)
        {
            DicrementHeels(finishWall.Height);
            if (_heelCount <= 0)
                PlayerState.SetState<PLayerStateFinish>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Wall wall = other.GetComponent<Wall>();

        if (wall != null)
            _characterController.center = new Vector3(0, _characterController.center.y + wall.Height, 0);
    }
}
