using System.IO;
using UnityEditor;
using UnityEngine;

public class Print_Position_In_World : MonoBehaviour
{
    #if UNITY_EDITOR
    
    [MenuItem("GameObject/Print Position")]
    static void SaveObject()
    {
        GameObject selectedObject = Selection.activeGameObject;

        Debug.Log("Position of " + selectedObject.name + ": (" + selectedObject.transform.position.x + ", " + selectedObject.transform.position.y + ", " + selectedObject.transform.position.z + ")");
    }

    #endif
}
