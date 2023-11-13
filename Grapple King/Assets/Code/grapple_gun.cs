using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool isSpacebarHeld = false;
    private bool isGrappling = false;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpacebarHeld)
        {
            isSpacebarHeld = true;
            ShootGrappleTowardsTarget(TargetObject);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isSpacebarHeld = false;
            StopGrapple();
        }

        Vector3 newPosition = player.position + new Vector3(0f, 0f, 1.4f);
        transform.position = newPosition;

        // Adjust rotation based on the grapple status
        if (isGrappling)
        {
            // Calculate the direction vector from the gun to the grapplePoint.
            Vector3 direction = grapplePoint - transform.position;

            // Calculate the Z rotation to look at the grapplePoint.
            float newZRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the Z rotation while preserving the existing Y and X rotations.
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZRotation);
        }
        else
        {
            // Calculate the direction vector from the gun to the target object.
            Vector3 direction = TargetObject.position - transform.position;

            // Calculate the Z rotation to look at the target object.
            float newZRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the Z rotation while preserving the existing Y and X rotations.
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZRotation);
        }
    }

    // Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    
    void ShootGrappleTowardsTarget(Transform targetObject)
    {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, targetObject.position - gunTip.position, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            Vector3 direction = grapplePoint - player.position;

            // Keep the Z-component of the direction vector at 0.
            direction.z = 0;

            // Calculate the angle to look at the grapplePoint.
            float newZRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the Z rotation while preserving the existing Y and X rotations.
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZRotation);

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // The distance the grapple will try to keep from the grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // Adjust these values to fit your game.
            joint.spring = 9.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            isGrappling = true;
        }
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        isGrappling = false;
    }

    private Vector3 currentGrapplePosition;

    private void DrawRope()
    {
        if (!joint)
        {
            return;
        }

        // Calculate the direction vector from the player to the grapplePoint.
        Vector3 direction = grapplePoint - player.position;

        // Keep the Z-component of the direction vector at 0.
        direction.z = 0;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        // Update line renderer positions with the z-component set to 0.
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public Transform TargetObject;
}