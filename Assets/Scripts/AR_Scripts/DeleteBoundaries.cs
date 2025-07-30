using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Meta;
using UnityEngine.XR.OpenXR;
using System.Collections;

public class DeleteBoundaries : MonoBehaviour
{

    void Awake()
    {
        StartCoroutine(waiter());
    }

    

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(5);

        var feature = OpenXRSettings.Instance.GetFeature<BoundaryVisibilityFeature>();
        var result = feature.TryRequestBoundaryVisibility(
            XrBoundaryVisibility.VisibilitySuppressed);

        if ((int)result ==
            BoundaryVisibilityFeature.XR_BOUNDARY_VISIBILITY_SUPPRESSION_NOT_ALLOWED_META)
        {
            Debug.Log("Boundary visibility suppression is not allowed.");
        }

        if (result < 0)
        {
            Debug.LogError("Failed to suppress boundary visibility.");
        }
    }
}


