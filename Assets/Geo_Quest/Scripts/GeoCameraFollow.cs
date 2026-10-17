using UnityEngine;

// ============================================================================
//  Geo Quest - Camera Follow
// ----------------------------------------------------------------------------
//  Keeps the Main Camera nicely centred on the player while it travels through
//  the level. Drag the Player into the "Target" slot in the Inspector.
//
//  (Nothing is spawned - this only moves the camera that is already in the scene.)
// ============================================================================
public class GeoCameraFollow : MonoBehaviour
{
    [Tooltip("Drag the Player here.")]
    public Transform target;

    [Tooltip("Where the camera sits relative to the player.")]
    public Vector2 offset = new Vector2(0f, 1.5f);

    [Tooltip("Smaller = snappier camera.")]
    public float smoothTime = 0.12f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 wanted = new Vector3(target.position.x + offset.x,
                                     target.position.y + offset.y,
                                     transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, wanted, ref velocity, smoothTime);
    }
}
