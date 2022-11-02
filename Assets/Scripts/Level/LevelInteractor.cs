using UnityEngine;

public class LevelInteractor : Interactor
{
    public int CurrentLevelIndex => _repository.CurrentLevelIndex;
    public int TotalLevels => _levelsPrefab.Length;

    private LevelRepository _repository;
    private GameObject[] _levelsPrefab;
    private GameObject _goCurrentLevel;
    private Transform _levelsContainer;
    private bool _isNewCycle;

    public override void OnCreate()
    {
        base.OnCreate();

        _repository = Game.GetRepository<LevelRepository>();
    }

    public override void Initialize()
    {
        base.Initialize();
        _levelsContainer = new GameObject("[LEVELS]").transform;

        _levelsPrefab = Resources.Load<LevelsData>("Data/LevelData").LevelPrefab;

        _goCurrentLevel = Object.Instantiate(_levelsPrefab[CurrentLevelIndex], _levelsContainer);
    }

    public void ResetLevel()
    {
        _goCurrentLevel.SetActive(false);

        _goCurrentLevel = Object.Instantiate(_levelsPrefab[CurrentLevelIndex], _levelsContainer);
    }

    public void NextLevel()
    {
        //если закончатся уровни, то пойдут по новой
        if (CurrentLevelIndex >= _levelsPrefab.Length - 1)
        {
            _repository.CurrentLevelIndex = 0;
            _isNewCycle = true;
        }


        if (!_isNewCycle)
            _repository.CurrentLevelIndex++;

        _repository.Save();
        _goCurrentLevel.SetActive(false);

        _goCurrentLevel = Object.Instantiate(_levelsPrefab[CurrentLevelIndex], _levelsContainer);
        _isNewCycle = false;
    }
}
