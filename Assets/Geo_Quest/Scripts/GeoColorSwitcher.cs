using UnityEngine;

// ============================================================================
//  Geo Quest - Colour Switching  (keys 1, 2 and 3)
// ----------------------------------------------------------------------------
//  Press 1, 2 or 3 to change the player's colour.
//  Every colour value is scaled from 0 to 1 (that is what Color() wants).
//
//  1 = red      2 = blue      3 = green
//
//  The coloured doors in Level 3 look at CurrentColorIndex to decide whether
//  they should open for us.
//
//  This script only reads the keyboard and tints the picture.
// ============================================================================
public class GeoColorSwitcher : MonoBehaviour
{
    public Color color1 = new Color(1f, 0.2f, 0.2f);      // key 1 - red
    public Color color2 = new Color(0.2f, 0.55f, 1f);     // key 2 - blue
    public Color color3 = new Color(0.2f, 0.9f, 0.35f);   // key 3 - green

    private SpriteRenderer sprite;
    private int current = -1;      // -1 means "no colour picked yet"

    // 0 = red, 1 = blue, 2 = green, -1 = no colour. The doors read this.
    public int CurrentColorIndex { get { return current; } }

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetColor(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetColor(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetColor(2);
    }

    // Paint the player's picture in the chosen colour.
    void SetColor(int index)
    {
        current = index;

        if (index == 0) sprite.color = color1;
        else if (index == 1) sprite.color = color2;
        else sprite.color = color3;
    }
}
