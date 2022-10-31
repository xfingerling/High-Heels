using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIInterface : MonoBehaviour
{
    [SerializeField] private Transform _hudLayer;
    [SerializeField] private Transform _popupLayer;
    [SerializeField] private Camera _uiCamera;

    public Transform HudLayer => _hudLayer;
    public Transform PopupLayer => _popupLayer;

    private void Awake()
    {
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(_uiCamera);
    }
}
