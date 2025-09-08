using UnityEditor;
using UnityEngine;


public class Room_Mesh : MonoBehaviour
{
    [Tooltip("GameObject whose MeshFilter will be assigned the generated room mesh")]
    [SerializeField]
    GameObject _objectToSave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mesh mesh = new Mesh();

        // Making the floor mesh
        Vector3[] vertices = new Vector3[4];

        // Define here the desired mesh properties:
        // - vertices (Vector3[])
        // - normals (Vector3[])
        // - triangles (int[])
        //
        // Example:
        //   mesh.vertices = ...
        //   mesh.normals = ...
        //   mesh.triangles = ...

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateUVDistributionMetrics();
        mesh.name = "RoomMesh";

        if (_objectToSave != null)
        {
            _objectToSave.GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}
