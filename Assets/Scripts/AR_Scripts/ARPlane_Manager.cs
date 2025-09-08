using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlane_Manager : MonoBehaviour
{
    [Tooltip("Material applied to detected floor planes")]
    [SerializeField]
    Material _planeMaterial;

    private ARPlaneManager _planeManager;
    private List<GameObject> _FloorPlanes = new List<GameObject>();
    private bool _planeVisibility = false;
    private List<Material> _materialsFloor = new List<Material>();

    void Start()
    {
        _planeManager = GetComponentInParent<ARPlaneManager>();

        _planeManager.trackablesChanged.AddListener(OnPlanesChanged);
        _materialsFloor.Add(_planeMaterial);
    }

    private void OnDestroy()
    {
        _planeManager.trackablesChanged.RemoveListener(OnPlanesChanged);
    }

    private void OnPlanesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
    {
        if (args.added.Count > 0)
        {
            foreach (var plane in _planeManager.trackables)
            {
                if (plane.classifications != PlaneClassifications.Floor)
                {
                    plane.gameObject.SetActive(false);
                }
                else
                {

                    plane.GetComponent<MeshRenderer>().SetMaterials(_materialsFloor);
                    int LayerIgnoreRaycast = LayerMask.NameToLayer("ARPlane");
                    plane.gameObject.layer = LayerIgnoreRaycast;
                    _FloorPlanes.Add(plane.gameObject);
                    SetPlaneVisibility(_planeVisibility);
                }
            }
        }
    }

    public void TogglePlaneVisibility()
    {
        _planeVisibility = !_planeVisibility;

        SetPlaneVisibility(_planeVisibility);
    }

    public List<GameObject> GetFloorPlanes()
    {
        return _FloorPlanes;
    }

    private void SetPlaneVisibility(bool isVisible)
    {
        float fillAlpha = isVisible ? 0.5f : 0f;

        Debug.Log("-> Setting visibility for plane");
        foreach (var plane in _FloorPlanes)
        {
            SetTrackableAlpha(plane, fillAlpha);
        }
    }

    private void SetTrackableAlpha(GameObject trackable, float fillAlpha)
    {
        MeshRenderer meshRenderer = trackable.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = trackable.GetComponentInChildren<MeshRenderer>();
        }


        if (meshRenderer != null)
        {
            Color color = meshRenderer.material.color;
            color.a = fillAlpha;
            meshRenderer.material.color = color;
        }
        else
        {
            Debug.Log("-> Can't find 'meshRenderer' - on: " + trackable.name);
        }
    }
}
