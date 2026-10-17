using UnityEngine;

// ============================================================================
//  Geo Quest - Colour Door
// ----------------------------------------------------------------------------
//  A coloured wall that only opens for the player who is wearing the SAME colour.
//
//  requiredColor:  0 = colour 1 (key 1)
//                  1 = colour 2 (key 2)
//                  2 = colour 3 (key 3)
//
//  Drag the Player's GeoColorSwitcher component into the "Player" slot in the
//  Inspector. Nothing is spawned - the door already lives in the scene and only
//  turns its own collider on / off.
// ============================================================================
[RequireComponent(typeof(BoxCollider2D))]
public class GeoColorGate : MonoBehaviour
{
    [Tooltip("0 = red (key 1),  1 = blue (key 2),  2 = green (key 3)")]
    public int requiredColor = 0;

    [Tooltip("Drag the Player here (its GeoColorSwitcher component).")]
    public GeoColorSwitcher player;

    [Header("Looks")]
    public float closedAlpha = 0.8f;
    public float openAlpha = 0.12f;

    private BoxCollider2D gateCollider;
    private SpriteRenderer gateSprite;
    private Color baseColor = Color.white;
    private bool isOpen;

    void Start()
    {
        gateCollider = GetComponent<BoxCollider2D>();

        gateSprite = GetComponent<SpriteRenderer>();
        if (gateSprite != null) baseColor = gateSprite.color;

        // Make sure we start in a known state.
        Apply(false);
    }

    void Update()
    {
        bool shouldOpen = player != null && player.CurrentColorIndex == requiredColor;

        if (shouldOpen != isOpen)
        {
            Apply(shouldOpen);
        }
    }

    private void Apply(bool open)
    {
        isOpen = open;

        if (gateCollider != null)
        {
            gateCollider.enabled = !open;
        }

        if (gateSprite != null)
        {
            gateSprite.color = new Color(baseColor.r, baseColor.g, baseColor.b,
                                         open ? openAlpha : closedAlpha);
        }
    }
}
