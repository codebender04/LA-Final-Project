using UnityEngine;

public class PortalView : MonoBehaviour
{
    public Transform player;       // The Green Triangle
    public Transform portalEnter;  // e.g., Blue Portal
    public Transform portalExit;   // e.g., Red Portal

    void Update()
    {
        // 1. Get the player's position relative to the entrance portal
        Vector3 relativePos = portalEnter.InverseTransformPoint(player.position);

        // 2. Flip the relative position (Portals are usually back-to-back logic)
        // We rotate the position 180 degrees around the Y axis
        relativePos = Quaternion.Euler(0, 180, 0) * relativePos;

        // 3. Set the Yellow View's position relative to the exit portal
        transform.position = portalExit.TransformPoint(relativePos);

        // 4. Handle Rotation
        Quaternion relativeRot = Quaternion.Inverse(portalEnter.rotation) * player.rotation;
        transform.rotation = portalExit.rotation * Quaternion.Euler(0, 180, 0) * relativeRot;
    }
}