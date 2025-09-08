using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room_Mesh))]
public class Room_MeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Texte explicatif affiché en haut de l'inspector
        EditorGUILayout.HelpBox(
        "This script allows you to assign a generated Mesh to a GameObject.\n" +
        "The goal is to then save this GameObject as a Prefab with the mesh properties defined in the script (Assets > Prefabs > Rooms > ...).\n\n" +
        "To save the Prefab:\n\n" +
        "    1. In the Room_Mesh script, define the desired mesh properties (vertices, normals, triangles).\n" +
        "    2. Enable the Save_Room GameObject in the Hierarchy that this script is attached to.\n" +
        "    3. Enter Play Mode in the Unity Editor.\n" +
        "    4. Right-click on the GameObject associated with this script (the one used in 'Object To Save') in the Hierarchy window.\n" +
        "    5. Select 'Save as Prefab'.",
            MessageType.Info
        );

        // Affiche le reste de l'inspector par défaut
        base.OnInspectorGUI();
    }
}