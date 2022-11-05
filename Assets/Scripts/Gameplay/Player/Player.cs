using System;
using System.Collections.Generic;
using DG.Tweening;
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

    private IState _currentState => PlayerState.currentState;
    private HeelPool _heelPool;
    private int _heelCount;
    private List<Heels> _pickedHeels;
    private Wall _prevWall;
    private FinishWall _lastFinishWall;
    private PlayerInteractor _playerInteractor;

    private void Awake()
    {
        _playerInteractor = Game.GetInteractor<PlayerInteractor>();

        _heelPool = new HeelPool(transform, 10);

        PlayerState = new PlayerState();
        Animator = GetComponentInChildren<Animator>();
        AudioSources = GetComponentInChildren<PlayerAudioSources>();

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
        BalanceBoard balanceBoard = other.GetComponent<BalanceBoard>();

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

        if (other.CompareTag(typeof(BalanceBoard).Name))
            PlayerState.SetState<PlayerStateBalance>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(typeof(BalanceBoard).Name))
        {
            PlayerState.SetState<PlayerStateRunning>();
            transform.DORotate(Vector3.zero, 0.5f);
        }
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        _heelPool.DicrementHeels(_heelCount);
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
        _heelPool.IncrementHeels();

        _heelCount++;

        transform.position = new Vector3(transform.position.x, _heelCount, transform.position.z);
        _groundCheckerPivot.transform.localPosition = new Vector3(0, -_heelCount, 0);

        AudioSources.Pickup.Play();
    }

    private void LoseHeels(int value)
    {
        CheckDeath(value);

        value = value > _heelCount ? _heelCount : value;

        _heelPool.DicrementHeels(value);

        if (_pickedHeels.Count != 0)
        {
            Heels heels = _pickedHeels[_pickedHeels.Count - 1];

            heels.transform.position = new Vector3(transform.position.x, 0, transform.position.z - 1);
            heels.gameObject.SetActive(true);
        }

        _groundCheckerPivot.transform.localPosition = new Vector3(0, _groundCheckerPivot.transform.localPosition.y + value, 0);
        _heelCount -= value;

        AudioSources.Drop.Play();
    }
    #endregion
}
