using UnityEngine;

// ============================================================================
//  Geo Quest - Colour Switching
// ----------------------------------------------------------------------------
//  Press 1, 2 or 3 to change the player's colour.
//  The colour VALUES are scaled 0 to 1 (that is what new Color(r, g, b) wants).
//
//  -1 means "no colour picked yet", which leaves the player looking normal.
//  The coloured doors in Level 3 read CurrentColorIndex to decide if they open.
//
//  This script only reads input and tints the sprite - it never spawns objects.
// ============================================================================
public class GeoColorSwitcher : MonoBehaviour
{
    [Header("Colour 1  (key 1)")]
    public Color color1 = new Color(1f, 0.25f, 0.25f);      // red
    [Header("Colour 2  (key 2)")]
    public Color color2 = new Color(0.25f, 0.55f, 1f);      // blue
    [Header("Colour 3  (key 3)")]
    public Color color3 = new Color(0.25f, 1f, 0.4f);       // green

    [Tooltip("-1 = no colour chosen yet (player stays untinted)")]
    public int startColor = -1;

    private SpriteRenderer playerSprite;
    private int currentIndex = -1;

    // 0 = red, 1 = blue, 2 = green, -1 = none
    public int CurrentColorIndex { get { return currentIndex; } }

    public Color CurrentColor { get { return ColorFor(currentIndex); } }

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        SetColor(startColor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetColor(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetColor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetColor(2);
        }
    }

    private void SetColor(int index)
    {
        currentIndex = Mathf.Clamp(index, -1, 2);

        if (playerSprite != null)
        {
            playerSprite.color = ColorFor(currentIndex);
        }
    }

    // The three colours, all built with values between 0 and 1.
    private Color ColorFor(int index)
    {
        switch (index)
        {
            case 0: return color1;
            case 1: return color2;
            case 2: return color3;
            default: return new Color(1f, 1f, 1f, 1f);   // no colour = white (normal)
        }
    }
}
