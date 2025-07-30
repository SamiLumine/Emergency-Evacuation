using UnityEngine;

public class Debuging : MonoBehaviour
{
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
