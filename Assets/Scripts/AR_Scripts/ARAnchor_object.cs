using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARAnchor_object : MonoBehaviour
{
    [SerializeField]
    private ARAnchorManager _anchorManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pose pose = new Pose(transform.position, transform.rotation);

        CreateAnchorAsync(pose);
    }

    private async void CreateAnchorAsync(Pose pose)
    {
        var result = await _anchorManager.TryAddAnchorAsync(pose);

        if (result.status.IsSuccess())
        {
            var anchor = result.value as ARAnchor;
            //_anchors.Add(anchor);

            //GameObject instance = Instantiate(_prefab, anchor.pose.position, anchor.pose.rotation);
            transform.SetParent(anchor.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
