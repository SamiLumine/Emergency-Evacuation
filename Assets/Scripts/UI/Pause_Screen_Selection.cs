using UnityEngine;

public class Pause_Screen_Selection : MonoBehaviour
{
    [SerializeField]
    private GameObject _startingScreen;

    private GameObject _currentScreen;

    private void Start()
    {
        _currentScreen = _startingScreen;

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if(child.gameObject == _startingScreen)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        gameObject.SetActive(false);
    }

    public void ResetPause()
    {
        if( _currentScreen != _startingScreen)
        {
            _currentScreen.SetActive(false);
            _startingScreen.SetActive(true);
            _currentScreen = _startingScreen;
        }
    }

    public void ChangeScreen(GameObject screen)
    {
        _currentScreen.SetActive(false);
        _currentScreen = screen;
        _currentScreen.SetActive(true);
    }
}
