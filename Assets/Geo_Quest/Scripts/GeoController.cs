using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GeoController : MonoBehaviour
{
    // ---- Variables (Lesson: Variables & Scope) ----
    private string Var2 = "Hello ";   // global variable (declared in the class, used by every method)

    public int speed = 5;                        
    public string nextLevel = "Level_2";      

    // ---- Components ----
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Challenge: print the global variable + a local variable
        string Var3 = "World";     // local variable (only exists inside Start)
        Debug.Log(Var2 + Var3);
    }

    // Update runs every frame
    void Update()
    {
        if (rb == null) return;

        // ---- Movement with the Input Manager ----
        // Horizontal axis = -1 (A / Left Arrow), 0 (nothing), 1 (D / Right Arrow)
        float xInput = Input.GetAxis("Horizontal");

        // Only change the X velocity, keep the Y velocity so gravity still works
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        // ---- Safety: if the player falls off the world, treat it as a death ----
        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    // ---- Collision Detection (Lesson: OnTriggerEnter2D + Tags + Switch) ----
    // Fires when the player touches an object that has a Collider2D with "Is Trigger" checked
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");

        switch (collision.tag)
        {
            case "Death":
                Die();
                break;

            case "Finish":
                if (Application.CanStreamedLevelBeLoaded(nextLevel))
                {
                    SceneManager.LoadScene(nextLevel);
                }
                else
                {
                    Debug.LogWarning("Scene '" + nextLevel + "' is not in File > Build Settings yet.");
                }
                break;
        }
    }

    // ---- Death: print the message and restart this level ----
    private void Die()
    {
        Debug.Log("Player Has Died");

        string thisLevel = SceneManager.GetActiveScene().name;

        if (Application.CanStreamedLevelBeLoaded(thisLevel))
        {
            SceneManager.LoadScene(thisLevel);
        }
        else
        {
            Debug.LogWarning("Scene '" + thisLevel + "' is not in File > Build Settings yet.");
        }
    }

    // ---- Old code from the earlier challenges (commented out so it does not conflict) ----
    /*
    // Continuous movement along the X axis
    transform.position += new Vector3(0.005f, 0, 0);

    // WASD Movement Controls
    if (Input.GetKeyDown(KeyCode.W))
    {
        transform.position += new Vector3(0, 1, 0);
    }

    if (Input.GetKeyDown(KeyCode.S))
    {
        transform.position += new Vector3(0, -1, 0);
    }

    if (Input.GetKeyDown(KeyCode.A))
    {
        transform.position += new Vector3(-1, 0, 0);
    }

    if (Input.GetKeyDown(KeyCode.D))
    {
        transform.position += new Vector3(1, 0, 0);
    }
    */
}
