using UnityEngine;

public class FinishWall : MonoBehaviour, IWall
{
    [SerializeField] private int _bonus;

    private int _height = 1;

    public int Height => _height;
    public int Bonus => _bonus;
}
