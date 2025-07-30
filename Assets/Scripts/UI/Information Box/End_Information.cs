using UnityEngine;

public class End_Information : MonoBehaviour
{
    private Information_Box _informationBox;

    void Start()
    {
        _informationBox = GetComponent<Information_Box>();

        End_Program.GetStopEvent().AddListener(LaunchEnd);
    }

    private void LaunchEnd()
    {
        _informationBox.AcitvateInformationBox();
    }
}
