using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_position : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask; // Assign the layer mask in the Inspector.

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        // Ignore collisions with the object itself by excluding its layer from the raycast.
        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, ~layerMask))
        {
            transform.position = raycastHit.point;
        }
    }
}
