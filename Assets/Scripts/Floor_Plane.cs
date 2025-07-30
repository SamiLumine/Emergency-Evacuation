using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Floor_Plane : MonoBehaviour
{
    private GameObject _floorPlane = null;
    private LayerMask _layerMask;
    private Player_height _playerHeight;

    void Start()
    {
        int layer = LayerMask.NameToLayer("ARPlane");
        _layerMask = 1 << layer;
        _playerHeight = GetComponentInParent<Player_height>();
    }
    
    void Update()
    {
        if(_floorPlane == null)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 5.0f, _layerMask))
            {
                Debug.Log("Raycast hit");

                if (hit.transform.GetComponent<ARPlane>() != null)
                {
                    if (_floorPlane == null)
                    {
                        _floorPlane = hit.transform.gameObject;
                        _playerHeight.UpdatePlayerHeight(_floorPlane);
                        Debug.Log("Floor plane found: " + _floorPlane.name);
                    }
                }
            }
        }
        
    }

    public GameObject GetFloorPlane()
    {
        return _floorPlane;
    }

    public void PrintFloorMesh()
    {
        Mesh mesh = _floorPlane.GetComponent<MeshFilter>().sharedMesh;

        Debug.Log("-> Floor Mesh\nPosition: " + _floorPlane.transform.position + ", Rotation: " + _floorPlane.transform.eulerAngles + "\nVertices:");

        string vertices = "";
        for (int i = 0;  i < mesh.vertices.Length; i++)
        {
            vertices += mesh.vertices[i].ToString();

            if(i <  mesh.vertices.Length - 1)
            {
                vertices += ", ";
            }
        }

        Debug.Log(vertices + "\nTriangles:");

        string triangles = "[";
        for (int i = 0; i < mesh.triangles.Length;i++)
        {
            triangles += mesh.triangles[i].ToString();

            if (i < mesh.triangles.Length - 1)
            {
                triangles += ", ";
            }
        }

        Debug.Log(triangles + "]\nNormals:");

        string normals = "";
        for(int i = 0;i < mesh.normals.Length;i++)
        {
            normals += mesh.normals[i].ToString();

            if( i < mesh.normals.Length - 1)
            {
                normals += ", ";
            }
        }

        Debug.Log(normals);
    }
}
