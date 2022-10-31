using UnityEngine;

[CreateAssetMenu(fileName = "Level Data")]
public class LevelsData : ScriptableObject
{
    [SerializeField] private GameObject[] _levelsPrefab;

    public GameObject[] LevelPrefab => _levelsPrefab;
}
