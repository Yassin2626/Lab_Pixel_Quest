using UnityEngine;

// ============================================================================
//  Geo Quest - Camera Follow
// ----------------------------------------------------------------------------
//  Keeps the camera centred on the player while they run through the level.
//  Drag the Player into the "Target" box in the Inspector.
//
//  Nothing is created here - this only moves the camera that is already in
//  the scene.
// ============================================================================
public class GeoCameraFollow : MonoBehaviour
{
    public Transform target;                          // the player
    public Vector2 offset = new Vector2(0f, 1.5f);    // sit a little above them
    public float smoothTime = 0.12f;                  // smaller = snappier camera

    private Vector3 speed;    // used by SmoothDamp to smooth the movement

    void LateUpdate()
    {
        if (target == null) return;

        // Where we would like the camera to be.
        Vector3 wanted = new Vector3(target.position.x + offset.x,
                                     target.position.y + offset.y,
                                     transform.position.z);

        // Glide towards that spot instead of snapping to it.
        transform.position = Vector3.SmoothDamp(transform.position, wanted, ref speed, smoothTime);
    }
}
