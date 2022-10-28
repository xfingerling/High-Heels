using UnityEngine;

public class LevelInteractor : Interactor
{
    private GameObject[] _levelsPrefab;
    private GameObject _currentLevel;

    public override void Initialize()
    {
        base.Initialize();

        _levelsPrefab = Resources.LoadAll<GameObject>("Levels");

        _currentLevel = Object.Instantiate(_levelsPrefab[1]);
    }
}
