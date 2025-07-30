using UnityEngine;
using UnityEngine.UI;

public class ToggleImages : MonoBehaviour
{
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ToggleImage()
    {
        _image.enabled = !_image.enabled;
    }
}
