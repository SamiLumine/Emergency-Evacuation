using UnityEngine;

public class Debuging : MonoBehaviour
{
    [Tooltip("Check this to enable debug mode. If enabled, debug tools will be displayed in the pause menu")]
    [SerializeField]
    private bool _inDebug;
    
    static private bool _isDebuging;

    private void Awake()
    {
        _isDebuging = _inDebug;
    }

    static public bool IsDebuging()
    {
        return _isDebuging;
    }
}
