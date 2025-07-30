using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARAnchor_Manager : MonoBehaviour
{
    private ARAnchorManager _anchorManager;

    void Start()
    {
        _anchorManager = GetComponentInParent<ARAnchorManager>();

        _anchorManager.trackablesChanged.AddListener(OnAnchorsChanged);
    }

    private void OnDestroy()
    {
        _anchorManager.trackablesChanged.RemoveListener(OnAnchorsChanged);
    }

    private void OnAnchorsChanged(ARTrackablesChangedEventArgs<ARAnchor> args)
    {
        foreach (var removedAnchor in args.removed)
        {
            if (removedAnchor.Value != null)
            {
                Destroy(removedAnchor.Value.gameObject);
            }

        }
    }
}
