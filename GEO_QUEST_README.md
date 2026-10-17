# Geo Quest — what was added

Everything below lives **inside the .unity scene files**. No script ever creates or
spawns a GameObject at runtime — open any level in the Hierarchy and every spike,
pad, goal, belt and water block is sitting there, ready to select and move by hand.

---

## Files

| File | What it is |
|---|---|
| `Assets/Geo_Quest/Scenes/Level_1.unity` | **Level 1 — Basic Movement** (rebuilt) |
| `Assets/Geo_Quest/Scenes/Level_2.unity` | **Level 2 — Bounce Pads, Walls & Spikes** (rebuilt) |
| `Assets/Geo_Quest/Scenes/Level_3.unity` | **Level 3 — Conveyor Belt, Water & Colour Doors** (new) |
| `Assets/Geo_Quest/Scenes/Geo_Win.unity` | Win screen (new) |
| `Assets/Geo_Quest/Scripts/GeoController.cs` | Player: move, jump, wall slide, spikes, goal, fall death |
| `Assets/Geo_Quest/Scripts/GeoBouncePad.cs` | Bounce pad launcher |
| `Assets/Geo_Quest/Scripts/GeoColorSwitcher.cs` | Keys **1 / 2 / 3** colour switching |
| `Assets/Geo_Quest/Scripts/GeoColorGate.cs` | Coloured door that opens for the matching colour |
| `Assets/Geo_Quest/Scripts/GeoCameraFollow.cs` | Camera follows the player |
| `Assets/Geo_Quest/Scripts/GeoWinScreen.cs` | Space restarts from Level 1 |
| `Assets/Geo_Quest/Assets/PlayerMaterial.physicsMaterial2D` | High friction → the player grips walls/belts |

Also changed: `ProjectSettings/TagManager.asset` (added the **Player** and **Finish**
tags — *Finish was missing, which is why level transitions were broken before*) and
`ProjectSettings/EditorBuildSettings.asset` (Level_1 → Level_2 → Level_3 → Geo_Win).

---

## Controls

* **A / D** or **← / →** — move
* **Space** (or W / ↑) — jump
* **1 / 2 / 3** — switch colour (red / blue / green, colour values scaled 0–1)
* Touching a spike or falling off the world reloads the level
* Touching the green goal loads the next level

---

## Where each rubric item is

| Rubric | Where to look |
|---|---|
| **Player movement (10%)** | `GeoController` + the `Player` object in any level |
| **Spikes (10%)** | The `Spike` objects (red triangles, tag `Death`) — Levels 2 & 3 |
| **Bounce pad (10%)** | The cyan `BouncePad` objects with `GeoBouncePad.cs` — Level 2 |
| **Level goal (10%)** | The green `Goal` objects (tag `Finish`) in every level |
| **Three unique levels (30%)** | Level_1 (pure platforming), Level_2 (bounce pads + walls + spikes), Level_3 (belt + water + colours) |
| **Buoyancy / Platform / Surface Effector (15%)** | **BuoyancyEffector2D** on `Water` and **SurfaceEffector2D** on `ConveyorBelt` — both in Level_3 |
| **Colour switching (15%)** | `GeoColorSwitcher.cs` on the Player (keys 1/2/3) plus the three `ColourDoor_*` objects it opens |

Level 3 was built so the colour doors prove the mechanic is real — the door is a
solid panel until the player wears the matching colour, then it fades and lets
them walk through.

---

## Important: how to open it

The Unity Editor was **running while these files were written**, so please:

1. **Close Unity without saving anything** (if it asks to save an open scene, say no).
2. Re-open the project from Unity Hub.

That guarantees the new scenes, tags and Build Settings are all picked up.

If Unity ever asks whether to reload a scene that "changed on disk", choose
**Reload** (do not overwrite it with the in-memory copy).

The two original scene files were backed up to
`%TEMP%\opencode\geobuild\backup\` before being rebuilt, just in case.

---

## Making the .exe

1. `File ▸ Build Settings` — the four Geo Quest scenes are already listed and
   enabled, with **Level_1** first.
2. Platform: **Windows, Mac, Linux Standalone** → Target Platform **Windows**.
3. `Project Settings ▸ Player` — set Fullscreen Mode to **Windowed**, resolution
   **1024 × 576**, tick **Resizable Window**.
4. Click **Build**, pick a new empty folder, and wait.
5. Zip that folder and upload it to the form.
