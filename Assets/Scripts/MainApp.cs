using UnityEngine;

public class MainApp : MonoBehaviour
{
    private void Awake()
    {
        Game.Run();
    }

    private void LateUpdate()
    {
        PlayerInput.Instance.ResetInputs();
    }
}
