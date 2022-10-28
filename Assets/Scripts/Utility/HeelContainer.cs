using UnityEngine;

public class HeelContainer : MonoBehaviour
{
    [SerializeField] private Transform _heelContainer;

    private void LateUpdate()
    {
        transform.position = _heelContainer.position;
    }
}
