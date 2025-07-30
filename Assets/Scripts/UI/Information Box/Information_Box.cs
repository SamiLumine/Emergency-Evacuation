using UnityEngine;

public class Information_Box : MonoBehaviour
{
    private bool _dialogOn = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DesactivateInformationBox()
    {
        gameObject.SetActive(false);
        _dialogOn = false;
    }

    public void AcitvateInformationBox()
    {
        gameObject.SetActive(true);
        _dialogOn = true;
    }

    public bool isInformationBooxOn()
    {
        return _dialogOn;
    }
}
