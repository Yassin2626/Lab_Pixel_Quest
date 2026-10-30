#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class HW2SceneSetup
{
    [MenuItem("HW2/Setup Scene")]
    public static void Setup()
    {
        string scenePath = "Assets/Hands-On Homework #2/Scenes/HW2_Scene.unity";
        var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

        // 1. Create or update NewBullet prefab
        string bulletPrefabPath = "Assets/Hands-On Homework #2/PreFabs/Bullet.prefab";
        string newBulletPrefabPath = "Assets/Hands-On Homework #2/PreFabs/NewBullet.prefab";
        
        GameObject bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(bulletPrefabPath);
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet.prefab not found at " + bulletPrefabPath);
            return;
        }

        // Instantiate bullet to create new bullet variant
        GameObject newBulletInstance = Object.Instantiate(bulletPrefab);
        newBulletInstance.name = "NewBullet";
        newBulletInstance.tag = "Bullet";
        newBulletInstance.transform.localScale = new Vector3(1.6f, 1.6f, 1f);

        // Update CircleCollider2D radius
        var col = newBulletInstance.GetComponent<CircleCollider2D>();
        if (col != null)
        {
            col.radius = 0.12f;
        }

        // Update HW2Bullet speed to 5 (higher speed than regular bullet's 2)
        var bulletScript = newBulletInstance.GetComponent<HW2Bullet>();
        if (bulletScript != null)
        {
            bulletScript.speed = 5f;
            bulletScript.deathTime = 1.5f;
            bulletScript.playerBullet = true;
        }

        // Update Sprite child
        Transform spriteChild = newBulletInstance.transform.Find("Sprite");
        if (spriteChild != null)
        {
            var sr = spriteChild.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                // Find sliced sprite Sprite_Tilemap_27 in Sprite_Tilemap.png
                string tilemapPath = "Assets/Hands-On Homework #2/Sprites/Sprite_Tilemap.png";
                Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(tilemapPath);
                Sprite targetSprite = null;
                if (allAssets != null)
                {
                    foreach (var a in allAssets)
                    {
                        if (a is Sprite s && s.name == "Sprite_Tilemap_27")
                        {
                            targetSprite = s;
                            break;
                        }
                    }
                }
                if (targetSprite != null)
                {
                    sr.sprite = targetSprite;
                }
                // Fiery orange/plasma color
                sr.color = new Color(1f, 0.45f, 0.1f, 1f);
            }
        }

        GameObject savedNewBulletPrefab = PrefabUtility.SaveAsPrefabAsset(newBulletInstance, newBulletPrefabPath);
        Object.DestroyImmediate(newBulletInstance);
        AssetDatabase.SaveAssets();
        Debug.Log("Created NewBullet prefab at " + newBulletPrefabPath);

        // 2. Configure Player GameObject in HW2_Scene
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found in HW2_Scene!");
            return;
        }

        // HW2PlayerMovement
        var movement = player.GetComponent<HW2PlayerMovement>();
        if (movement == null) movement = player.AddComponent<HW2PlayerMovement>();
        movement.speed = 3f;

        // HW2PlayerRotation
        var rotation = player.GetComponent<HW2PlayerRotation>();
        if (rotation == null) rotation = player.AddComponent<HW2PlayerRotation>();

        // HW2PlayerDie
        var die = player.GetComponent<HW2PlayerDie>();
        if (die == null) die = player.AddComponent<HW2PlayerDie>();
        
        // Find EndGame panel (including inactive objects)
        GameObject endGamePanel = null;
        Canvas[] canvases = Object.FindObjectsOfType<Canvas>(true);
        foreach (var c in canvases)
        {
            Transform t = c.transform.Find("EndGame");
            if (t != null)
            {
                endGamePanel = t.gameObject;
                break;
            }
        }
        if (endGamePanel == null)
        {
            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (go.name == "EndGame" && go.scene == scene)
                {
                    endGamePanel = go;
                    break;
                }
            }
        }
        die.endPanel = endGamePanel;
        Debug.Log("Assigned EndGame panel: " + (endGamePanel != null ? endGamePanel.name : "null"));

        // HW2PlayerShoot
        var shoot = player.GetComponent<HW2PlayerShoot>();
        if (shoot == null) shoot = player.AddComponent<HW2PlayerShoot>();

        shoot.preFab = bulletPrefab;
        shoot.newBulletPrefab = savedNewBulletPrefab;

        GameObject trashObj = GameObject.Find("BulletTrash");
        if (trashObj != null) shoot.bulletTrash = trashObj.transform;

        Transform spawnPoint = player.transform.Find("SpawnPoint");
        if (spawnPoint != null) shoot.bulletSpawn = spawnPoint;

        EditorUtility.SetDirty(player);
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();

        Debug.Log("HW2 Scene Setup completed successfully!");
    }
}
#endif
