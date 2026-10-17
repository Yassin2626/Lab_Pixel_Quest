using UnityEngine;
using UnityEngine.SceneManagement;

// ============================================================================
//  Geo Quest - Win Screen
// ----------------------------------------------------------------------------
//  Sits on a tiny object in the Geo_Win scene. Press Space / R to jump back to
//  the first level. Nothing is spawned here either.
// ============================================================================
public class GeoWinScreen : MonoBehaviour
{
    public string firstLevel = "Level_1";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.R)
            || Input.GetKeyDown(KeyCode.Return))
        {
            if (Application.CanStreamedLevelBeLoaded(firstLevel))
            {
                SceneManager.LoadScene(firstLevel);
            }
            else
            {
                Debug.LogWarning("Scene '" + firstLevel + "' is not in File > Build Settings yet.");
            }
        }
    }
}
