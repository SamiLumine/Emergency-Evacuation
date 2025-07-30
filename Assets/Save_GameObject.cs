using System.IO;
using UnityEditor;
using UnityEngine;

public class Save_GameObject : MonoBehaviour
{
    #if UNITY_EDITOR
    
    [MenuItem("GameObject/Save as Prefab")]
    static void SaveObject()
    {
        GameObject selectedObject = Selection.activeGameObject;

        string localPath = "";
        string directoryPath = "";

        if (selectedObject.transform.parent != null)
        {
            if (!Directory.Exists("Assets/Prefabs/Rooms/" + selectedObject.transform.parent.name))
                AssetDatabase.CreateFolder("Assets/Prefabs/Rooms", selectedObject.transform.parent.name);

            directoryPath = "Assets/Prefabs/Rooms/" + selectedObject.transform.parent.name + "/";
            localPath = directoryPath + selectedObject.name + ".prefab";
        }
        else
        {
            directoryPath = "Assets/Prefabs/Rooms/";
            localPath = "Assets/Prefabs/Rooms/" + selectedObject.name + ".prefab";
        }

        MeshFilter meshFilter = selectedObject.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.sharedMesh;
            if (mesh != null)
            {
                string meshPath = directoryPath + selectedObject.name + "_Mesh.asset";
                AssetDatabase.CreateAsset(mesh, meshPath);
                meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(meshPath);
            }
        }

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        bool prefabSuccess;
        PrefabUtility.SaveAsPrefabAssetAndConnect(selectedObject, localPath, InteractionMode.UserAction, out prefabSuccess);
        if (prefabSuccess == true)
            Debug.Log("Prefab was saved successfully");
        else
            Debug.Log("Prefab failed to save" + prefabSuccess);
    }

    #endif
}
