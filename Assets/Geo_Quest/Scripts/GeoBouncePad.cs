using UnityEngine;

// ============================================================================
//  Geo Quest - Bounce Pad
// ----------------------------------------------------------------------------
//  Put this on a square whose BoxCollider2D has "Is Trigger" ticked.
//  Any player that touches it gets launched straight up.
//
//  The pad is placed by hand in the scene - nothing is ever spawned.
// ============================================================================
public class GeoBouncePad : MonoBehaviour
{
    [Tooltip("How fast the player is fired upwards.")]
    public float bounceForce = 16.5f;

    [Tooltip("Should the pad also keep the player's sideways speed?")]
    public bool keepSideSpeed = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bounce(collision);
    }

    // Also work if the pad is ever made solid instead of a trigger.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.collider);
    }

    private void Bounce(Collider2D other)
    {
        if (other == null) return;

        // Only bounce things that have physics (the player does).
        Rigidbody2D body = other.attachedRigidbody;
        if (body == null) return;

        float side = keepSideSpeed ? body.velocity.x : 0f;
        body.velocity = new Vector2(side, bounceForce);

        Debug.Log("Boing!");
    }
}
