using UnityEngine;

public class Wall : MonoBehaviour, IWall
{
    [SerializeField] private int _height;

    public int Height => _height;
}
