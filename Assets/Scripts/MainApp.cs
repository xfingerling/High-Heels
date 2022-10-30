using UnityEngine;

public class MainApp : MonoBehaviour
{
    private void Awake()
    {
        Game.Run();
        SaveManager.Instance.Load();
    }
}
