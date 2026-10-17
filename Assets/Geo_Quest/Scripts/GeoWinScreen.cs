using UnityEngine;
using UnityEngine.SceneManagement;

// ============================================================================
//  Geo Quest - Win Screen
// ----------------------------------------------------------------------------
//  This sits on a small empty object in the Geo_Win scene.
//
//  After you win the last level the game waits a few seconds and then starts
//  again from the first level. You can also press SPACE to restart right away.
//
//  Nothing is created here either.
// ============================================================================
public class GeoWinScreen : MonoBehaviour
{
    public string firstLevel = "Level_1";   // the level we restart the game on
    public float restartAfter = 8f;         // seconds before it restarts by itself

    void Start()
    {
        // Call RestartGame() once, after "restartAfter" seconds.
        Invoke("RestartGame", restartAfter);
    }

    void Update()
    {
        // Or let the player restart straight away with SPACE.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    // Load the first level again, so the game starts over.
    void RestartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }
}
