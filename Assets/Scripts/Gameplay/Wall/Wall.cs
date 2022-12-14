using UnityEngine;

public class Wall : MonoBehaviour, IWall
{
    [SerializeField] private int _height = 1;

    public float EndPoint { get; private set; }
    public int Height => _height;

    private void Awake()
    {
        EndPoint = transform.position.z + 1;
    }
}
