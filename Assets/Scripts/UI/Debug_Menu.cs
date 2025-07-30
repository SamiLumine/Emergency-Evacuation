using UnityEngine;

public class Debug_Menu : MonoBehaviour
{
    private void Start()
    {
        if(!Debuging.IsDebuging())
        {
            Destroy(gameObject);
        }
    }
}
