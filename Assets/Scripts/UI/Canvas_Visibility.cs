using UnityEngine;

public class Canvas_Visibility : MonoBehaviour
{
    private Canvas _canvas;
    private bool _visibility = false;

    void Start()
    {
        _canvas = GetComponent<Canvas>();

        SetCanvasVisibility(_visibility);
    }

    private void SetCanvasVisibility(bool visibility)
    {
        _canvas.enabled = visibility;
    }

    public void ToggleCanvasVisibility()
    {
        _visibility = !_visibility;
        SetCanvasVisibility(_visibility);
    }
}
