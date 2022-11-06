using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPickedUpCoinEvent;

    [SerializeField] private Transform _leftHeelContainer;
    [SerializeField] private Transform _rightHeelContainer;
    [SerializeField] private GameObject _model;
    [SerializeField] private SphereCollider _groundCheckerPivot;
    [SerializeField] private float _checkGroundRadius = 0.3f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private ParticleSystem _conffetiParticle;

    public Transform LeftHeelContainer => _leftHeelContainer;
    public Transform RightHeelContainer => _rightHeelContainer;
    public PlayerState PlayerState { get; private set; }
    public Animator Animator { get; private set; }
    public bool IsGrounded { get; private set; }
    public ParticleSystem ConffetiParticle => _conffetiParticle;
    public PlayerAudioSources AudioSources { get; private set; }

    private int heelAmount => _heelPool.GetAmountActiveHeels();
    private IState _currentState => PlayerState.currentState;
    private HeelPool _heelPool;
    private List<Heels> _pickedHeels;
    private Wall _prevWall;
    private FinishWall _lastFinishWall;
    private PlayerInteractor _playerInteractor;

    private void Awake()
    {
        _playerInteractor = Game.GetInteractor<PlayerInteractor>();
        Animator = GetComponentInChildren<Animator>();
        AudioSources = GetComponentInChildren<PlayerAudioSources>();

        PlayerState = new PlayerState();

        _heelPool = new HeelPool(transform, 20);
        _pickedHeels = new List<Heels>();
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
        BalanceBoardTrigger balanceBoard = other.GetComponent<BalanceBoardTrigger>();

        if (wall)
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

        if (heels)
            PickupHeels(heels);

        if (finishWall)
        {
            LoseHeels(finishWall.Height);

            if (heelAmount == 0)
            {
                _lastFinishWall = finishWall;
                PlayerState.SetState<PLayerStateFinish>();
            }
        }

        if (coin)
            PickupCoin();

        if (balanceBoard)
            PlayerState.SetState<PlayerStateBalance>();
    }

    private void OnTriggerExit(Collider other)
    {
        BalanceBoardTrigger balanceBoard = other.GetComponent<BalanceBoardTrigger>();

        if (balanceBoard)
            PlayerState.SetState<PlayerStateRunning>();

    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        _heelPool.DicrementHeels(heelAmount);
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

    private void CheckDeath(int value)
    {
        if (value > heelAmount)
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
        _heelPool.IncrementHeels();

        transform.position = new Vector3(transform.position.x, heelAmount, transform.position.z);
        _groundCheckerPivot.transform.localPosition = new Vector3(0, -heelAmount, 0);

        AudioSources.Pickup.Play();
    }

    private void LoseHeels(int value)
    {
        CheckDeath(value);

        value = value > heelAmount ? heelAmount : value;

        _heelPool.DicrementHeels(value);

        if (_pickedHeels.Count != 0)
        {
            Heels heels = _pickedHeels[_pickedHeels.Count - 1];

            heels.transform.position = new Vector3(transform.position.x, 0, transform.position.z - 1);
            heels.gameObject.SetActive(true);
        }

        _groundCheckerPivot.transform.localPosition = new Vector3(0, _groundCheckerPivot.transform.localPosition.y + value, 0);

        AudioSources.Drop.Play();
    }
    #endregion
}
