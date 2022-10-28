using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int _height;

    public int Height { get { return _height; } }
}
