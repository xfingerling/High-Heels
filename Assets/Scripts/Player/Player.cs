using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public event Action OnPickedUpCoinEvent;

    [SerializeField] private GameObject _model;
    [SerializeField] private Transform _rightHeelContainer;
    [SerializeField] private Transform _leftHeelContainer;
    [SerializeField] private SphereCollider _groundCheckerPivot;
    [SerializeField] private float _checkGroundRadius = 0.3f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private ParticleSystem _conffetiParticle;

    public PlayerState PlayerState { get; private set; }
    public Animator Animator { get; private set; }
    public bool IsGrounded { get; private set; }
    public ParticleSystem ConffetiParticle => _conffetiParticle;
    public PlayerAudioSources AudioSources { get; private set; }

    private IState _currentState => PlayerState.currentState;
    private int _heelCount;
    private List<GameObject> _poolLeftHeels;
    private List<GameObject> _poolRightHeels;
    private List<Heels> _pickedHeels;
    private Wall _prevWall;
    private FinishWall _lastFinishWall;
    private PlayerInteractor _playerInteractor;

    private void Awake()
    {
        _playerInteractor = Game.GetInteractor<PlayerInteractor>();

        PlayerState = new PlayerState();
        Animator = GetComponentInChildren<Animator>();
        AudioSources = GetComponentInChildren<PlayerAudioSources>();

        _pickedHeels = new List<Heels>();
    }

    private void Start()
    {
        FillPoolHeels(10);
    }

    private void Update()
    {
        UpdateMotor();

        TryResetPrevWall();
    }

    private void OnTriggerEnter(Collider other)
    {
        Wall wall = other.GetComponent<Wall>();
        FinishWall finishWall = other.GetComponent<FinishWall>();
        Heels heels = other.GetComponent<Heels>();
        Coin coin = other.GetComponent<Coin>();

        Debug.Log(other.name);

        if (wall != null)
        {
            if (_prevWall == null)
            {
                _prevWall = wall;
                LoseHeels(wall.Height);
            }
            else
            {
                if (_prevWall.Height < wall.Height)
                {
                    LoseHeels(wall.Height - _prevWall.Height);
                    _prevWall = wall;
                }
            }
        }

        if (heels != null)
            PickupHeels(heels);

        if (finishWall != null)
        {
            LoseHeels(finishWall.Height);

            if (_heelCount == 0)
            {
                _lastFinishWall = finishWall;
                PlayerState.SetState<PLayerStateFinish>();
            }
        }

        if (coin != null)
            PickupCoin();
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        DicrementHeels(_heelCount);
        _heelCount = 0;
        _prevWall = null;
        _pickedHeels.Clear();
    }

    public void ResetModel()
    {
        _model.transform.localPosition = Vector3.zero;
    }

    public int GetFinishWallBonus()
    {
        if (_lastFinishWall != null)
            return _lastFinishWall.Bonus;

        return 1;
    }

    #region PRIVATE

    private void TryResetPrevWall()
    {
        if (_prevWall != null && transform.position.z > _prevWall.EndPoint)
            _prevWall = null;
    }

    private void UpdateMotor()
    {
        IsGrounded = IsOnGround();

        if (_currentState != null)
        {
            _currentState.Update();
            _currentState.Transition();
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
        if (value > _heelCount)
            PlayerState.SetState<PlayerStateDeath>();
    }

    private void PickupCoin()
    {
        _playerInteractor.AddCoins();
        AudioSources.Coin.Play();

        OnPickedUpCoinEvent?.Invoke();
    }

    private void PickupHeels(Heels heels)
    {
        _pickedHeels.Add(heels);
        IncrementHeels();

        AudioSources.Pickup.Play();
    }

    private void LoseHeels(int value)
    {
        CheckDeath(value);
        DicrementHeels(value);

        if (_pickedHeels.Count != 0)
        {
            Heels heels = _pickedHeels[_pickedHeels.Count - 1];

            heels.transform.position = new Vector3(transform.position.x, 0, transform.position.z - 1);
            heels.gameObject.SetActive(true);
        }

        AudioSources.Drop.Play();
    }
    #endregion
}
