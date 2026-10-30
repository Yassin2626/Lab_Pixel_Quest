using UnityEngine;

public class HW2PlayerShoot : MonoBehaviour
{
    // Prefabs
    public GameObject preFab;              // Regular bullet (Left Mouse Button)
    public GameObject newBulletPrefab;     // New bullet (Right Mouse Button)

    // Hierarchy References
    public Transform bulletTrash;
    public Transform bulletSpawn;

    // Cooldown Timer
    private const float timer = 0.5f;
    private float currentTime = 0.5f;
    private bool canShoot = true;

    private void Update()
    {
        TimerMethod();
        Shoot();
    }

    private void TimerMethod()
    {
        if (!canShoot)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                canShoot = true;
                currentTime = timer;
            }
        }
    }

    private void Shoot()
    {
        if (!canShoot) return;

        // Left Mouse Button fires regular bullet
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnBullet(preFab);
        }
        // Right Mouse Button fires new bullet
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnBullet(newBulletPrefab != null ? newBulletPrefab : preFab);
        }
    }

    private void SpawnBullet(GameObject bulletToSpawn)
    {
        if (bulletToSpawn == null) return;

        GameObject bullet = Instantiate(bulletToSpawn, bulletSpawn.position, Quaternion.identity);
        bullet.transform.SetParent(bulletTrash);
        canShoot = false;
    }
}
