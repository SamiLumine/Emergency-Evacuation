using UnityEngine;

public class Launch_Tutorial : MonoBehaviour
{
    private Information_Box _informationBox;

    void Start()
    {
        _informationBox = GetComponent<Information_Box>();

        Check_Application_Started.GetEvent().AddListener(LaunchTuto);
    }

    private void LaunchTuto()
    {
        _informationBox.AcitvateInformationBox();
    }
}
