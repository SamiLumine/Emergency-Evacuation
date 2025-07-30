using UnityEngine;
using UnityEngine.XR.Hands.Gestures;

public class Left_Hand_Gesture_Menu : MonoBehaviour
{
    [SerializeField]
    TogglePauseScreen _togglePauseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void LaunchPauseScreen()
    {
        _togglePauseScreen.PauseScreenToggle();
    }

}
