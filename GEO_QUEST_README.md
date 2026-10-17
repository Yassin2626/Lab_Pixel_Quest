# Geo Quest — what was added

Everything below lives **inside the .unity scene files**. No script ever creates or
spawns a GameObject at runtime — open any level in the Hierarchy and every spike,
pad, goal, belt and water block is sitting there, ready to select and move by hand.

---

## Files

| File | What it is |
|---|---|
| `Assets/Geo_Quest/Scenes/Level_0_Tutorial.unity` | **Tutorial** — the very first, very easy level (new) |
| `Assets/Geo_Quest/Scenes/Level_1.unity` | **Level 1 — Basic Movement** |
| `Assets/Geo_Quest/Scenes/Level_2.unity` | **Level 2 — Bounce Pads, Walls & Spikes** |
| `Assets/Geo_Quest/Scenes/Level_3.unity` | **Level 3 — Conveyor Belt, Water & Colour Doors** |
| `Assets/Geo_Quest/Scenes/Geo_Win.unity` | Win screen — waits, then restarts the game |
| `Assets/Geo_Quest/Scripts/GeoController.cs` | Player: move, jump, turn around, spikes, goal, fall death |
| `Assets/Geo_Quest/Scripts/GeoBouncePad.cs` | Bounce pad launcher |
| `Assets/Geo_Quest/Scripts/GeoColorSwitcher.cs` | Keys **1 / 2 / 3** colour switching |
| `Assets/Geo_Quest/Scripts/GeoColorGate.cs` | Coloured door that opens for the matching colour |
| `Assets/Geo_Quest/Scripts/GeoCameraFollow.cs` | Camera follows the player |
| `Assets/Geo_Quest/Scripts/GeoWinScreen.cs` | Restarts from the first level after you win |
| `Assets/Geo_Quest/Assets/PlayerMaterial.physicsMaterial2D` | High friction → the player grips walls/belts |

Also changed: `ProjectSettings/TagManager.asset` (added the **Player** and **Finish**
tags — *Finish was missing, which is why level transitions were broken before*) and
`ProjectSettings/EditorBuildSettings.asset`
(Tutorial → Level_1 → Level_2 → Level_3 → Geo_Win).

---

## Controls

* **A / D** or **← / →** — move (the orc turns to face the way it walks)
* **Space** (or W / ↑) — jump
* **1 / 2 / 3** — switch colour (red / blue / green, colour values scaled 0–1)
* Touching a spike or falling off the world reloads the level
* Touching the green goal loads the next level
* On the win screen: **Space** replays, or it restarts by itself after 8 seconds

---

## Where each rubric item is

| Rubric | Where to look |
|---|---|
| **Player movement (10%)** | `GeoController` + the `Player` object in any level |
| **Spikes (10%)** | The `Spike` objects (red triangles, tag `Death`) — Levels 2 & 3 |
| **Bounce pad (10%)** | The cyan `BouncePad` objects with `GeoBouncePad.cs` — Level 2 |
| **Level goal (10%)** | The green `Goal` objects (tag `Finish`) in every level |
| **Three unique levels (30%)** | Level_1 (pure platforming), Level_2 (bounce pads + walls + spikes), Level_3 (belt + water + colours) — plus the tutorial |
| **Buoyancy / Platform / Surface Effector (15%)** | **BuoyancyEffector2D** on `Water` and **SurfaceEffector2D** on `ConveyorBelt` — both in Level_3 |
| **Colour switching (15%)** | `GeoColorSwitcher.cs` on the Player (keys 1/2/3) plus the three `ColourDoor_*` objects it opens |

Level 3 was built so the colour doors prove the mechanic is real — the door is a
solid panel until the player wears the matching colour, then it fades and lets
them walk through.

---

## Colours

Every block is drawn with a **solid, fully opaque** colour (alpha = 1), so nothing
looks washed out or see-through. The only see-through object is the water, which is
meant to look like water.

---

## Making the .exe

1. `File ▸ Build Settings` — the five Geo Quest scenes are already listed and
   enabled, with **Level_0_Tutorial** first.
2. Platform: **Windows, Mac, Linux Standalone** → Target Platform **Windows**.
3. `Project Settings ▸ Player` — set Fullscreen Mode to **Windowed**, resolution
   **1024 × 576**, tick **Resizable Window**.
4. Click **Build**, pick a new empty folder, and wait.
5. Zip that folder and upload it to the form.
