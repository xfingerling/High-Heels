using UnityEngine;

public class LevelInteractor : Interactor
{
    public int CurrentLevelIndex => _repository.CurrentLevelIndex;
    public int TotalLevels => _levelsPrefab.Length;

    private LevelRepository _repository;
    private GameObject[] _levelsPrefab;
    private GameObject _goCurrentLevel;

    public override void OnCreate()
    {
        base.OnCreate();

        _repository = Game.GetRepository<LevelRepository>();
    }

    public override void Initialize()
    {
        base.Initialize();

        _levelsPrefab = Resources.Load<LevelsData>("Data/LevelData").LevelPrefab;

        _goCurrentLevel = Object.Instantiate(_levelsPrefab[CurrentLevelIndex]);
    }

    public void NextLevel()
    {
        _repository.CurrentLevelIndex++;
        _repository.Save();
        _goCurrentLevel.SetActive(false);

        _goCurrentLevel = Object.Instantiate(_levelsPrefab[CurrentLevelIndex]);
    }
}
