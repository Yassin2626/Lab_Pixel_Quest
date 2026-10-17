using UnityEngine;
using UnityEngine.SceneManagement;

// ============================================================================
//  Geo Quest - Player Controller
// ----------------------------------------------------------------------------
//  Handles the core mechanics of the game:
//      * Movement .................. A / D  or  Left / Right Arrow
//      * Jumping ................... Space  (or W / Up Arrow)
//      * Wall interaction .......... high friction lets the player cling + slide
//      * Dying to spikes ........... anything tagged "Death"
//      * Level progression ......... touching the goal (tag "Finish")
//      * Falling off the world ..... treated as a death
//
//  NOTE: this script NEVER creates / spawns objects. Every spike, bounce pad,
//  goal, belt, water pool ... is placed by hand inside the .unity scene files.
// ============================================================================
public class GeoController : MonoBehaviour
{
    // ---- Variables (Lesson: Variables & Scope) ----
    [Header("Movement")]
    public int speed = 5;                 // horizontal speed in units per second
    public float acceleration = 45f;      // how quickly the player reaches that speed
    public float brake = 12f;             // how quickly the player slows down when no key is held
    public float jumpForce = 11f;         // upward speed applied when jumping
    public float maxFallSpeed = 18f;      // terminal velocity so falling stays readable
    public float wallSlideSpeed = 2f;     // how slowly the player slides down a wall

    [Header("Level")]
    public string nextLevel = "Level_2";  // scene loaded when the goal is touched
    public float killY = -12f;            // fall below this and the player dies

    // ---- Components ----
    private Rigidbody2D rb;
    private BoxCollider2D box;

    // ---- State ----
    private bool grounded;
    private bool touchingWall;
    private bool leavingLevel;      // stops the level loading twice in one frame

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update runs every frame
    void Update()
    {
        if (rb == null) return;

        // ---- Movement with the Input Manager ----
        // Horizontal axis = -1 (A / Left Arrow), 0 (nothing), 1 (D / Right Arrow)
        float xInput = Input.GetAxisRaw("Horizontal");

        // Are we standing on something / leaning on a wall?
        grounded = GroundCheck();
        touchingWall = WallCheck();

        // ---- Horizontal movement ----
        // We move TOWARDS the wanted speed instead of snapping to it, so that
        // effectors (like the conveyor belt in Level 3) can still push the player.
        float wantedX = xInput * speed;
        float rate = (Mathf.Abs(xInput) > 0.01f) ? acceleration : brake;
        float newX = Mathf.MoveTowards(rb.velocity.x, wantedX, rate * Time.deltaTime);
        rb.velocity = new Vector2(newX, rb.velocity.y);

        // ---- Jumping ----
        if (grounded && JumpPressed())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // ---- Wall interaction (Lesson: high friction = cling) ----
        // While airborne and holding into a wall we slide down slowly.
        // If something pushes us UP (a conveyor wall / water) we leave it alone.
        if (!grounded && touchingWall && Mathf.Abs(xInput) > 0.1f && rb.velocity.y < -wallSlideSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }

        // ---- Keep falling speed sane ----
        if (rb.velocity.y < -maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
        }

        // ---- Safety: if the player falls off the world, treat it as a death ----
        if (transform.position.y < killY)
        {
            Die();
        }
    }

    // Space bar (or W / Up Arrow) jumps.
    private bool JumpPressed()
    {
        return Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow);
    }

    // A small box just under the player's feet.
    private bool GroundCheck()
    {
        if (box == null) return false;

        Bounds b = box.bounds;
        Vector2 centre = new Vector2(b.center.x, b.min.y - 0.05f);
        Vector2 size = new Vector2(b.size.x * 0.9f, 0.12f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(centre, size, 0f);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == box) continue;      // ignore our own collider
            if (hits[i].isTrigger) continue;   // triggers (spikes, pads) are not ground
            return true;
        }
        return false;
    }

    // A small vertical box on both sides of the player.
    private bool WallCheck()
    {
        if (box == null) return false;

        Bounds b = box.bounds;
        Vector2 size = new Vector2(0.1f, b.size.y * 0.8f);
        Vector2 left = new Vector2(b.min.x - 0.06f, b.center.y);
        Vector2 right = new Vector2(b.max.x + 0.06f, b.center.y);

        return SideBlocked(left, size) || SideBlocked(right, size);
    }

    private bool SideBlocked(Vector2 centre, Vector2 size)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(centre, size, 0f);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == box) continue;
            if (hits[i].isTrigger) continue;
            return true;
        }
        return false;
    }

    // ---- Collision Detection (Lesson: OnTriggerEnter2D + Tags + Switch) ----
    // Fires when the player touches an object that has a Collider2D with
    // "Is Trigger" ticked.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Death":
                Die();
                break;

            case "Finish":
                GoToNextLevel();
                break;
        }
    }

    // ---- Level Goal: load the next stage ----
    private void GoToNextLevel()
    {
        if (leavingLevel) return;

        if (string.IsNullOrEmpty(nextLevel))
        {
            // No next level set, so just restart this one.
            Die();
            return;
        }

        if (Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            leavingLevel = true;
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogWarning("Scene '" + nextLevel + "' is not in File > Build Settings yet.");
        }
    }

    // ---- Death: print the message and restart this level ----
    public void Die()
    {
        if (leavingLevel) return;
        leavingLevel = true;

        Debug.Log("Player Has Died");

        string thisLevel = SceneManager.GetActiveScene().name;

        if (Application.CanStreamedLevelBeLoaded(thisLevel))
        {
            SceneManager.LoadScene(thisLevel);
        }
        else
        {
            leavingLevel = false;
            Debug.LogWarning("Scene '" + thisLevel + "' is not in File > Build Settings yet.");
        }
    }

    // Small helper so other scripts (bounce pads) can find the rigidbody.
    public Rigidbody2D Body()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        return rb;
    }
}
