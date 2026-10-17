using UnityEngine;

// ============================================================================
//  Geo Quest - Colour Door
// ----------------------------------------------------------------------------
//  A coloured wall that only opens when the player is wearing the SAME colour.
//
//  requiredColor:  0 = red   (press 1)
//                  1 = blue  (press 2)
//                  2 = green (press 3)
//
//  Drag the Player into the "Player" box in the Inspector.
//
//  Nothing is created here. The door is already in the scene, and this script
//  only turns its wall on and off.
// ============================================================================
public class GeoColorGate : MonoBehaviour
{
    public int requiredColor = 0;        // 0 = red, 1 = blue, 2 = green
    public GeoColorSwitcher player;      // the player's colour switcher

    private BoxCollider2D doorWall;      // the invisible wall
    private SpriteRenderer sprite;       // the coloured picture

    void Start()
    {
        doorWall = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        Open(false);      // start closed
    }

    void Update()
    {
        // Is the player wearing the colour this door wants?
        bool sameColour = player != null && player.CurrentColorIndex == requiredColor;

        Open(sameColour);
    }

    // Open the door = switch the wall off and make the picture see-through.
    void Open(bool open)
    {
        doorWall.enabled = !open;

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,
                                 open ? 0.25f : 1f);
    }
}
