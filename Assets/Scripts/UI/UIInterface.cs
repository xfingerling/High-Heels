using UnityEngine;

public class UIInterface : MonoBehaviour
{
    [SerializeField] private Transform _hudLayer;
    [SerializeField] private Transform _popupLayer;

    public Transform HudLayer => _hudLayer;
    public Transform PopupLayer => _popupLayer;
}
