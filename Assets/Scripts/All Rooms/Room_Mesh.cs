using UnityEditor;
using UnityEngine;


public class Room_Mesh : MonoBehaviour
{
    [SerializeField]
    GameObject _objectToSave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mesh mesh = new Mesh();

        // Making the floor mesh
        Vector3[] vertices = new Vector3[6];

        vertices[0] = new Vector3(4.66f, 0f, 3.24f);
        vertices[1] = new Vector3(4.62f, 0f, -3.10f);
        vertices[2] = new Vector3(-2.58f, 0f, -3.08f);
        vertices[3] = new Vector3(-2.60f, 0f, -3.31f);
        vertices[4] = new Vector3(-4.66f, 0f, -3.31f);
        vertices[5] = new Vector3(-4.63f, 0f, 3.31f);
        mesh.vertices = vertices;

        Vector3[] normals = new Vector3[6];
        normals[0] = Vector3.up;
        normals[1] = Vector3.up;
        normals[2] = Vector3.up;
        normals[3] = Vector3.up;
        normals[4] = Vector3.up;
        normals[5] = Vector3.up;
        mesh.normals = normals;

        int[] triangles = {0, 1, 5, 1, 2, 5, 2, 3, 5, 3, 4, 5};
        mesh.triangles = triangles;

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
