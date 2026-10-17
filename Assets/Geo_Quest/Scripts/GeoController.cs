using UnityEngine;
using UnityEngine.SceneManagement;

// ============================================================================
//  Geo Quest - Player Controller
// ----------------------------------------------------------------------------
//  This script does four things:
//      1. Move the player left and right with A / D (or the arrow keys)
//      2. Jump with SPACE (or W / Up arrow)
//      3. Turn the player's picture around so it faces the way it walks
//      4. Restart the level if we hit a spike or fall off the world,
//         and load the next level when we touch the goal
//
//  IMPORTANT: this script never creates any objects. Every spike, bounce pad,
//  goal, belt and water pool is already sitting inside the scene file.
// ============================================================================
public class GeoController : MonoBehaviour
{
    public int speed = 5;                  // how fast the player runs
    public float jumpForce = 11f;          // how high the player jumps
    public float fallLimit = -8f;          // fall below this height and you die
    public string nextLevel = "Level_2";   // level to load when we touch the goal

    private Rigidbody2D rb;                // the physics body of the player
    private BoxCollider2D box;             // the invisible box around the player
    private SpriteRenderer sprite;         // the player's picture

    private bool leaving;                  // stops us dying / winning twice

    void Start()
    {
        // Find the parts that are already on the player.
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ---- 1. Read the keyboard -------------------------------------------
        // "Horizontal" is -1 when we hold left, 0 when we do nothing,
        // and +1 when we hold right.
        float move = Input.GetAxisRaw("Horizontal");

        bool jumpPressed = Input.GetKeyDown(KeyCode.Space)
                        || Input.GetKeyDown(KeyCode.W)
                        || Input.GetKeyDown(KeyCode.UpArrow);

        // ---- 2. Move left and right -----------------------------------------
        // We only set the speed while a key is held down. When the player lets
        // go we leave the speed alone, which is what lets the conveyor belt
        // and the water push them along.
        if (move != 0)
        {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }

        // ---- 3. Turn the picture the right way round ------------------------
        // The orc picture is drawn facing left, so we flip it when we go right.
        // (If the orc ever walks backwards, just swap these two lines around.)
        if (move > 0) sprite.flipX = true;    // walking right -> flipped
        if (move < 0) sprite.flipX = false;   // walking left  -> not flipped

        // ---- 4. Jump, but only when we are standing on something ------------
        if (jumpPressed && OnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // ---- 5. If we fall off the world, we die ----------------------------
        if (transform.position.y < fallLimit)
        {
            Die();
        }
    }

    // Look in a small box just under the player's feet.
    // If anything solid is in there, we are standing on the ground.
    private bool OnGround()
    {
        Vector2 middle = new Vector2(box.bounds.center.x, box.bounds.min.y - 0.1f);
        Vector2 size = new Vector2(box.bounds.size.x * 0.9f, 0.2f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(middle, size, 0f);

        foreach (Collider2D hit in hits)
        {
            // Ignore our own body, and ignore triggers (spikes, pads, water).
            if (hit != box && !hit.isTrigger)
            {
                return true;
            }
        }

        return false;
    }

    // This runs when the player touches a trigger object (spikes, goal, water).
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Death")   // a spike
        {
            Die();
        }
        else if (other.tag == "Finish")   // the goal
        {
            WinLevel();
        }
    }

    // Restart the level we are already in.
    void Die()
    {
        if (leaving) return;
        leaving = true;

        Debug.Log("Player has died");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load the next level.
    void WinLevel()
    {
        if (leaving) return;
        leaving = true;

        SceneManager.LoadScene(nextLevel);
    }
}
