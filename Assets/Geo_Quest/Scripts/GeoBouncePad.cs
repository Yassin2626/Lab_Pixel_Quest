using UnityEngine;

// ============================================================================
//  Geo Quest - Bounce Pad
// ----------------------------------------------------------------------------
//  Stand on this pad and you get thrown up into the air.
//
//  The pad already lives in the scene file. This script does not create
//  anything, it only changes the player's speed when they touch it.
// ============================================================================
public class GeoBouncePad : MonoBehaviour
{
    public float bounceForce = 16.5f;    // how hard the player is thrown upwards

    // This runs when the player touches the pad.
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only things with physics can be bounced. The player has physics,
        // and "attachedRigidbody" gives us that physics body.
        Rigidbody2D body = other.attachedRigidbody;
        if (body == null) return;

        // Keep the player's left / right speed, but shoot them straight up.
        body.velocity = new Vector2(body.velocity.x, bounceForce);
    }
}
