using UnityEngine;
using UnityEngine.Events;

public class End_Program : MonoBehaviour
{
    private static UnityEvent _stopEvent = new UnityEvent();

    public static UnityEvent GetStopEvent()
    {
        return _stopEvent;
    }
}
