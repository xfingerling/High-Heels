using UnityEngine;

public class HeelContainer : MonoBehaviour
{
    [SerializeField] private Transform _heelContainer;

    private Vector3 _offset = new Vector3(0, 0.15f, 0);

    private void LateUpdate()
    {
        transform.position = _heelContainer.position - _offset;
    }
}
