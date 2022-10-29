using UnityEngine;

public class FinishWall : MonoBehaviour, IWall
{
    private int _height = 1;

    public int Height => _height;
}
